namespace ScarletCafe.TASauriaPlugin;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Common.PathExtensions;


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

    /// <summary>
    /// This is a queue of events that should be run in GeneralUpdate.
    /// These get executed on the main thread between frames, making it safe to access the GUI and core.
    /// As many updates as possible will be executed before allowing execution to continue, so it's important
    /// that actions pushed to this queue do not take too long else it will lock up the emulator.
    /// </summary>
    public static ConcurrentQueue<Action<ApiContainer>> generalUpdateQueue = new();

    public static void GeneralUpdate(ApiContainer container)
    {
        while (generalUpdateQueue.TryDequeue(out Action<ApiContainer> action)) {
            try {
                action.Invoke(container);
            } catch (Exception exception) {
                Logging.Log($"Action in general update queue caused exception: {exception}");
            }
        }
    }
}
