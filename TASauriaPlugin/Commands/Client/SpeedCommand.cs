namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;
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

    public override SpeedOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SpeedInput payload)
    {
        // !HACK!: It's not possible to read the current speed percentage without access to the config
        // While the external tool API exposes Config to the ExternalToolForm, an older version of TASauria had
        // a mode that allowed it to run without the form open, so we wouldn't have been able to retrieve it this
        // way. That's no longer the case, but it's way easier to just pull it from the main form instead of
        // refactoring the code to hot potato the config all the way over here.
        EmuClientApi concreteApi =
            (EmuClientApi)api.EmuClient;
        BizHawk.Client.EmuHawk.MainForm mainForm =
            (BizHawk.Client.EmuHawk.MainForm)concreteApi
            .GetType()
            .GetField("_mainForm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);
        Config config =
            (Config)mainForm
            .GetType()
            .GetProperty("Config", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(mainForm);

        int currentValue = config.SpeedPercent;

        if (payload.Percentage != null) {
            api.EmuClient.SpeedMode(
                Math.Max(5, Math.Min(5000, (int)payload.Percentage!))
            );
        }

        return new SpeedOutput {
            Percentage = currentValue,
        };
    }
}
