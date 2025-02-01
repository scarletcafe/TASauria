namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class LaggedOutput {
    public bool Lagged { get; set; }
}

public class LaggedCommand : EmulatorCommand<NoArguments, LaggedOutput>
{
    public LaggedCommand():
        base(
            @"/client/lagged"
        )
    {}

    public override LaggedOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new LaggedOutput {
            Lagged = api.Emulation.IsLagged(),
        };
    }
}
