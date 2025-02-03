namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using System.IO;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using Newtonsoft.Json.Linq;


public class NewInput {
    public bool StartFromSaveState { get; set; }
    public bool StartFromSaveRAM { get; set; }
    public string? Author { get; set; }
}

public class NewOutput {
    public string Author { get; set; } = "";
}


public class NewCommand : EmulatorCommand<NewInput, NewOutput>
{
    public NewCommand():
        base(
            @"^/movie/new$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowMovieManagement;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow movie management' to be enabled in the TASauria plugin security settings.";

    public override NewOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NewInput payload)
    {
        // !HACK!: Accessing the config allows us to read the default author. See Client/SpeedCommand.cs for why
        // we access the config in this way.
        EmuClientApi concreteEmuClientApi =
            (EmuClientApi)api.EmuClient;
        BizHawk.Client.EmuHawk.MainForm mainForm =
            (BizHawk.Client.EmuHawk.MainForm)concreteEmuClientApi
            .GetType()
            .GetField("_mainForm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteEmuClientApi);
        Config config =
            (Config)mainForm
            .GetType()
            .GetProperty("Config", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(mainForm);

        // !HACK!: IMovieApi doesn't expose the API for creating new movies.
        MovieApi concreteMovieApi =
            (MovieApi)api.Movie;
        IMovieSession movieSession =
            (IMovieSession)concreteMovieApi
            .GetType()
            .GetField("_movieSession", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteMovieApi);

        // Create a new path for the movie to reside in.
        string newMoviePath = System.IO.Path.GetTempFileName();

        // Set up and create the movie.
        // Emulates the behavior of BizHawk.Client.EmuHawk.RecordMovie.Ok_Click
        var movieToRecord = movieSession.Get(newMoviePath);
        movieToRecord.Author = payload.Author ?? config.DefaultAuthor;

        if (payload.StartFromSaveState && mainForm.Emulator.HasSavestates()) {
            var core = mainForm.Emulator.AsStatable();

            movieToRecord.StartsFromSavestate = true;

            if (config.Savestates.Type == SaveStateType.Binary)
            {
                movieToRecord.BinarySavestate = core.CloneSavestate();
            }
            else
            {
                using var sw = new StringWriter();
                core.SaveStateText(sw);
                movieToRecord.TextSavestate = sw.ToString();
            }

            movieToRecord.SavestateFramebuffer = [];
            if (mainForm.Emulator.HasVideoProvider())
            {
                movieToRecord.SavestateFramebuffer = mainForm.Emulator.AsVideoProvider().GetVideoBufferCopy();
            }
        } else if (payload.StartFromSaveRAM && mainForm.Emulator.HasSaveRam()) {
            var core = mainForm.Emulator.AsSaveRam();
            movieToRecord.StartsFromSaveRam = true;
            movieToRecord.SaveRam = core.CloneSaveRam();
        }

        mainForm.StartNewMovie(movieToRecord, true);

        return new NewOutput {
            Author = movieToRecord.Author,
        };
    }
}
