namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class FrameCountOutput {
    public long FrameCount { get; set; }
}

public class FrameCountCommand : EmulatorCommand<NoArguments, FrameCountOutput>
{
    public FrameCountCommand():
        base(
            @"^/client/framecount$"
        )
    {}

    public override FrameCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new FrameCountOutput {
            FrameCount = api.Emulation.FrameCount(),
        };
    }
}
