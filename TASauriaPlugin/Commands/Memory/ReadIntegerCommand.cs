namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReadIntegerInput {
    [JsonProperty(Required = Required.Always)]
    public long Address { get; set; }
    [JsonProperty(Required = Required.Always)]
    public int Size { get; set; }
    public bool Signed { get; set; } = true;
    public bool Little { get; set; } = false;
    public string? Domain { get; set; }
}

public class ReadIntegerOutput {
    public long Data { get; set; }
    public string Domain { get; set; } = "";
}

public class ReadIntegerCommand : EmulatorCommand<ReadIntegerInput, ReadIntegerOutput>
{
    public ReadIntegerCommand():
        base(
            @"^/memory/readinteger$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryRead;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow reading memory' to be enabled in the TASauria plugin security settings.";

    public override ReadIntegerOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, ReadIntegerInput payload)
    {
        long value = 0;
        string domain = payload.Domain ?? emulator.APIs.Memory.GetCurrentMemoryDomain();

        emulator.APIs.Memory.SetBigEndian(!payload.Little);

        switch (payload.Size) {
            case 0:
                break;
            case 1:
                value = payload.Signed ?
                    emulator.APIs.Memory.ReadS8(payload.Address, domain) :
                    emulator.APIs.Memory.ReadU8(payload.Address, domain);
                break;
            case 2:
                value = payload.Signed ?
                    emulator.APIs.Memory.ReadS16(payload.Address, domain) :
                    emulator.APIs.Memory.ReadU16(payload.Address, domain);
                break;
            case 3:
                value = payload.Signed ?
                    emulator.APIs.Memory.ReadS24(payload.Address, domain) :
                    emulator.APIs.Memory.ReadU24(payload.Address, domain);
                break;
            case 4:
                value = payload.Signed ?
                    emulator.APIs.Memory.ReadS32(payload.Address, domain) :
                    emulator.APIs.Memory.ReadU32(payload.Address, domain);
                break;
            default:
                throw new ArgumentOutOfRangeException("Integer size must be between 1-4");
        }

        return new ReadIntegerOutput {
            Data = value,
            Domain = domain,
        };
    }
}
