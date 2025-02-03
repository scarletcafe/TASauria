namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class DisplayTypeOutput {
    public string DisplayType { get; set; } = "";
}

public class DisplayTypeCommand : EmulatorCommand<NoArguments, DisplayTypeOutput>
{
    public DisplayTypeCommand():
        base(
            @"^/client/displaytype$"
        )
    {}

    public override DisplayTypeOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new DisplayTypeOutput {
            DisplayType = api.Emulation.GetDisplayType(),
        };
    }
}
