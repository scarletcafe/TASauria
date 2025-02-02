namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class ReadDomainInput {
    public string? Domain { get; set; }
}

public class ReadDomainCommand : EmulatorCommand<ReadDomainInput, ReadRangeOutput>
{
    public ReadDomainCommand():
        base(
            @"/memory/readdomain"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMemoryRead;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow reading memory' to be enabled in the TASauria plugin security settings.";

    public override ReadRangeOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, ReadDomainInput payload)
    {
        string domain = payload.Domain ?? api.Memory.GetCurrentMemoryDomain();

        // Probably not necessary, but for good measure and consistency
        api.Memory.SetBigEndian(true);

        // Get memory domain size
        long size = api.Memory.GetMemoryDomainSize(domain);

        var bytes = api.Memory.ReadByteRange(0, (int)size, domain);

        return new ReadRangeOutput {
            Data = [.. bytes],
            Domain = domain,
        };
    }
}
