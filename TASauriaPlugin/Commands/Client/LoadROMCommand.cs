namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class LoadROMInput {
    public byte[] Data { get; set; } = [];
}

public class LoadROMOutput {
    public bool Success { get; set; }
}

public class LoadROMCommand : EmulatorCommand<LoadROMInput, LoadROMOutput>
{
    public LoadROMCommand():
        base(
            @"^/client/loadrom$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowROMManagement;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow loading and closing ROMs' to be enabled in the TASauria plugin security settings.";

    public override LoadROMOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, LoadROMInput payload)
    {
        // !HACK!: We need to access MainForm and config to load the ROM without committing it to the recent ROMs.
        EmuClientApi concreteApi =
            (EmuClientApi)api.EmuClient;
        BizHawk.Client.EmuHawk.MainForm mainForm =
            (BizHawk.Client.EmuHawk.MainForm)concreteApi
            .GetType()
            .GetField("_mainForm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);
        Config config =
            (Config)mainForm
            .GetType()
            .GetProperty("Config", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(mainForm);

        // Generate a temporary file to load from
        string fileName = System.IO.Path.GetTempFileName();

        // Write the state provided to that file
        System.IO.File.WriteAllBytes(fileName, payload.Data);

        var success = mainForm.LoadRom(fileName, new LoadRomArgs(new OpenAdvanced_OpenRom(fileName)));

        config.RecentRoms.Remove(fileName);

        return new LoadROMOutput {
            Success = success,
        };
    }
}
