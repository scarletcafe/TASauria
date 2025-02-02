namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class ReadRangeInput {
    public int Address { get; set; }
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
            @"/memory/readrange"
        )
    {}

    public override ReadRangeOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, ReadRangeInput payload)
    {
        string domain = payload.Domain ?? api.Memory.GetCurrentMemoryDomain();

        // Probably not necessary, but for good measure and consistency
        api.Memory.SetBigEndian(true);

        var bytes = api.Memory.ReadByteRange(payload.Address, payload.Size, domain);

        return new ReadRangeOutput {
            Data = [.. bytes],
            Domain = domain,
        };
    }
}
