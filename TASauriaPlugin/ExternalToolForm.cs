namespace ScarletCafe.TASauriaPlugin;

using System;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;


[ExternalTool("TASauria", Description = "")]
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
        secAllowROMLoad.Checked = GlobalState.configuration.SecurityAllowROMManagement;

        if (GlobalState.configuration.ServerStartAutomatically && GlobalState.server == null) {
            GlobalState.StartServer();
        }

        UpdateServerHostVisibility();

        Logging.Log("Form initialized");
    }

#region EmuHawk events
    protected override void GeneralUpdate() {
        GlobalState.GeneralUpdate(APIs);
    }
#endregion

#region WinForms
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
    }

    private void saveSettingsButton_Click(object sender, EventArgs e)
    {
        WriteConfiguration();
        GlobalState.SaveConfig();
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
    }

    private void secAllowClientControl_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowClientControl = secAllowClientControl.Checked;
    }

    private void secAllowJoypad_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowJoypadSystemInput = secAllowJoypad.Checked;
    }

    private void secAllowMemoryRead_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowMemoryRead = secAllowMemoryRead.Checked;
    }

    private void secAllowMemoryWrite_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowMemoryWrite = secAllowMemoryWrite.Checked;
    }

    private void secAllowSavestate_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowSavestate = secAllowSavestate.Checked;
    }

    private void secAllowROMLoad_CheckedChanged(object sender, EventArgs e)
    {
        GlobalState.configuration.SecurityAllowROMManagement = secAllowROMLoad.Checked;
    }

#endregion

}
