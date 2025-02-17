namespace ScarletCafe.TASauriaPlugin.Commands.AV;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;


public class PauseAVInput {
    public bool? Set { get; set; } = null;
}

public class PauseAVOutput {
    public bool Paused { get; set; }
}

public class PauseAVCommand : EmulatorCommand<PauseAVInput, PauseAVOutput>
{
    public PauseAVCommand():
        base(
            @"^/av/pause$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowAVControl || (input?.GetValue("set")?.Type ?? JTokenType.Null) == JTokenType.Null;
    }

    public override string SecurityRemarks { get; } = "To set A/V pause state, this command requires 'Allow A/V control' to be enabled in the TASauria plugin security settings.";

    public override PauseAVOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, PauseAVInput payload)
    {
        bool paused = emulator.MainForm.PauseAvi;

        if (payload.Set == true) {
            emulator.APIs.EmuClient.PauseAv();
        } else if (payload.Set == false) {
            emulator.APIs.EmuClient.UnpauseAv();
        }

        return new PauseAVOutput {
            Paused = paused
        };
    }
}
