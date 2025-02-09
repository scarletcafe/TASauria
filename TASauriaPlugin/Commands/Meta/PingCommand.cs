namespace ScarletCafe.TASauriaPlugin.Commands.Meta;

using System.Collections.Generic;

public class PingOutput {
    public bool Pong { get; set; }
}

public class PingCommand : Command<NoArguments, PingOutput>
{
    public PingCommand():
        base(
            @"^/meta/ping$"
        )
    {}

    public override PingOutput Run(Dictionary<string, string> arguments, NoArguments payload)
    {
        return new PingOutput {
            Pong = true,
        };
    }
}
