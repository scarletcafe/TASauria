namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class TurboOutput {
    public bool Turboing { get; set; }
}

public class TurboCommand : EmulatorCommand<NoArguments, TurboOutput>
{
    public TurboCommand():
        base(
            @"/client/turbo"
        )
    {}

    public override TurboOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new TurboOutput {
            Turboing = api.EmuClient.IsTurbo(),
        };
    }
}
