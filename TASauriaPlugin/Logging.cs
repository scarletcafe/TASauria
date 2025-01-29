namespace ScarletCafe.TASauriaPlugin;

using System;

public static class Logging {
    public static void Log(string text, params object[] formatParameters) {
        Console.WriteLine(string.Format(
            "[TASauria {0:yyyy-MM-dd HH:mm:ss.ff}] {1}",
            DateTime.Now,
            string.Format(text, formatParameters)
        ));
    }

}
