namespace ScarletCafe.TASauriaPlugin.Commands.Joypad;

using System.Collections.Generic;
using System.Linq;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class SetInput {
    public int? Controller { get; set; }
    public Dictionary<string, JValue> State { get; set; } = [];
}


public class SetCommand : EmulatorCommand<SetInput, GetOutput>
{
    public SetCommand():
        base(
            @"/joypad/set"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowJoypadSystemInput;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow joypad/system input' to be enabled in the TASauria plugin security settings.";

    public override GetOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SetInput payload)
    {
        var dictionary = api.Joypad.Get(payload.Controller);

        var buttonInputs = new Dictionary<string, bool>();
        var analogInputs = new Dictionary<string, int?>();

        foreach (var pair in payload.State) {
            if (pair.Value.Type == JTokenType.Integer && dictionary.ContainsKey(pair.Key)) {
                analogInputs.Add(pair.Key, pair.Value.Value<int>());
            } else if (pair.Value.Type == JTokenType.Boolean && dictionary.ContainsKey(pair.Key)) {
                buttonInputs.Add(pair.Key, pair.Value.Value<bool>());
            }
        }

        api.Joypad.Set(buttonInputs, payload.Controller);
        api.Joypad.SetAnalog(analogInputs, payload.Controller);

        return new GetOutput {
            State = dictionary.ToDictionary(
                x => x.Key,
                x => {
                    if (x.Value is int v1)
                        return JToken.FromObject(v1);
                    else if (x.Value is bool v2)
                        return JToken.FromObject(v2);
                    return JValue.CreateNull();
                }
            )
        };
    }
}
