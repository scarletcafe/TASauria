namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;

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

    public override GetOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        byte[] movieData = [];

        string? moviePath = emulator.IMovieSession.Movie?.Filename;
        if (moviePath != null && moviePath.Length > 0 && System.IO.File.Exists(moviePath)) {
            movieData = System.IO.File.ReadAllBytes(moviePath);
        }

        return new GetOutput {
            Data = movieData
        };
    }
}
