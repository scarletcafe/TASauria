namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
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

    public override FrameStatusOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, FrameAdvanceInput payload)
    {
        long cycleCount = emulator.APIs.Emulation.TotalExecutedCycles();
        int frameCount = emulator.APIs.Emulation.FrameCount();
        int lagCount = emulator.APIs.Emulation.LagCount();
        bool lagged = emulator.APIs.Emulation.IsLagged();

        bool pause = payload.Unpause == null ? emulator.APIs.EmuClient.IsPaused() : !(bool)payload.Unpause;

        if (pause) {
            emulator.APIs.EmuClient.DoFrameAdvance();
        } else {
            emulator.APIs.EmuClient.DoFrameAdvanceAndUnpause();
        }

        return new FrameStatusOutput {
            CycleCount = cycleCount,
            FrameCount = frameCount,
            LagCount = lagCount,
            Lagged = lagged
        };
    }
}
