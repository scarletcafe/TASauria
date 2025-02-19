namespace ScarletCafe.TASauriaPlugin;

public class Configuration {

    public string LanguageSelected { get; set; } = "en";

    public string ServerHost { get; set; } = "127.0.0.1";
    public int ServerPort { get; set; } = 20251;
    public bool ServerStartAutomatically { get; set; } = false;

    public bool SecurityAllowClientControl { get; set; } = true;
    public bool SecurityAllowJoypadSystemInput { get; set; } = true;
    public bool SecurityAllowMemoryRead { get; set; } = true;
    public bool SecurityAllowMemoryWrite { get; set; } = false;
    public bool SecurityAllowSavestate { get; set; } = false;
    public bool SecurityAllowMovieManagement { get; set; } = false;
    public bool SecurityAllowAVControl { get; set; } = false;
    public bool SecurityAllowROMManagement { get; set; } = false;

}
