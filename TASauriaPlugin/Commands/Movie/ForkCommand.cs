namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;
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

    public override GetOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
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
        if (moviePath != null && moviePath.Length > 0 && System.IO.File.Exists(moviePath)) {
            // Generate a temporary file to fork to
            string newMoviePath = System.IO.Path.GetTempFileName();
            api.Movie.Save(newMoviePath);

            movieData = System.IO.File.ReadAllBytes(movieSession.Movie!.Filename);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
