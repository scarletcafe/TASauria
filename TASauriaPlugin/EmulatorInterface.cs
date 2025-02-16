namespace ScarletCafe.TASauriaPlugin;

using System.Collections.Generic;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;

public class EmulatorInterface {
    public ApiContainer APIs { get; private set; }
    public IMainFormForTools IMainForm { get; private set; }
    public MainForm MainForm { get; private set; }
    public Config Config { get; private set; }
    public IMovieSession IMovieSession { get; private set; }

    public EmulatorInterface(
        ApiContainer apis,
        IMainFormForTools iMainForm,
        Config config
    ) {
        APIs = apis;
        IMainForm = iMainForm;
        // !HACK!: casting interface to explicit class
        MainForm = (MainForm)iMainForm;
        Config = config;

        // !HACK!: IMovieSession not directly exposed
        MovieApi concreteApi =
            (MovieApi)apis.Movie;
        IMovieSession =
            (IMovieSession)concreteApi
            .GetType()
            .GetField("_movieSession", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);
    }

    public IGameInfo? GetGameInfo() {
#if BIZHAWK_VERSION_PRE_2_9_X
        EmulationApi concreteApi =
            (EmulationApi)APIs.Emulation;
        IGameInfo gameInfo =
            (IGameInfo)concreteApi
            .GetType()
            .GetField("_game", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(concreteApi);
        return gameInfo;
#else
        return APIs.Emulation.GetGameInfo();
#endif
    }

    public IReadOnlyDictionary<string, string?> GetGameOptions() {
#if BIZHAWK_VERSION_PRE_2_9_X
        IGameInfo? gameInfo = GetGameInfo();

        var dictionary = new Dictionary<string, string?>();

        if (gameInfo != null) {
            foreach (var pair in ((GameInfo) gameInfo).GetOptions()) {
                dictionary.Add(pair.Key, pair.Value);
            }
        }

        return dictionary;
#else
        return APIs.Emulation.GetGameOptions();
#endif
    }
}
