namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using BizHawk.Client.Common;

public class CommentsOutput {
    public string[] Comments { get; set; } = [];
}

public class CommentsCommand : EmulatorCommand<NoArguments, CommentsOutput>
{
    public CommentsCommand():
        base(
            @"^/movie/comments$"
        )
    {}

    public override CommentsOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
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

        var comments = movieSession.Movie?.Comments;

        return new CommentsOutput {
            Comments = comments != null ? [ .. comments ] : []
        };
    }
}
