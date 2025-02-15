namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;


public class FrameStatusOutput {
    public long CycleCount { get; set; }
    public long FrameCount { get; set; }
    public long LagCount { get; set; }
    public bool Lagged { get; set; }
}

public class FrameStatusCommand : EmulatorCommand<NoArguments, FrameStatusOutput>
{
    public FrameStatusCommand():
        base(
            @"^/client/framestatus$"
        )
    {}

    public override FrameStatusOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new FrameStatusOutput {
            CycleCount = emulator.APIs.Emulation.TotalExecutedCycles(),
            FrameCount = emulator.APIs.Emulation.FrameCount(),
            LagCount = emulator.APIs.Emulation.LagCount(),
            Lagged = emulator.APIs.Emulation.IsLagged(),
        };
    }
}
