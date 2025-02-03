namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class CloseROMOutput {
    public bool Success { get; set; }
}

public class CloseROMCommand : EmulatorCommand<NoArguments, CloseROMOutput>
{
    public CloseROMCommand():
        base(
            @"^/client/closerom$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowROMManagement;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow loading and closing ROMs' to be enabled in the TASauria plugin security settings.";

    public override CloseROMOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        api.EmuClient.CloseRom();

        return new CloseROMOutput {
            Success = true,
        };
    }
}
