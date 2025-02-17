namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReadRangeInput {
    [JsonProperty(Required = Required.Always)]
    public long Address { get; set; }
    [JsonProperty(Required = Required.Always)]
    public int Size { get; set; }
    public string? Domain { get; set; }
}

public class ReadRangeOutput {
    public byte[] Data { get; set; } = [];
    public string Domain { get; set; } = "";
}

public class ReadRangeCommand : EmulatorCommand<ReadRangeInput, ReadRangeOutput>
{
    public ReadRangeCommand():
        base(
            @"^/memory/readrange$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryRead;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow reading memory' to be enabled in the TASauria plugin security settings.";

    public override ReadRangeOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, ReadRangeInput payload)
    {
        string domain = payload.Domain ?? emulator.APIs.Memory.GetCurrentMemoryDomain();

        // Probably not necessary, but for good measure and consistency
        emulator.APIs.Memory.SetBigEndian(true);

        var bytes = emulator.APIs.Memory.ReadByteRange(payload.Address, payload.Size, domain);

        return new ReadRangeOutput {
            Data = [.. bytes],
            Domain = domain,
        };
    }
}
