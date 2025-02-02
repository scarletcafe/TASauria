namespace ScarletCafe.TASauriaPlugin.Commands.Joypad;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class SetButtonInput {
    public string Name { get; set; } = "";
    public bool Value;
    public int? Controller { get; set; }
}

public class SetButtonOutput {
    public bool Value;
}

public class SetButtonCommand : EmulatorCommand<SetButtonInput, SetButtonOutput>
{
    public SetButtonCommand():
        base(
            @"/joypad/setbutton"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowJoypadSystemInput;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow joypad/system input' to be enabled in the TASauria plugin security settings.";

    public override SetButtonOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SetButtonInput payload)
    {
        var dictionary = api.Joypad.Get(payload.Controller);
        bool value;

        if (dictionary.TryGetValue(payload.Name, out object old)) {
            if (old is bool val) {
                value = val;
                api.Joypad.Set(payload.Name, payload.Value, payload.Controller);
            } else {
                throw new ArgumentException("Input with the provided name was not a button.");
            }
        } else {
            throw new ArgumentException("Button with provided name did not exist.");
        }

        return new SetButtonOutput {
            Value = value
        };
    }
}
