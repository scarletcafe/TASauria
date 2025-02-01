namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class CycleCountOutput {
    public long CycleCount { get; set; }
}

public class CycleCountCommand : EmulatorCommand<NoArguments, CycleCountOutput>
{
    public CycleCountCommand():
        base(
            @"/client/cyclecount"
        )
    {}

    public override CycleCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new CycleCountOutput {
            CycleCount = api.Emulation.TotalExecutedCycles(),
        };
    }
}
