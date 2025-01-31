namespace ScarletCafe.TASauriaPlugin;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;

[ExternalTool("TASauria", Description = "")]
public sealed partial class ExternalToolForm : ToolFormBase, IExternalToolForm {
    protected override string WindowTitleStatic
        => "TASauria";

    #region APIs
    // Requesting these three APIs specifically somehow invokes some black magic that makes
    // EmuHawk index the assembly on launch, allowing the server to be booted without user
    // interaction in the Background Hook build of TASauria.
    // Removing any of these seems to stop the assembly from being loaded at launch, I'm honestly
    // not totally sure why.
    [OptionalApi]
    public IEmuClientApi? EmuClientAPI { get; set; }
    [OptionalApi]
    public IEmulationApi? EmulationAPI { get; set; }
    [OptionalApi]
    public IGuiApi? GuiAPI { get; set; }

    // API container - this is what we will actually use.
    public ApiContainer? _maybeAPIContainer { get; set; }

    private ApiContainer APIs
        => _maybeAPIContainer!;
#endregion

#region Lifecycle
    private Server? _server;

    public Server? Server {
        get {
            if (_server == null) {
                return null;
            }

            var server = _server!;

            // There are other reasons we might want to return null here, such as if the server is dead.
            // TODO

            return server;
        }
    }
#endregion

#region Convenience
    private string ServerHostSetting {
        get {
            switch (hostSelectorComboBox.SelectedIndex) {
                case 0:
                default:
                    return "127.0.0.1";
                case 1:
                    return "[::1]";
                case 2:
                    return "0.0.0.0";
                case 3:
                    return "[::]";
                case 4:
                    return customHostTextBox.Text.Trim();
            }
        }
        set {
            // Resets the custom host text box for if this is not actually a custom host
            customHostTextBox.Text = "127.0.0.1";

            var trimmedValue = value.Trim();

            switch (trimmedValue) {
                case "127.0.0.1":
                    hostSelectorComboBox.SelectedIndex = 0;
                    return;
                case "[::1]":
                case "::1":
                    hostSelectorComboBox.SelectedIndex = 1;
                    return;
                case "0.0.0.0":
                    hostSelectorComboBox.SelectedIndex = 2;
                    return;
                case "[::]":
                case "::":
                    hostSelectorComboBox.SelectedIndex = 3;
                    return;
                default:
                    hostSelectorComboBox.SelectedIndex = 4;
                    customHostTextBox.Text = trimmedValue;
                    return;
            }
        }
    }
#endregion

    public ExternalToolForm() {
        Logging.Log("Form initializing");

        InitializeComponent();  // defined in ExternalToolForm.Designer.cs

        hostSelectorComboBox.SelectedIndex = 0;

        Logging.Log("Form initialized");
    }

#region EmuHawk events

    protected override void GeneralUpdate() {
        if (!GlobalState.IsBackgroundHooked)
        {
            GlobalState.GeneralUpdate(APIs, false);
        }
    }

#endregion

#region WinForms
    private void hostSelectorComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        // The custom host text box should only be visible if the user has selected to provide a custom host value.
        customHostTextBox.Visible = hostSelectorComboBox.SelectedIndex == 4;
    }

    private void saveHostSettingsButton_Click(object sender, System.EventArgs e)
    {
        // TODO
    }

    private void serverStartButton_Click(object sender, System.EventArgs e)
    {

    }

    private void serverStopButton_Click(object sender, System.EventArgs e)
    {

    }
#endregion

}
