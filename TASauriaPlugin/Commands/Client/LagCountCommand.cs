namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class LagCountOutput {
    public long LagCount { get; set; }
}

public class LagCountCommand : EmulatorCommand<NoArguments, LagCountOutput>
{
    public LagCountCommand():
        base(
            @"/client/lagcount"
        )
    {}

    public override LagCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new LagCountOutput {
            LagCount = api.Emulation.LagCount(),
        };
    }
}
