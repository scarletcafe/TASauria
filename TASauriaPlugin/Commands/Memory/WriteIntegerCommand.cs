namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System;
using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WriteIntegerInput {
    [JsonProperty(Required = Required.Always)]
    public int Address { get; set; }
    [JsonProperty(Required = Required.Always)]
    public int Size { get; set; }
    public bool Signed { get; set; } = true;
    public bool Little { get; set; } = false;
    [JsonProperty(Required = Required.Always)]
    public long Data { get; set; }
    public string? Domain { get; set; }
}

public class WriteIntegerCommand : EmulatorCommand<WriteIntegerInput, ReadIntegerOutput>
{
    public WriteIntegerCommand():
        base(
            @"/memory/writeinteger"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryWrite;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow writing memory' to be enabled in the TASauria plugin security settings.";

    public override ReadIntegerOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, WriteIntegerInput payload)
    {
        long value = 0;
        string domain = payload.Domain ?? api.Memory.GetCurrentMemoryDomain();

        api.Memory.SetBigEndian(!payload.Little);

        switch (payload.Size) {
            case 0:
                break;
            case 1:
                if (payload.Signed) {
                    value = api.Memory.ReadS8(payload.Address, domain);
                    api.Memory.WriteS8(payload.Address, (int)payload.Data, domain);
                } else {
                    value = api.Memory.ReadU8(payload.Address, domain);
                    api.Memory.WriteU8(payload.Address, (uint)payload.Data, domain);
                }
                break;
            case 2:
                if (payload.Signed) {
                    value = api.Memory.ReadS16(payload.Address, domain);
                    api.Memory.WriteS16(payload.Address, (int)payload.Data, domain);
                } else {
                    value = api.Memory.ReadU16(payload.Address, domain);
                    api.Memory.WriteU16(payload.Address, (uint)payload.Data, domain);
                }
                break;
            case 3:
                if (payload.Signed) {
                    value = api.Memory.ReadS24(payload.Address, domain);
                    api.Memory.WriteS24(payload.Address, (int)payload.Data, domain);
                } else {
                    value = api.Memory.ReadU24(payload.Address, domain);
                    api.Memory.WriteU24(payload.Address, (uint)payload.Data, domain);
                }
                break;
            case 4:
                if (payload.Signed) {
                    value = api.Memory.ReadS32(payload.Address, domain);
                    api.Memory.WriteS32(payload.Address, (int)payload.Data, domain);
                } else {
                    value = api.Memory.ReadU32(payload.Address, domain);
                    api.Memory.WriteU32(payload.Address, (uint)payload.Data, domain);
                }
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
