namespace ScarletCafe.TASauriaPlugin.Commands.Savestate;

using System.Collections.Generic;
using BizHawk.Client.Common;
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

    public override LoadSlotOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, LoadSlotInput payload)
    {
        var success = api.SaveState.LoadSlot(payload.Slot, payload.SuppressOSD);

        return new LoadSlotOutput {
            Success = success
        };
    }
}
