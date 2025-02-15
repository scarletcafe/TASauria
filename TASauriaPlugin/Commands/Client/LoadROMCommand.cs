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

    public override LoadROMOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, LoadROMInput payload)
    {
        // Generate a temporary file to load from
        string fileName = System.IO.Path.GetTempFileName();

        // Write the state provided to that file
        System.IO.File.WriteAllBytes(fileName, payload.Data);

        var success = emulator.MainForm.LoadRom(fileName, new LoadRomArgs { OpenAdvanced = new OpenAdvanced_OpenRom { Path = fileName } });

        emulator.Config.RecentRoms.Remove(fileName);

        return new LoadROMOutput {
            Success = success,
        };
    }
}
