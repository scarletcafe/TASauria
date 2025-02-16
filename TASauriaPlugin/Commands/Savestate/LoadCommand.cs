namespace ScarletCafe.TASauriaPlugin.Commands.Savestate;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class LoadInput {
    public byte[] Data { get; set; } = [];
}

public class LoadCommand : EmulatorCommand<LoadInput, LoadSlotOutput>
{
    public LoadCommand():
        base(
            @"^/savestate/load$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowSavestate;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow saving and loading save states' to be enabled in the TASauria plugin security settings.";

    public override LoadSlotOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, LoadInput payload)
    {
        // Generate a temporary file to load from
        string fileName = System.IO.Path.GetTempFileName();

        // Write the state provided to that file
        System.IO.File.WriteAllBytes(fileName, payload.Data);

        // Load the save state
#if BIZHAWK_VERSION_PRE_2_9_X
        emulator.APIs.SaveState.Load(fileName, true);
        var success = true;
#else
        var success = emulator.APIs.SaveState.Load(fileName, true);
#endif

        // Delete the file
        System.IO.File.Delete(fileName);

        return new LoadSlotOutput {
            Success = success
        };
    }
}
