namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class SeekInput {
    public int? Frame { get; set; } = null;
}

public class SeekOutput {
    public bool Seeking { get; set; }
}

public class SeekingCommand : EmulatorCommand<SeekInput, SeekOutput>
{
    public SeekingCommand():
        base(
            @"^/client/seek$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl || (input?.GetValue("frame")?.Type ?? JTokenType.Null) == JTokenType.Null;
    }

    public override string SecurityRemarks { get; } = "To seek, this command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

    public override SeekOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SeekInput payload)
    {
        bool seeking = api.EmuClient.IsSeeking();

        if (payload.Frame != null) {
            api.EmuClient.SeekFrame((int)payload.Frame!);
        }

        return new SeekOutput {
            Seeking = seeking,
        };
    }
}
