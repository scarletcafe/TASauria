namespace ScarletCafe.TASauriaPlugin.Commands.Savestate;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class SaveOutput {
    public byte[] Data { get; set; } = [];
}

public class SaveCommand : EmulatorCommand<NoArguments, SaveOutput>
{
    public SaveCommand():
        base(
            @"^/savestate/save$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowSavestate;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow saving and loading save states' to be enabled in the TASauria plugin security settings.";

    public override SaveOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        // Generate a temporary file to save to
        string fileName = System.IO.Path.GetTempFileName();

        // Save the state to that file
        api.SaveState.Save(fileName, true);

        // Read the data from the file
        byte[] saveStateData = System.IO.File.ReadAllBytes(fileName);

        // Delete the file
        System.IO.File.Delete(fileName);

        return new SaveOutput {
            Data = saveStateData
        };
    }
}
