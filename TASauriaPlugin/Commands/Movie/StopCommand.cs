namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;
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

    public override GetOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, StopInput payload)
    {
        // !HACK!: IMovieApi raises NullReferenceException internally when using some of the API for movies.
        // It's easier to circumvent this by accessing the content directly.
        MovieApi concreteApi =
            (MovieApi)api.Movie;
        IMovieSession movieSession =
            (IMovieSession)concreteApi
            .GetType()
            .GetField("_movieSession", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);

        byte[] movieData = [];

        string? moviePath = movieSession.Movie?.Filename;

        api.Movie.Stop(payload.Save);

        if (moviePath != null && moviePath.Length > 0 && System.IO.File.Exists(moviePath)) {
            movieData = System.IO.File.ReadAllBytes(moviePath);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
