namespace ScarletCafe.TASauriaPlugin.Commands.Test;

using System;
using System.Collections.Generic;

public class WaitInput {
    public int Time { get; set; } = 0;
}

public class WaitOutput {
    public DateTime TimeStarted { get; set; }
    public DateTime TimeStopped { get; set; }
}

public class WaitCommand : Command<WaitInput, WaitOutput>
{
    public WaitCommand():
        base(
            @"/test/wait"
        )
    {}

    public override WaitOutput Run(Dictionary<string, string> arguments, WaitInput payload)
    {
        DateTime startTime = DateTime.UtcNow;

        System.Threading.Thread.Sleep(payload.Time);

        DateTime endTime = DateTime.UtcNow;

        return new WaitOutput {
            TimeStarted = startTime,
            TimeStopped = endTime,
        };
    }
}
