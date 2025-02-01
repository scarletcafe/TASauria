namespace ScarletCafe.TASauriaPlugin;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Server {

    private HttpServer httpServer;

    public Server(Configuration config) {

        IPAddress address = IPAddress.Parse(config.ServerHost);

        httpServer = new HttpServer(address, config.ServerPort, false);

        // HTTP GET handler
        httpServer.OnGet += (sender, e) => {
            var request = e.Request;
            var response = e.Response;

            // Turn values on query string into the input for the payload
            JObject input = [];
            foreach (var key in request.QueryString.AllKeys) {
                string value = request.QueryString.Get(key);

                if (value == "true") {
                    input.Add(key, true);
                } else if (value == "false") {
                    input.Add(key, false);
                } else if (int.TryParse(value, out int intValue)) {
                    input.Add(key, intValue);
                } else if (float.TryParse(value, out float floatValue)) {
                    input.Add(key, floatValue);
                } else {
                    input.Add(key, value);
                }
            }

            Handle(response, request.RawUrl, input);
        };

        // HTTP POST handler
        httpServer.OnPost += (sender, e) => {
            var request = e.Request;
            var response = e.Response;

            using StreamReader reader = new(e.Request.InputStream);
            string value = reader.ReadToEnd();

            JObject input;
            try {
                input = JObject.Parse(value);
            } catch (JsonReaderException exception) {
                WriteObjectToResponse(response, new JObject() {
                    {"status", 400},
                    {"error", $"Invalid JSON in payload:\n{exception}"}
                });
                return;
            }

            Handle(response, request.RawUrl, input);
        };

        httpServer.AddWebSocketService<WebsocketHandler>("/websocket");

        httpServer.Start();
    }

    private void Handle(WebSocketSharp.Net.HttpListenerResponse response, string path, JObject input) {
        JObject output = Commands.Registry.Resolve(path, input);
        WriteObjectToResponse(response, output);
    }

    private void WriteObjectToResponse(WebSocketSharp.Net.HttpListenerResponse response, JObject payload) {
        string jsonOutput = payload.ToString(Formatting.None);
        byte[] buffer = Encoding.UTF8.GetBytes(jsonOutput);
        response.ContentLength64 = buffer.Length;
        response.ContentEncoding = Encoding.UTF8;
        response.ContentType = "application/json";
        response.StatusCode = payload.GetValue("status")?.ToObject<int>() ?? 200;

        try {
            response.Close(buffer, true);
        } catch (IOException) {
            // This happens if e.g. the other end hangs up while we were processing, so it's not a huge deal
        }
    }

    public void Stop()
    {
        httpServer.Stop();
    }

    private class WebsocketHandler : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e)
        {
            JObject input;
            try {
                input = JObject.Parse(e.Data);
            } catch (JsonReaderException exception) {
                Send(new JObject() {
                    {"status", 400},
                    {"error", $"Invalid JSON sent over socket:\n{exception}"},
                }.ToString(Formatting.None));
                return;
            }

            string? path = input.GetValue("command")?.ToObject<string>();

            if (path == null) {
                Send(new JObject() {
                    {"status", 400},
                    {"error", $"No command field was included in the payload"},
                    {"messageIdentifier", input.GetValue("messageIdentifier")},
                }.ToString(Formatting.None));
                return;
            }

            JObject output = Commands.Registry.Resolve(path, input);

            Send(output.ToString(Formatting.None));
        }
    }
}
