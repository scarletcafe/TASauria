namespace ScarletCafe.TASauriaPlugin;

using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;

public static class GlobalState {
    static GlobalState() {
        // This initialisation should always occur on the main thread (thread ID 1)
        Logging.Log(
            "Global state initializing on thread ID {0}",
            Thread.CurrentThread.ManagedThreadId
        );

        if (Thread.CurrentThread.ManagedThreadId != 1) {
            Logging.Log("Warning: Initialization was not done on the main thread. This may cause unexpected behaviour.");
        }

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
        // without modification, we're therefore going to use Harmony to patch extra function code onto the end of
        // `GeneralUpdateActiveExtTools`. It is this call that is responsible for deciding what tools are active in the
        // first place, so hooking into it guarantees our code runs always, on the main thread, around the same point
        // a tool would usually receive its general update anyway.
        var harmony = new Harmony("ScarletCafe.TASauriaPlugin.Harmony");

        var originalMethod = AccessTools.Method(
            typeof(BizHawk.Client.EmuHawk.ToolManager),
            nameof(BizHawk.Client.EmuHawk.ToolManager.GeneralUpdateActiveExtTools)
        );

        var methodPostfix = SymbolExtensions.GetMethodInfo(
            () => GeneralUpdateBackground()
        );

        harmony.Patch(originalMethod, null, new HarmonyMethod(methodPostfix));

    }

    [ModuleInitializer]
    internal static void InitializeModule()
    {
        // This function does nothing, however, its existence forces the class to be loaded on assembly startup,
        // instead of being deferred until the symbol is first referenced (such as when the tool window is opened).
        // Because EmuHawk only loads the DLL as part of its effort to detect external tools, by the time we hit
        // this, the client should already be (at least partially) initialized, but we should still be careful
        // to not disturb it too much.
    }

    public static void GeneralUpdateBackground() {
        Logging.Log("Background GeneralUpdate");
    }

}
