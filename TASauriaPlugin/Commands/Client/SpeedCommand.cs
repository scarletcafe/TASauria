namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class SpeedInput {
    public int? Percentage { get; set; } = null;
}

public class SpeedOutput {
    public int Percentage { get; set; }
}

public class SpeedCommand : EmulatorCommand<SpeedInput, SpeedOutput>
{
    public SpeedCommand():
        base(
            @"^/client/speed$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl || (input?.GetValue("percentage")?.Type ?? JTokenType.Null) == JTokenType.Null;
    }

    public override string SecurityRemarks { get; } = "To set the speed, this command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

    public override SpeedOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, SpeedInput payload)
    {
        int currentValue = emulator.Config.SpeedPercent;

        if (payload.Percentage != null) {
            emulator.APIs.EmuClient.SpeedMode(
                Math.Max(5, Math.Min(5000, (int)payload.Percentage!))
            );
        }

        return new SpeedOutput {
            Percentage = currentValue,
        };
    }
}
