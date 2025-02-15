namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class PauseAVCommand : EmulatorCommand<PauseInput, PauseOutput>
{
    public PauseAVCommand():
        base(
            @"^/client/pauseav$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowAVControl || (input?.GetValue("set")?.Type ?? JTokenType.Null) == JTokenType.Null;
    }

    public override string SecurityRemarks { get; } = "To set A/V pause state, this command requires 'Allow A/V control' to be enabled in the TASauria plugin security settings.";

    public override PauseOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, PauseInput payload)
    {
        bool paused = emulator.MainForm.PauseAvi;

        if (payload.Set == true) {
            emulator.APIs.EmuClient.PauseAv();
        } else if (payload.Set == false) {
            emulator.APIs.EmuClient.UnpauseAv();
        }

        return new PauseOutput {
            Paused = paused
        };
    }
}
