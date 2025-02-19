namespace ScarletCafe.TASauriaPlugin;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

public class LocalizationFile {
    public static string[] AVAILABLE_LANGUAGES = [
        "en", "ja"
    ];

    public static Dictionary<string, LocalizationFile> AVAILABLE_LANGUAGE_FILES =
        AVAILABLE_LANGUAGES
        .Select((identifier) => {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"TASauriaPlugin.Resources.Localization.{identifier}.i18n.json");
            using StreamReader reader = new(stream);
            return (identifier, JsonConvert.DeserializeObject<LocalizationFile>(reader.ReadToEnd())!);
        })
        .ToDictionary(pair => pair.identifier, pair => pair.Item2);

    public string MetaLanguageName { get; set; } = "";

    public string UIServerTab { get; set; } = "";
    public string UISecurityTab { get; set; } = "";

    public string UIServerHost { get; set; } = "";
    public string UIServerPort { get; set; } = "";
    public string UIServerStart { get; set; } = "";
    public string UIServerStop { get; set; } = "";
    public string UIServerStartAuto { get; set; } = "";

    public string UIHostLoopbackIPv4 { get; set; } = "";
    public string UIHostLoopbackIPv6 { get; set; } = "";
    public string UIHostAllIPv4 { get; set; } = "";
    public string UIHostAllIPv6 { get; set; } = "";
    public string UIHostCustom { get; set; } = "";

    public string UISecAllowClientControl { get; set; } = "";
    public string UISecAllowJoypad { get; set; } = "";
    public string UISecAllowMemoryRead { get; set; } = "";
    public string UISecAllowMemoryWrite { get; set; } = "";
    public string UISecAllowSavestate { get; set; } = "";
    public string UISecAllowROMLoad { get; set; } = "";
    public string UISecAllowMovie { get; set; } = "";
    public string UISecAllowAVControl { get; set; } = "";

    public string UISaveAsDefault { get; set; } = "";

    public string UIStatusNotInitialized { get; set; } = "";
    public string UIStatusSavedSettings { get; set; } = "";
    public string UIStatusServerStopped { get; set; } = "";
    public string UIStatusServerRunning { get; set; } = "";

    public string UICloseTitle { get; set; } = "";
    public string UICloseDescription { get; set; } = "";
}
