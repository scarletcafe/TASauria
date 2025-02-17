namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WriteFloatInput {
    [JsonProperty(Required = Required.Always)]
    public long Address { get; set; }
    public bool Little { get; set; } = false;
    [JsonProperty(Required = Required.Always)]
    public double Data { get; set; }
    public string? Domain { get; set; }
}

public class WriteFloatCommand : EmulatorCommand<WriteFloatInput, ReadFloatOutput>
{
    public WriteFloatCommand():
        base(
            @"^/memory/writefloat$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryWrite;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow writing memory' to be enabled in the TASauria plugin security settings.";

    public override ReadFloatOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, WriteFloatInput payload)
    {
        string domain = payload.Domain ?? emulator.APIs.Memory.GetCurrentMemoryDomain();

        emulator.APIs.Memory.SetBigEndian(!payload.Little);

        float value = emulator.APIs.Memory.ReadFloat(payload.Address, domain);
        emulator.APIs.Memory.WriteFloat(payload.Address, (float)payload.Data, domain);

        return new ReadFloatOutput {
            Data = value,
            Domain = domain,
        };
    }
}
