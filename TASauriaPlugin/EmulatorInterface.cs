namespace ScarletCafe.TASauriaPlugin;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;

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
}
