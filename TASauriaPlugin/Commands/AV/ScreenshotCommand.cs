namespace ScarletCafe.TASauriaPlugin.Commands.AV;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;


public class ScreenshotInput {
    public bool? IncludeOSD { get; set; } = null;
}

public class ScreenshotOutput {
    public byte[] Data { get; set; } = [];
}

public class ScreenshotCommand : EmulatorCommand<ScreenshotInput, ScreenshotOutput>
{
    public ScreenshotCommand():
        base(
            @"^/av/screenshot$"
        )
    {}

    public override bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return GlobalState.configuration.SecurityAllowAVControl;
    }

    public override string SecurityRemarks { get; } = "This command requires 'Allow A/V control' to be enabled in the TASauria plugin security settings.";

    public override ScreenshotOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, ScreenshotInput payload)
    {
        // Store current setting
        var currentScreenshotSetting = emulator.Config.ScreenshotCaptureOsd;

        // Set based on input
        emulator.Config.ScreenshotCaptureOsd = payload.IncludeOSD ?? false;

        // Generate a temporary file to save to
        string fileName = System.IO.Path.GetTempFileName();

        // Save the screenshot to that file
        emulator.APIs.EmuClient.Screenshot(fileName);

        // Read the data from the file
        byte[] screenshotData = System.IO.File.ReadAllBytes(fileName);

        // Delete the file
        System.IO.File.Delete(fileName);

        // Restore setting
        emulator.Config.ScreenshotCaptureOsd = currentScreenshotSetting;

        return new ScreenshotOutput {
            Data = screenshotData
        };
    }
}
