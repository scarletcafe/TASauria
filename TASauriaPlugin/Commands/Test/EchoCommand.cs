namespace ScarletCafe.TASauriaPlugin.Commands.Test;

using System.Collections.Generic;
using BizHawk.Common.CollectionExtensions;

public class EchoInput {
    public string Input { get; set; } = "";
}

public class EchoOutput {
    public string? PathMessage { get; set; } = "";
    public string PayloadMessage { get; set; } = "";
}

public class EchoCommand : Command<EchoInput, EchoOutput>
{
    public EchoCommand():
        base(
            @"/test/echo(?:/(?<PathContent>\w+))?"
        )
    {}

    public override EchoOutput Run(Dictionary<string, string> arguments, EchoInput payload)
    {
        return new EchoOutput {
            PathMessage = arguments.GetValueOrDefault("PathContent"),
            PayloadMessage = payload.Input,
        };
    }
}
