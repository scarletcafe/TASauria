namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if BIZHAWK_VERSION_PRE_2_9_X
using System.Linq;
#endif

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

    public override ReadRangeOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, WriteRangeInput payload)
    {
        string domain = payload.Domain ?? emulator.APIs.Memory.GetCurrentMemoryDomain();

        // Probably not necessary, but for good measure and consistency
        emulator.APIs.Memory.SetBigEndian(true);

        var bytes = emulator.APIs.Memory.ReadByteRange(payload.Address, payload.Data.Length, domain);
#if BIZHAWK_VERSION_PRE_2_9_X
        emulator.APIs.Memory.WriteByteRange(payload.Address, payload.Data.ToList(), domain);
#else
        emulator.APIs.Memory.WriteByteRange(payload.Address, payload.Data, domain);
#endif

        return new ReadRangeOutput {
            Data = [.. bytes],
            Domain = domain,
        };
    }
}
