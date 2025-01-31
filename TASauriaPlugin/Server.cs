namespace ScarletCafe.TASauriaPlugin;

using System.Net;
using System.Text;
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

            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = "application/json";

            byte[] buffer = Encoding.UTF8.GetBytes("\"Test\"");
            response.ContentLength64 = buffer.Length;
            response.Close(buffer, true);
        };

        httpServer.Start();
    }

    public void Stop()
    {
        httpServer.Stop();
    }
}
