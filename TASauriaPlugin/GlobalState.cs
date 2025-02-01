namespace ScarletCafe.TASauriaPlugin;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Common.PathExtensions;

#if TASAURIA_BACKGROUND_EXECUTION
using HarmonyLib;
#endif

public static class GlobalState {
    public static Configuration configuration;
    public static Server? server;

    public static string ConfigLocation {
        get {
            return Path.Combine(PathUtils.ExeDirectoryPath, "tasauria.ini");
        }
    }

    static GlobalState() {
        // This initialisation should always occur on the main thread (thread ID 1)
        Logging.Log(
            "Global state initializing on thread ID {0}",
            Thread.CurrentThread.ManagedThreadId
        );

        if (Thread.CurrentThread.ManagedThreadId != 1) {
            Logging.Log("Warning: Initialization was not done on the main thread. This may cause unexpected behaviour.");
        }

        // Load config
        configuration = ConfigService.Load<Configuration>(ConfigLocation);

#if TASAURIA_BACKGROUND_EXECUTION
        // In usual cases, external tools would be run by overriding UpdateValues or one of its called methods:
        // https://github.com/TASEmulators/BizHawk/blob/2.10/src/BizHawk.Client.EmuHawk/tools/ToolFormBase.cs#L52-L72
        // In particular, the one we care about in our case is ToolFormUpdateType.General because it is called
        // regardless of if the emulator is paused or not. The usual call stack would be:
        //  - EmuHawk.Program.Main
        //  - EmuHawk.Program.SubMain
        //  - EmuHawk.MainForm.ProgramRunLoop
        //  - EmuHawk.ToolManager.GeneralUpdateActiveExtTools
        //  - TASauriaPlugin.ExternalToolForm.UpdateValues
        // However, this system makes the assumption that a tool should only run when its form is open (the lifecycle
        // of a tool is strictly tied to the lifecycle of the window it produces). In most cases, like for game-specific
        // tools, this is OK, but in my case, especially if the user has chosen to run the server at startup, I want the
        // functionality to remain active even if the tool form is closed during operation, or if it was never opened in
        // the first place (if the user has chosen to have the server run at startup).
        // It is important, because cores are not thread-safe or even conventionally syncable, that we execute any
        // EmuHawk or core-related code in the same thread as is used to update the UI and advance the core. To run
        // without modification, we're therefore going to use Harmony to patch extra function code onto the start of
        // `GeneralUpdateActiveExtTools`. It is this call that is responsible for deciding what tools are active in the
        // first place, so hooking into it guarantees our code runs always, on the main thread, around the same point
        // a tool would usually receive its general update anyway.
        Logging.Log("Applying background update loop patch");
        var harmony = new Harmony("ScarletCafe.TASauriaPlugin.Harmony");

        var originalMethod = AccessTools.Method(
            typeof(ToolManager),
            nameof(ToolManager.GeneralUpdateActiveExtTools)
        );

        var methodPrefix = AccessTools.Method(
            typeof(GlobalState),
            nameof(BackgroundUpdateHook)
        );

        harmony.Patch(
            original: originalMethod,
            prefix: new HarmonyMethod(methodPrefix)
        );

        Logging.Log("Starting server in background");
        StartServer();

#endif
    }

    public static void SaveConfig() {
        ConfigService.Save(ConfigLocation, configuration);
    }

    public static void StartServer()
    {
        try
        {
            server = new Server(configuration);
        } catch (Exception exception)
        {
            MessageBox.Show($"The server could not start because an exception occurred:\n{exception}", "TASauria server failed to start", MessageBoxButtons.OK);
        }

    }

    public static void StopServer()
    {
        server?.Stop();
        server = null;
    }

    [ModuleInitializer]
    internal static void InitializeModule()
    {
        // This function does nothing, however, its existence forces the class to be loaded on assembly startup,
        // instead of being deferred until the symbol is first referenced (such as when the tool window is opened).
        // Because EmuHawk only loads the DLL as part of its effort to detect external tools, by the time we hit
        // this, the client should already be (at least partially) initialized, but we should still be careful
        // to not disturb it too much.
        Logging.Log("Module initialized");
    }

    /// <summary>
    /// This is a queue of events that should be run in GeneralUpdate.
    /// These get executed on the main thread between frames, making it safe to access the GUI and core.
    /// As many updates as possible will be executed before allowing execution to continue, so it's important
    /// that actions pushed to this queue do not take too long else it will lock up the emulator.
    /// </summary>
    public static ConcurrentQueue<Action<ApiContainer>> generalUpdateQueue = new();

    public static void GeneralUpdate(ApiContainer container, bool background)
    {
        // Fetch frame number
        Logging.Log("GeneralUpdate (background: {1}) on frame {0}", container.Emulation.FrameCount(), background);

        while (generalUpdateQueue.TryDequeue(out Action<ApiContainer> action)) {
            try {
                action.Invoke(container);
            } catch (Exception exception) {
                Logging.Log($"Action in general update queue caused exception: {exception}");
            }
        }
    }

    public static bool IsBackgroundHooked {
        get
        {
#if TASAURIA_BACKGROUND_EXECUTION
            return _hookAPIContainer != null;
#else
            return false;
#endif
        }
    }

#if TASAURIA_BACKGROUND_EXECUTION
    private static ApiContainer? _hookAPIContainer;
    private static int _hookAttemptCounter = 0;

    public static void BackgroundUpdateHook(ToolManager __instance) {
        // Attempt to retrieve API container
        if (_hookAttemptCounter < 5)
        {
            _hookAPIContainer = null;
            _hookAttemptCounter++;
            // Manually trigger the ApiProvider initializer and capture the collection
            var method = __instance.GetType().GetMethod("GetOrInitApiProvider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (method != null) {
                try
                {
                    IExternalApiProvider maybeProvider = (IExternalApiProvider)method.Invoke(__instance, []);
                    _hookAPIContainer = maybeProvider.Container;
                    _hookAttemptCounter = 0;
                } catch (Exception exception)
                {
                    Logging.Log("Failed to hook onto the API provider because an exception occurred: {}", exception);
                }
            } else
            {
                Logging.Log("Failed to hook onto the API provider because the method could not be located.");
            }

            if (_hookAttemptCounter >= 5)
            {
                Logging.Log("Failed to hook 5 times in a row. No further attempts to hook will be made (reverting to non-hook mode)");
            }
        }

        if (_hookAPIContainer != null)
        {
            GeneralUpdate(_hookAPIContainer!, true);
        }
    }
#endif
}
