namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;

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
