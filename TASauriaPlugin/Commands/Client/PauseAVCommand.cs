namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class PauseAVCommand : EmulatorCommand<PauseInput, PauseOutput>
{
    public PauseAVCommand():
        base(
            @"/client/pauseav"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowClientControl || input?.GetValue("set")?.Type == JTokenType.None;
    }

    public override string SecurityRemarks { get; } = "To set pause state, this command requires 'Allow client control' to be enabled in the TASauria plugin security settings.";

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
