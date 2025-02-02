namespace ScarletCafe.TASauriaPlugin.Commands.Joypad;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class SetAnalogInput {
    public string Name { get; set; } = "";
    public int Value;
    public int? Controller { get; set; }
}

public class SetAnalogOutput {
    public int Value;
}

public class SetAnalogCommand : EmulatorCommand<SetAnalogInput, SetAnalogOutput>
{
    public SetAnalogCommand():
        base(
            @"/joypad/setanalog"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowJoypadSystemInput;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow joypad/system input' to be enabled in the TASauria plugin security settings.";

    public override SetAnalogOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SetAnalogInput payload)
    {
        var dictionary = api.Joypad.Get(payload.Controller);
        int value;

        if (dictionary.TryGetValue(payload.Name, out object old)) {
            if (old is int val) {
                value = val;
                api.Joypad.SetAnalog(payload.Name, payload.Value, payload.Controller);
            } else {
                throw new ArgumentException("Input with the provided name was not a button.");
            }
        } else {
            throw new ArgumentException("Button with provided name did not exist.");
        }

        return new SetAnalogOutput {
            Value = value
        };
    }
}
