namespace ScarletCafe.TASauriaPlugin.Commands.Emulation;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class FrameAdvanceCommand : EmulatorCommand<FrameCountInput, FrameCountOutput>
{
    public FrameAdvanceCommand():
        base(
            @"/emulation/frameadvance"
        )
    {}

    public override FrameCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, FrameCountInput payload)
    {
        api.EmuClient.DoFrameAdvance();

        return new FrameCountOutput {
            FrameCount = api.Emulation.FrameCount()
        };
    }
}
