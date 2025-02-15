namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class ForkCommand : EmulatorCommand<NoArguments, GetOutput>
{
    public ForkCommand():
        base(
            @"^/movie/fork$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMovieManagement;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow movie management' to be enabled in the TASauria plugin security settings.";

    public override GetOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        byte[] movieData = [];

        string? moviePath = emulator.IMovieSession.Movie?.Filename;
        if (moviePath != null && moviePath.Length > 0 && System.IO.File.Exists(moviePath)) {
            // Generate a temporary file to fork to
            string newMoviePath = System.IO.Path.GetTempFileName();
            emulator.APIs.Movie.Save(newMoviePath);

            movieData = System.IO.File.ReadAllBytes(emulator.IMovieSession.Movie!.Filename);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
