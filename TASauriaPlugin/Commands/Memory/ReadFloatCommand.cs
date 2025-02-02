namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class ReadFloatInput {
    public int Address { get; set; }
    public bool Little { get; set; } = false;
    public string? Domain { get; set; }
}

public class ReadFloatOutput {
    public double Data { get; set; }
    public string Domain { get; set; } = "";
}

public class ReadFloatCommand : EmulatorCommand<ReadFloatInput, ReadFloatOutput>
{
    public ReadFloatCommand():
        base(
            @"/memory/readfloat"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryRead;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow reading memory' to be enabled in the TASauria plugin security settings.";

    public override ReadFloatOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, ReadFloatInput payload)
    {
        string domain = payload.Domain ?? api.Memory.GetCurrentMemoryDomain();

        api.Memory.SetBigEndian(!payload.Little);

        float value = api.Memory.ReadFloat(payload.Address, domain);

        return new ReadFloatOutput {
            Data = value,
            Domain = domain,
        };
    }
}
