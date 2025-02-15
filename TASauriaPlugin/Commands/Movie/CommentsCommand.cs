namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;

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

    public override CommentsOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        var comments = emulator.IMovieSession.Movie?.Comments;

        return new CommentsOutput {
            Comments = comments != null ? [ .. comments ] : []
        };
    }
}
