namespace ScarletCafe.TASauriaPlugin.Commands.Savestate;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadSlotInput {
    [JsonProperty(Required = Required.Always)]
    public int Slot { get; set; }
    public bool SuppressOSD { get; set; }
}

public class LoadSlotOutput {
    public bool Success { get; set; }
}

public class LoadSlotCommand : EmulatorCommand<LoadSlotInput, LoadSlotOutput>
{
    public LoadSlotCommand():
        base(
            @"^/savestate/loadslot$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowSavestate;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow saving and loading save states' to be enabled in the TASauria plugin security settings.";

    public override LoadSlotOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, LoadSlotInput payload)
    {
#if BIZHAWK_VERSION_PRE_2_9_X
        emulator.APIs.SaveState.LoadSlot(payload.Slot, payload.SuppressOSD);
        var success = true;
#else
        var success = emulator.APIs.SaveState.LoadSlot(payload.Slot, payload.SuppressOSD);
#endif

        return new LoadSlotOutput {
            Success = success
        };
    }
}
