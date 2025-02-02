namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class PauseInput {
    public bool? Set { get; set; } = null;
}

public class PauseOutput {
    public bool Paused { get; set; }
}

public class PauseCommand : EmulatorCommand<PauseInput, PauseOutput>
{
    public PauseCommand():
        base(
            @"/client/pause"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl || input?.GetValue("set")?.ToObject<bool?>() == null;
    }

    public override string SecurityRemarks { get; } = "To set pause state, this command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

    public override PauseOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, PauseInput payload)
    {
        bool paused = api.EmuClient.IsPaused();

        if (payload.Set == true) {
            api.EmuClient.Pause();
        } else if (payload.Set == false) {
            api.EmuClient.Unpause();
        }

        return new PauseOutput {
            Paused = paused
        };
    }
}
