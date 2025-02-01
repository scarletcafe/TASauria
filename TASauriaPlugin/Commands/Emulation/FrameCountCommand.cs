namespace ScarletCafe.TASauriaPlugin.Commands.Emulation;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class FrameCountInput {}

public class FrameCountOutput {
    public long FrameCount { get; set; }
}

public class FrameCountCommand : EmulatorCommand<FrameCountInput, FrameCountOutput>
{
    public FrameCountCommand():
        base(
            @"/emulation/framecount"
        )
    {}

    public override FrameCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, FrameCountInput payload)
    {
        return new FrameCountOutput {
            FrameCount = api.Emulation.FrameCount()
        };
    }
}
