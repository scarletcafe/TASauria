namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class SystemIDOutput {
    public string SystemID { get; set; } = "";
}

public class SystemIDCommand : EmulatorCommand<NoArguments, SystemIDOutput>
{
    public SystemIDCommand():
        base(
            @"/client/systemid"
        )
    {}

    public override SystemIDOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new SystemIDOutput {
            SystemID = api.Emulation.GetSystemId(),
        };
    }
}
