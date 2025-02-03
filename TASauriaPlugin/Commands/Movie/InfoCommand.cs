namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class InfoOutput {
    public bool Loaded { get; set; }
    public string Mode { get; set; } = "";
    public bool ReadOnly { get; set; }
    public string FileName { get; set; } = "";
    public int Length { get; set; }
    public double FrameRate { get; set; }
    public ulong Rerecords { get; set; }
}

public class InfoCommand : EmulatorCommand<NoArguments, InfoOutput>
{
    public InfoCommand():
        base(
            @"^/movie/info$"
        )
    {}

    public override InfoOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
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

        // We don't show the full path. Only the actual file name.
        string moviePath = movieSession.Movie?.Filename ?? "";
        if (moviePath.Length > 0) {
            moviePath = System.IO.Path.GetFileName(moviePath);
        }

        return new InfoOutput {
            Loaded = movieSession.Movie?.IsActive() ?? false,
            Mode = (movieSession.Movie?.Mode ?? MovieMode.Inactive).ToString(),
            ReadOnly = movieSession.ReadOnly,
            FileName = moviePath,
            Length = movieSession.Movie?.FrameCount ?? 0,
            FrameRate = api.Movie.GetFps(),
            Rerecords = movieSession.Movie?.Rerecords ?? 0,
        };
    }
}
