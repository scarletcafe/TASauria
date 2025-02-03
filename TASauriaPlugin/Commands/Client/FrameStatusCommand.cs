namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


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

    public override FrameStatusOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new FrameStatusOutput {
            CycleCount = api.Emulation.TotalExecutedCycles(),
            FrameCount = api.Emulation.FrameCount(),
            LagCount = api.Emulation.LagCount(),
            Lagged = api.Emulation.IsLagged(),
        };
    }
}
