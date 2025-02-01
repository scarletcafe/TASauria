namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Client.Common;


public class BoardNameOutput {
    public string BoardName { get; set; } = "";
}

public class BoardNameCommand : EmulatorCommand<NoArguments, BoardNameOutput>
{
    public BoardNameCommand():
        base(
            @"/client/boardname"
        )
    {}

    public override BoardNameOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        return new BoardNameOutput {
            BoardName = api.Emulation.GetBoardName(),
        };
    }
}
