namespace ScarletCafe.TASauriaPlugin.Commands.Test;

using System.Collections.Generic;

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
            @"^/test/echo(?:/(?<PathContent>\w+))?$"
        )
    {}

    public override EchoOutput Run(Dictionary<string, string> arguments, EchoInput payload)
    {
        string pathContent = "";
        if (arguments.TryGetValue("PathContent", out string content))
        {
            pathContent = content;
        }

        return new EchoOutput {
            PathMessage = pathContent,
            PayloadMessage = payload.Input,
        };
    }
}
