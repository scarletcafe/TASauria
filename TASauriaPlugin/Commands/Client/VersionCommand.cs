namespace ScarletCafe.TASauriaPlugin.Commands.Client;

using System.Collections.Generic;
using BizHawk.Common;

public class VersionOutput {
    public string StableVersion { get; set; } = "";
    public string ReleaseDate { get; set; } = "";
    public string GitBranch { get; set; } = "";
    public string GitHash { get; set; } = "";
    public string GitRevision { get; set; } = "";
    public bool IsDevelopmentVersion { get; set; }
    public string? CustomBuildString { get; set; } = "";
}

public class VersionCommand : Command<NoArguments, VersionOutput>
{
    public VersionCommand():
        base(
            @"^/client/version$"
        )
    {}

    public override VersionOutput Run(Dictionary<string, string> arguments, NoArguments payload)
    {
        return new VersionOutput {
            StableVersion = VersionInfo.MainVersion,
            ReleaseDate = VersionInfo.ReleaseDate,
            GitBranch = VersionInfo.GIT_BRANCH,
            GitHash = VersionInfo.GIT_SHORTHASH,
            GitRevision = VersionInfo.SVN_REV,
            IsDevelopmentVersion = VersionInfo.DeveloperBuild,
            CustomBuildString = VersionInfo.CustomBuildString ?? null,
        };
    }
}
