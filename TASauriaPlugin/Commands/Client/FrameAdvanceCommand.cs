namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class FrameAdvanceInput {
    public bool? Unpause { get; set; } = null;
}

public class FrameAdvanceCommand : EmulatorCommand<FrameAdvanceInput, FrameCountOutput>
{
    public FrameAdvanceCommand():
        base(
            @"/client/frameadvance"
        )
    {}

    public override FrameCountOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, FrameAdvanceInput payload)
    {
        int frameCount = api.Emulation.FrameCount();
        bool pause = payload.Unpause == null ? api.EmuClient.IsPaused() : !(bool)payload.Unpause;

        if (pause) {
            api.EmuClient.DoFrameAdvance();
        } else {
            api.EmuClient.DoFrameAdvanceAndUnpause();
        }

        return new FrameCountOutput {
            FrameCount = frameCount
        };
    }
}
