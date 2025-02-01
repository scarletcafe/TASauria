namespace ScarletCafe.TASauriaPlugin.Commands.Emulation;

using System.Collections.Generic;
using BizHawk.Client.Common;

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
            @"/emulation/pause"
        )
    {}

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
