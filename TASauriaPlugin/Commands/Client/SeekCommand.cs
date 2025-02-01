namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


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
            @"/client/seek"
        )
    {}

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
