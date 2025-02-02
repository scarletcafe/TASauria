namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class RebootCoreOutput {
    public bool Success { get; set; }
}

public class RebootCoreCommand : EmulatorCommand<NoArguments, RebootCoreOutput>
{
    public RebootCoreCommand():
        base(
            @"/client/rebootcore"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

    public override RebootCoreOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        api.EmuClient.RebootCore();

        return new RebootCoreOutput {
            Success = true,
        };
    }
}
