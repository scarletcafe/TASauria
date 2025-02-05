namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class FrameAdvanceInput {
    public bool? Unpause { get; set; } = null;
}

public class FrameAdvanceCommand : EmulatorCommand<FrameAdvanceInput, FrameStatusOutput>
{
    public FrameAdvanceCommand():
        base(
            @"^/client/frameadvance$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

    public override FrameStatusOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, FrameAdvanceInput payload)
    {
        long cycleCount = api.Emulation.TotalExecutedCycles();
        int frameCount = api.Emulation.FrameCount();
        int lagCount = api.Emulation.LagCount();
        bool lagged = api.Emulation.IsLagged();

        bool pause = payload.Unpause == null ? api.EmuClient.IsPaused() : !(bool)payload.Unpause;

        if (pause) {
            api.EmuClient.DoFrameAdvance();
        } else {
            api.EmuClient.DoFrameAdvanceAndUnpause();
        }

        return new FrameStatusOutput {
            CycleCount = cycleCount,
            FrameCount = frameCount,
            LagCount = lagCount,
            Lagged = lagged
        };
    }
}
