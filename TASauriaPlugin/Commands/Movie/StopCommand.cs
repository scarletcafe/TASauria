namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;


public class StopInput {
    public bool Save { get; set; } = true;
}

public class StopCommand : EmulatorCommand<StopInput, GetOutput>
{
    public StopCommand():
        base(
            @"^/movie/stop$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMovieManagement;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow movie management' to be enabled in the TASauria plugin security settings.";

    public override GetOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, StopInput payload)
    {

        byte[] movieData = [];

        string? moviePath = emulator.IMovieSession.Movie?.Filename;

        emulator.APIs.Movie.Stop(payload.Save);

        if (moviePath != null && moviePath.Length > 0 && System.IO.File.Exists(moviePath)) {
            movieData = System.IO.File.ReadAllBytes(moviePath);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
