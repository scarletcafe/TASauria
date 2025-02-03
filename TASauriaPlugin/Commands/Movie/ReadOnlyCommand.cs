namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class ReadOnlyInput {
    public bool? Set { get; set; } = null;
}

public class ReadOnlyOutput {
    public bool ReadOnly { get; set; }
}

public class ReadOnlyCommand : EmulatorCommand<ReadOnlyInput, ReadOnlyOutput>
{
    public ReadOnlyCommand():
        base(
            @"^/movie/readonly$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMovieManagement || (input?.GetValue("set")?.Type ?? JTokenType.Null) == JTokenType.Null;
    }

    public override string SecurityRemarks { get; } = "To set readonly state, this command requires 'Allow movie management' to be enabled in the TASauria plugin security settings.";

    public override ReadOnlyOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, ReadOnlyInput payload)
    {
        bool @readonly = api.Movie.GetReadOnly();

        if (payload.Set == true) {
            api.Movie.SetReadOnly(true);
        } else if (payload.Set == false) {
            api.Movie.SetReadOnly(false);
        }

        return new ReadOnlyOutput {
            ReadOnly = @readonly
        };
    }
}
