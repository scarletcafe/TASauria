namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class PauseAVCommand : EmulatorCommand<PauseInput, PauseOutput>
{
    public PauseAVCommand():
        base(
            @"/client/pauseav"
        )
    {}

    public override PauseOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, PauseInput payload)
    {
        // !HACK!
        EmuClientApi concreteApi =
            (EmuClientApi)api.EmuClient;
        BizHawk.Client.EmuHawk.MainForm mainForm =
            (BizHawk.Client.EmuHawk.MainForm)concreteApi
            .GetType()
            .GetField("_mainForm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);

        bool paused = mainForm.PauseAvi;

        if (payload.Set == true) {
            api.EmuClient.PauseAv();
        } else if (payload.Set == false) {
            api.EmuClient.UnpauseAv();
        }

        return new PauseOutput {
            Paused = paused
        };
    }
}
