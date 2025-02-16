namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using System.Linq;

public class GameOutput {
    public bool Loaded { get; set; }
    public string? Name { get; set; }
    public string? System { get; set; }
    public string? BoardType { get; set; }
    public string? Region { get; set; }
    public string? DisplayType { get; set; }
    public string? Hash { get; set; }
    public bool? InDatabase { get; set; }
    public string? DatabaseStatus { get; set; }
    public bool? DatabaseStatusBad { get; set; }
    public Dictionary<string, string?> GameOptions { get; set; } = [];
}

public class GameCommand : EmulatorCommand<NoArguments, GameOutput>
{
    public GameCommand():
        base(
            @"^/client/game$"
        )
    {}

    public override GameOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        var gameInfo = emulator.GetGameInfo();
        var gameOptions = emulator.GetGameOptions();

        return new GameOutput {
            Loaded = gameInfo != null && gameInfo.System != "NULL",
            Name = gameInfo?.Name,
            System = gameInfo?.System,
            BoardType = emulator.APIs.Emulation.GetBoardName(),
            Region = gameInfo?.Region,
            DisplayType = emulator.APIs.Emulation.GetDisplayType(),
            Hash = gameInfo?.Hash,
            InDatabase = !gameInfo?.NotInDatabase,
            DatabaseStatus = gameInfo?.Status.ToString(),
            DatabaseStatusBad = (
                gameInfo?.Status == BizHawk.Emulation.Common.RomStatus.BadDump ||
                gameInfo?.Status == BizHawk.Emulation.Common.RomStatus.Overdump
            ),
            GameOptions = gameOptions.ToDictionary(x => x.Key, x => x.Value),
        };
    }
}
