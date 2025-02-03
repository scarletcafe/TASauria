namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class GetOutput {
    public byte[] Data { get; set; } = [];
}

public class GetCommand : EmulatorCommand<NoArguments, GetOutput>
{
    public GetCommand():
        base(
            @"^/movie/get$"
        )
    {}

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
            movieData = System.IO.File.ReadAllBytes(moviePath);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
