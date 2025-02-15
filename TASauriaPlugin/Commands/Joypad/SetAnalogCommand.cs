namespace ScarletCafe.TASauriaPlugin.Commands.Joypad;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class SetAnalogInput {
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = "";
    [JsonProperty(Required = Required.Always)]
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
            @"^/joypad/setanalog$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowJoypadSystemInput;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow joypad/system input' to be enabled in the TASauria plugin security settings.";

    public override SetAnalogOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, SetAnalogInput payload)
    {
        var dictionary = emulator.APIs.Joypad.Get(payload.Controller);
        int value;

        if (dictionary.TryGetValue(payload.Name, out object old)) {
            if (old is int val) {
                value = val;
                emulator.APIs.Joypad.SetAnalog(payload.Name, payload.Value, payload.Controller);
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
