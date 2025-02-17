namespace ScarletCafe.TASauriaPlugin;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


[ExternalTool("TASauria", Description = "")]
[ExternalToolEmbeddedIcon("TASauriaPlugin.TASauriaIcon.ico")]
public sealed partial class ExternalToolForm : ToolFormBase, IExternalToolForm {
    protected override string WindowTitleStatic
        => "TASauria";

#region APIs
    public ApiContainer? _maybeAPIContainer { get; set; }

    private ApiContainer APIs
        => _maybeAPIContainer!;
#endregion

#region Convenience
    private string ServerHostSetting {
        get {
            return hostSelectorComboBox.SelectedIndex switch
            {
                1 => "[::1]",
                2 => "0.0.0.0",
                3 => "[::]",
                4 => customHostTextBox.Text.Trim(),
                _ => "127.0.0.1",
            };
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

    private void WriteConfiguration()
    {
        GlobalState.configuration.ServerHost = ServerHostSetting;
        GlobalState.configuration.ServerPort = (int)portNumericUpDown.Value;
    }

    private void UpdateServerHostVisibility()
    {
        bool serverisRunning = GlobalState.server != null;

        hostSelectorComboBox.Enabled = !serverisRunning;
        customHostTextBox.Enabled = !serverisRunning;
        portNumericUpDown.Enabled = !serverisRunning;
        serverStartButton.Enabled = !serverisRunning;
        serverStopButton.Enabled = serverisRunning;
    }

#endregion

    public ExternalToolForm() {
        Logging.Log("Form initializing");

        InitializeComponent();  // defined in ExternalToolForm.Designer.cs

        ServerHostSetting = GlobalState.configuration.ServerHost;
        portNumericUpDown.Value = GlobalState.configuration.ServerPort;
        serverStartOnLoad.Checked = GlobalState.configuration.ServerStartAutomatically;

        secAllowClientControl.Checked = GlobalState.configuration.SecurityAllowClientControl;
        secAllowJoypad.Checked = GlobalState.configuration.SecurityAllowJoypadSystemInput;
        secAllowMemoryRead.Checked = GlobalState.configuration.SecurityAllowMemoryRead;
        secAllowMemoryWrite.Checked = GlobalState.configuration.SecurityAllowMemoryWrite;
        secAllowSavestate.Checked = GlobalState.configuration.SecurityAllowSavestate;
        secAllowMovie.Checked = GlobalState.configuration.SecurityAllowMovieManagement;
        secAllowAVControl.Checked = GlobalState.configuration.SecurityAllowAVControl;
        secAllowROMLoad.Checked = GlobalState.configuration.SecurityAllowROMManagement;

        if (GlobalState.configuration.ServerStartAutomatically && GlobalState.server == null) {
            GlobalState.StartServer();
        }

        UpdateServerHostVisibility();

        saveSettingsButton.Enabled = false;

        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TASauriaPlugin.TASauriaIcon.ico")) {
            Icon = new System.Drawing.Icon(stream);
        }

        Logging.Log("Form initialized");
    }

#region EmuHawk events
    public override void UpdateValues(ToolFormUpdateType type) {
        if (Config != null) {
            GlobalState.GeneralUpdate(new EmulatorInterface(
                APIs, MainForm, Config
            ));
        } else {
            statusLabel.Text = "Config not initialized, no updates happening...";
        }

        // After saving, show it in the status label for 1 second
        if (DateTime.UtcNow < GlobalState.ConfigLastSaved.AddSeconds(1)) {
            statusLabel.Text = "Settings saved.";
            return;
        }

        if (GlobalState.server != null) {
            Server server = GlobalState.server!;
            statusLabel.Text = $"Server running on {server.Host}:{server.Port} since {server.Started} ({Commands.Registry.commandsExecuted} commands processed)";
        } else {
            statusLabel.Text = "Server stopped.";
        }
    }

#endregion

#region WinForms

    /// <summary>
    /// In BizHawk 2.10 and onwards, external tools receive GeneralUpdate, or more specifically,
    /// they receive UpdateValues(ToolFormUpdateType.General) every UI frame, regardless of if the
    /// core is advancing or not.
    ///
    /// However, this was not always the case.
    ///
    /// The change that introduced this is below:
    /// https://github.com/TASEmulators/BizHawk/issues/3626
    /// https://github.com/TASEmulators/BizHawk/commit/5eb2cd8cb128928fc4fd9af00010a5d74c99adcd
    ///
    /// Before 2.10, external tools would only be updated when the core was advancing.
    /// Because TASauria intentionally tries to block the main thread for as little time as possible,
    /// this means that in these old versions it is almost impossible to do things like update the
    /// joypad values every frame.
    ///
    /// In 2.10, we can do it by pausing the emulator and manually frame advancing, but in <2.10
    /// pausing stops TASauria from executing any requests at all.
    ///
    /// On the other hand, we can't just mess with the emulator without *any* regard for the UI status.
    ///
    /// BizHawk's cores are NOT thread safe and the behavior of ApiContainer and so on is undefined
    /// when not on the main thread.
    ///
    /// Fortunately, System.Windows.Forms.Timer comes in clutch:
    /// https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.timer?view=netframework-4.8.1
    ///
    /// As per the documentation:
    ///  "This Windows timer is designed for a single-threaded environment where UI threads are used
    ///   to perform processing."
    ///
    /// This gives us a way to take control of the main thread at regular intervals, allowing us to
    /// control the emulator while paused in <2.10 without losing thread safety.
    ///
    /// The only caveat is that:
    ///  "The Windows Forms Timer component is [...] limited to an accuracy of 55 milliseconds."
    ///
    /// This is not really a problem when paused anyway as operations should be able to take as much
    /// time as they need, but in 2.10 we can actually *guarantee* the external tool is updated once
    /// per UI frame, so if no frame limiter is enabled it can theoretically update at a much higher
    /// frequency.
    ///
    /// The resulting call stack ends up being:
    ///   - BizHawk.Client.EmuHawk.Program.Main
    ///   - BizHawk.Client.EmuHawk.Program.SubMain
    ///   - BizHawk.Client.EmuHawk.MainForm.ProgramRunLoop  (this is where GeneralUpdate happens in 2.10)
    ///   - BizHawk.Client.EmuHawk.MainForm.CheckMessages
    ///   - ScarletCafe.TASauriaPlugin.ExternalToolForm.frameUpdateTimer_Tick (this function)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frameUpdateTimer_Tick(object sender, EventArgs e)
    {
        UpdateValues(ToolFormUpdateType.General);
    }

    private void ExternalToolForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (GlobalState.server != null) {
            DialogResult result = MessageBox.Show(
                "The TASauria server can only interact with the emulator while this external tool window is open.\nIf you close it, the server will be automatically stopped.\nAre you sure you want to exit TASauria?",
                "Are you sure you want to exit TASauria?",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.No) {
                e.Cancel = true;
            } else {
                GlobalState.StopServer();
            }
        }
    }

    private void hostSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // The custom host text box should only be visible if the user has selected to provide a custom host value.
        customHostTextBox.Visible = hostSelectorComboBox.SelectedIndex == 4;
        saveSettingsButton.Enabled = true;
    }

    private void customHostTextBox_TextChanged(object sender, EventArgs e)
    {
        saveSettingsButton.Enabled = true;
    }

    private void portNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        saveSettingsButton.Enabled = true;
    }

    private void saveSettingsButton_Click(object sender, EventArgs e)
    {
        WriteConfiguration();
        GlobalState.SaveConfig();
        saveSettingsButton.Enabled = false;
    }

    private void serverStartButton_Click(object sender, EventArgs e)
    {
        WriteConfiguration();
        if (GlobalState.server == null) {
            GlobalState.StartServer();
        }
        UpdateServerHostVisibility();
    }

    private void serverStopButton_Click(object sender, EventArgs e)
    {
        if (GlobalState.server != null) {
            GlobalState.StopServer();
        }
        UpdateServerHostVisibility();
    }

    private void serverStartOnLoad_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.ServerStartAutomatically = serverStartOnLoad.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowClientControl_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowClientControl = secAllowClientControl.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowJoypad_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowJoypadSystemInput = secAllowJoypad.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowMemoryRead_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowMemoryRead = secAllowMemoryRead.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowMemoryWrite_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowMemoryWrite = secAllowMemoryWrite.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowSavestate_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowSavestate = secAllowSavestate.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowMovie_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowMovieManagement = secAllowMovie.Checked;
        saveSettingsButton.Enabled = true;
    }

    private void secAllowAVControl_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowAVControl = secAllowAVControl.Checked;
        saveSettingsButton.Enabled = true;
    }
    private void secAllowROMLoad_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowROMManagement = secAllowROMLoad.Checked;
        saveSettingsButton.Enabled = true;
    }

#endregion

}
