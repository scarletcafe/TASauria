namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WriteRangeInput {
    [JsonProperty(Required = Required.Always)]
    public int Address { get; set; }
    [JsonProperty(Required = Required.Always)]
    public byte[] Data { get; set; } = [];
    public string? Domain { get; set; }
}

public class WriteRangeCommand : EmulatorCommand<WriteRangeInput, ReadRangeOutput>
{
    public WriteRangeCommand():
        base(
            @"^/memory/writerange$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryWrite;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow writing memory' to be enabled in the TASauria plugin security settings.";

    public override ReadRangeOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, WriteRangeInput payload)
    {
        string domain = payload.Domain ?? api.Memory.GetCurrentMemoryDomain();

        // Probably not necessary, but for good measure and consistency
        api.Memory.SetBigEndian(true);

        var bytes = api.Memory.ReadByteRange(payload.Address, payload.Data.Length, domain);
        api.Memory.WriteByteRange(payload.Address, payload.Data, domain);

        return new ReadRangeOutput {
            Data = [.. bytes],
            Domain = domain,
        };
    }
}
