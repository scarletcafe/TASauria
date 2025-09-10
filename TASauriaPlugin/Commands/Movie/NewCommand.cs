namespace ScarletCafe.TASauriaPlugin.Commands.Movie;

using System.Collections.Generic;
using System.IO;
#if BIZHAWK_VERSION_PRE_2_10_1
#else
using BizHawk.Bizware.Graphics;
#endif
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

    public override NewOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NewInput payload)
    {
        // Create a new path for the movie to reside in.
        string newMoviePath = Path.GetTempFileName();

        // Set up and create the movie.
        // Emulates the behavior of BizHawk.Client.EmuHawk.RecordMovie.Ok_Click
        var movieToRecord = emulator.IMovieSession.Get(newMoviePath);
        movieToRecord.Author = payload.Author ?? emulator.Config.DefaultAuthor;

        if (payload.StartFromSaveState && emulator.MainForm.Emulator.HasSavestates()) {
            var core = emulator.MainForm.Emulator.AsStatable();

            movieToRecord.StartsFromSavestate = true;

            if (emulator.Config.Savestates.Type == SaveStateType.Binary)
            {
                movieToRecord.BinarySavestate = core.CloneSavestate();
            }
            else
            {
                using var sw = new StringWriter();
                core.SaveStateText(sw);
                movieToRecord.TextSavestate = sw.ToString();
            }

#if BIZHAWK_VERSION_PRE_2_10_1
            movieToRecord.SavestateFramebuffer = [];
#endif
            if (emulator.MainForm.Emulator.HasVideoProvider())
            {
#if BIZHAWK_VERSION_PRE_2_10_1
                movieToRecord.SavestateFramebuffer = (int[])emulator.MainForm.Emulator.AsVideoProvider().GetVideoBuffer().Clone();
#else
                var videoProvider = ((IEmulator)emulator).AsVideoProvider();
                movieToRecord.SavestateFramebuffer = new BitmapBuffer(videoProvider.BufferWidth, videoProvider.BufferHeight, videoProvider.GetVideoBuffer());
#endif
            }
        } else if (payload.StartFromSaveRAM && emulator.MainForm.Emulator.HasSaveRam()) {
            var core = emulator.MainForm.Emulator.AsSaveRam();
            movieToRecord.StartsFromSaveRam = true;
            movieToRecord.SaveRam = core.CloneSaveRam();
        }

        emulator.MainForm.StartNewMovie(movieToRecord, true);

        return new NewOutput {
            Author = movieToRecord.Author,
        };
    }
}
