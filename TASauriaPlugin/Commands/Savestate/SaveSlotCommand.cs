namespace ScarletCafe.TASauriaPlugin.Commands.Savestate;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class SaveSlotInput {
    [JsonProperty(Required = Required.Always)]
    public int Slot { get; set; }
    public bool SuppressOSD { get; set; }
}

public class SaveSlotOutput {
    public bool Success { get; set; }
}

public class SaveSlotCommand : EmulatorCommand<SaveSlotInput, SaveSlotOutput>
{
    public SaveSlotCommand():
        base(
            @"^/savestate/saveslot$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowSavestate;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow saving and loading save states' to be enabled in the TASauria plugin security settings.";

    public override SaveSlotOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, SaveSlotInput payload)
    {
        api.SaveState.SaveSlot(payload.Slot, payload.SuppressOSD);

        return new SaveSlotOutput {
            Success = true
        };
    }
}
