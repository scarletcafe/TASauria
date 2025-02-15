namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;

public class TurboOutput {
    public bool Turboing { get; set; }
}

public class TurboCommand : EmulatorCommand<NoArguments, TurboOutput>
{
    public TurboCommand():
        base(
            @"^/client/turbo$"
        )
    {}

    public override TurboOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new TurboOutput {
            Turboing = emulator.APIs.EmuClient.IsTurbo(),
        };
    }
}
