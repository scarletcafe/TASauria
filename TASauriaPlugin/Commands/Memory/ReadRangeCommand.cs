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
        var bytes = api.Memory.ReadByteRange(
            payload.Address, payload.Size, payload.Domain ?? api.Memory.GetCurrentMemoryDomain()
        );

        return new ReadRangeOutput {
            Data = [.. bytes]
        };
    }
}
