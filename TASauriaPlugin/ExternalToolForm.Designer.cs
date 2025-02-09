namespace ScarletCafe.TASauriaPlugin;

using System.Drawing;
using System.Windows.Forms;

partial class ExternalToolForm
{
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalToolForm));
            this.mainTabSelector = new System.Windows.Forms.TabControl();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.serverStartOnLoad = new System.Windows.Forms.CheckBox();
            this.customHostTextBox = new System.Windows.Forms.TextBox();
            this.portInfoLabel = new System.Windows.Forms.Label();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hostInfoLabel = new System.Windows.Forms.Label();
            this.hostSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.serverLifecycleButtonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.serverStartButton = new System.Windows.Forms.Button();
            this.serverStopButton = new System.Windows.Forms.Button();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.secAllowAVControl = new System.Windows.Forms.CheckBox();
            this.secAllowMovie = new System.Windows.Forms.CheckBox();
            this.secAllowROMLoad = new System.Windows.Forms.CheckBox();
            this.secAllowSavestate = new System.Windows.Forms.CheckBox();
            this.secAllowMemoryWrite = new System.Windows.Forms.CheckBox();
            this.secAllowMemoryRead = new System.Windows.Forms.CheckBox();
            this.secAllowJoypad = new System.Windows.Forms.CheckBox();
            this.secAllowClientControl = new System.Windows.Forms.CheckBox();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.mainTabSelector.SuspendLayout();
            this.tabServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.serverLifecycleButtonPanel.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.SuspendLayout();
            //
            // mainTabSelector
            //
            this.mainTabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabSelector.Controls.Add(this.tabServer);
            this.mainTabSelector.Controls.Add(this.tabSecurity);
            this.mainTabSelector.Location = new System.Drawing.Point(12, 92);
            this.mainTabSelector.Name = "mainTabSelector";
            this.mainTabSelector.SelectedIndex = 0;
            this.mainTabSelector.Size = new System.Drawing.Size(658, 253);
            this.mainTabSelector.TabIndex = 0;
            //
            // tabServer
            //
            this.tabServer.Controls.Add(this.serverStartOnLoad);
            this.tabServer.Controls.Add(this.customHostTextBox);
            this.tabServer.Controls.Add(this.portInfoLabel);
            this.tabServer.Controls.Add(this.portNumericUpDown);
            this.tabServer.Controls.Add(this.hostInfoLabel);
            this.tabServer.Controls.Add(this.hostSelectorComboBox);
            this.tabServer.Controls.Add(this.serverLifecycleButtonPanel);
            this.tabServer.Location = new System.Drawing.Point(4, 22);
            this.tabServer.Name = "tabServer";
            this.tabServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabServer.Size = new System.Drawing.Size(650, 227);
            this.tabServer.TabIndex = 0;
            this.tabServer.Text = "Server";
            this.tabServer.UseVisualStyleBackColor = true;
            //
            // serverStartOnLoad
            //
            this.serverStartOnLoad.AutoSize = true;
            this.serverStartOnLoad.Location = new System.Drawing.Point(6, 75);
            this.serverStartOnLoad.Name = "serverStartOnLoad";
            this.serverStartOnLoad.Size = new System.Drawing.Size(330, 16);
            this.serverStartOnLoad.TabIndex = 6;
            this.serverStartOnLoad.Text = "Start server automatically when external tool window opens";
            this.serverStartOnLoad.UseVisualStyleBackColor = true;
            this.serverStartOnLoad.CheckedChanged += new System.EventHandler(this.serverStartOnLoad_CheckedChanged);
            //
            // customHostTextBox
            //
            this.customHostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customHostTextBox.Location = new System.Drawing.Point(7, 47);
            this.customHostTextBox.Name = "customHostTextBox";
            this.customHostTextBox.Size = new System.Drawing.Size(506, 19);
            this.customHostTextBox.TabIndex = 5;
            this.customHostTextBox.Text = "127.0.0.1";
            this.customHostTextBox.Visible = false;
            this.customHostTextBox.TextChanged += new System.EventHandler(this.customHostTextBox_TextChanged);
            //
            // portInfoLabel
            //
            this.portInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portInfoLabel.AutoSize = true;
            this.portInfoLabel.Location = new System.Drawing.Point(517, 6);
            this.portInfoLabel.Name = "portInfoLabel";
            this.portInfoLabel.Size = new System.Drawing.Size(26, 12);
            this.portInfoLabel.TabIndex = 4;
            this.portInfoLabel.Text = "Port";
            //
            // portNumericUpDown
            //
            this.portNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portNumericUpDown.Location = new System.Drawing.Point(519, 21);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericUpDown.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(126, 19);
            this.portNumericUpDown.TabIndex = 3;
            this.portNumericUpDown.Value = new decimal(new int[] {
            20251,
            0,
            0,
            0});
            this.portNumericUpDown.ValueChanged += new System.EventHandler(this.portNumericUpDown_ValueChanged);
            //
            // hostInfoLabel
            //
            this.hostInfoLabel.AutoSize = true;
            this.hostInfoLabel.Location = new System.Drawing.Point(6, 6);
            this.hostInfoLabel.Name = "hostInfoLabel";
            this.hostInfoLabel.Size = new System.Drawing.Size(29, 12);
            this.hostInfoLabel.TabIndex = 2;
            this.hostInfoLabel.Text = "Host";
            //
            // hostSelectorComboBox
            //
            this.hostSelectorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostSelectorComboBox.FormattingEnabled = true;
            this.hostSelectorComboBox.Items.AddRange(new object[] {
            "127.0.0.1 (This machine only, IPv4)",
            "[::1] (This machine only, IPv6)",
            "0.0.0.0 (All interfaces, IPv4)",
            "[::] (All interfaces, IPv6)",
            "Custom..."});
            this.hostSelectorComboBox.Location = new System.Drawing.Point(6, 21);
            this.hostSelectorComboBox.Name = "hostSelectorComboBox";
            this.hostSelectorComboBox.Size = new System.Drawing.Size(507, 20);
            this.hostSelectorComboBox.TabIndex = 1;
            this.hostSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.hostSelectorComboBox_SelectedIndexChanged);
            //
            // serverLifecycleButtonPanel
            //
            this.serverLifecycleButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverLifecycleButtonPanel.ColumnCount = 2;
            this.serverLifecycleButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.Controls.Add(this.serverStartButton, 0, 0);
            this.serverLifecycleButtonPanel.Controls.Add(this.serverStopButton, 1, 0);
            this.serverLifecycleButtonPanel.Location = new System.Drawing.Point(0, 193);
            this.serverLifecycleButtonPanel.Name = "serverLifecycleButtonPanel";
            this.serverLifecycleButtonPanel.RowCount = 1;
            this.serverLifecycleButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.Size = new System.Drawing.Size(649, 33);
            this.serverLifecycleButtonPanel.TabIndex = 0;
            //
            // serverStartButton
            //
            this.serverStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverStartButton.Location = new System.Drawing.Point(3, 3);
            this.serverStartButton.Name = "serverStartButton";
            this.serverStartButton.Size = new System.Drawing.Size(318, 27);
            this.serverStartButton.TabIndex = 0;
            this.serverStartButton.Text = "Start";
            this.serverStartButton.UseVisualStyleBackColor = true;
            this.serverStartButton.Click += new System.EventHandler(this.serverStartButton_Click);
            //
            // serverStopButton
            //
            this.serverStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverStopButton.Enabled = false;
            this.serverStopButton.Location = new System.Drawing.Point(327, 3);
            this.serverStopButton.Name = "serverStopButton";
            this.serverStopButton.Size = new System.Drawing.Size(319, 27);
            this.serverStopButton.TabIndex = 1;
            this.serverStopButton.Text = "Stop";
            this.serverStopButton.UseVisualStyleBackColor = true;
            this.serverStopButton.Click += new System.EventHandler(this.serverStopButton_Click);
            //
            // tabSecurity
            //
            this.tabSecurity.Controls.Add(this.secAllowAVControl);
            this.tabSecurity.Controls.Add(this.secAllowMovie);
            this.tabSecurity.Controls.Add(this.secAllowROMLoad);
            this.tabSecurity.Controls.Add(this.secAllowSavestate);
            this.tabSecurity.Controls.Add(this.secAllowMemoryWrite);
            this.tabSecurity.Controls.Add(this.secAllowMemoryRead);
            this.tabSecurity.Controls.Add(this.secAllowJoypad);
            this.tabSecurity.Controls.Add(this.secAllowClientControl);
            this.tabSecurity.Location = new System.Drawing.Point(4, 22);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tabSecurity.Size = new System.Drawing.Size(650, 227);
            this.tabSecurity.TabIndex = 1;
            this.tabSecurity.Text = "Security";
            this.tabSecurity.UseVisualStyleBackColor = true;
            //
            // secAllowAVControl
            //
            this.secAllowAVControl.AutoSize = true;
            this.secAllowAVControl.Location = new System.Drawing.Point(6, 156);
            this.secAllowAVControl.Name = "secAllowAVControl";
            this.secAllowAVControl.Size = new System.Drawing.Size(117, 16);
            this.secAllowAVControl.TabIndex = 7;
            this.secAllowAVControl.Text = "Allow A/V control";
            this.secAllowAVControl.UseVisualStyleBackColor = true;
            this.secAllowAVControl.CheckedChanged += new System.EventHandler(this.secAllowAVControl_CheckedChanged);
            //
            // secAllowMovie
            //
            this.secAllowMovie.AutoSize = true;
            this.secAllowMovie.Location = new System.Drawing.Point(6, 131);
            this.secAllowMovie.Name = "secAllowMovie";
            this.secAllowMovie.Size = new System.Drawing.Size(154, 16);
            this.secAllowMovie.TabIndex = 6;
            this.secAllowMovie.Text = "Allow movie management";
            this.secAllowMovie.UseVisualStyleBackColor = true;
            this.secAllowMovie.CheckedChanged += new System.EventHandler(this.secAllowMovie_CheckedChanged);
            //
            // secAllowROMLoad
            //
            this.secAllowROMLoad.AutoSize = true;
            this.secAllowROMLoad.Location = new System.Drawing.Point(6, 181);
            this.secAllowROMLoad.Name = "secAllowROMLoad";
            this.secAllowROMLoad.Size = new System.Drawing.Size(183, 16);
            this.secAllowROMLoad.TabIndex = 5;
            this.secAllowROMLoad.Text = "Allow loading and closing ROM";
            this.secAllowROMLoad.UseVisualStyleBackColor = true;
            this.secAllowROMLoad.CheckedChanged += new System.EventHandler(this.secAllowROMLoad_CheckedChanged);
            //
            // secAllowSavestate
            //
            this.secAllowSavestate.AutoSize = true;
            this.secAllowSavestate.Location = new System.Drawing.Point(6, 106);
            this.secAllowSavestate.Name = "secAllowSavestate";
            this.secAllowSavestate.Size = new System.Drawing.Size(215, 16);
            this.secAllowSavestate.TabIndex = 4;
            this.secAllowSavestate.Text = "Allow saving and loading save states";
            this.secAllowSavestate.UseVisualStyleBackColor = true;
            this.secAllowSavestate.CheckedChanged += new System.EventHandler(this.secAllowSavestate_CheckedChanged);
            //
            // secAllowMemoryWrite
            //
            this.secAllowMemoryWrite.AutoSize = true;
            this.secAllowMemoryWrite.Location = new System.Drawing.Point(6, 81);
            this.secAllowMemoryWrite.Name = "secAllowMemoryWrite";
            this.secAllowMemoryWrite.Size = new System.Drawing.Size(134, 16);
            this.secAllowMemoryWrite.TabIndex = 3;
            this.secAllowMemoryWrite.Text = "Allow writing memory";
            this.secAllowMemoryWrite.UseVisualStyleBackColor = true;
            this.secAllowMemoryWrite.CheckedChanged += new System.EventHandler(this.secAllowMemoryWrite_CheckedChanged);
            //
            // secAllowMemoryRead
            //
            this.secAllowMemoryRead.AutoSize = true;
            this.secAllowMemoryRead.Checked = true;
            this.secAllowMemoryRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secAllowMemoryRead.Location = new System.Drawing.Point(6, 56);
            this.secAllowMemoryRead.Name = "secAllowMemoryRead";
            this.secAllowMemoryRead.Size = new System.Drawing.Size(137, 16);
            this.secAllowMemoryRead.TabIndex = 2;
            this.secAllowMemoryRead.Text = "Allow reading memory";
            this.secAllowMemoryRead.UseVisualStyleBackColor = true;
            this.secAllowMemoryRead.CheckedChanged += new System.EventHandler(this.secAllowMemoryRead_CheckedChanged);
            //
            // secAllowJoypad
            //
            this.secAllowJoypad.AutoSize = true;
            this.secAllowJoypad.Checked = true;
            this.secAllowJoypad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secAllowJoypad.Location = new System.Drawing.Point(6, 31);
            this.secAllowJoypad.Name = "secAllowJoypad";
            this.secAllowJoypad.Size = new System.Drawing.Size(371, 16);
            this.secAllowJoypad.TabIndex = 1;
            this.secAllowJoypad.Text = "Allow joypad/system input (buttons, control stick, reset button, etc)";
            this.secAllowJoypad.UseVisualStyleBackColor = true;
            this.secAllowJoypad.CheckedChanged += new System.EventHandler(this.secAllowJoypad_CheckedChanged);
            //
            // secAllowClientControl
            //
            this.secAllowClientControl.AutoSize = true;
            this.secAllowClientControl.Checked = true;
            this.secAllowClientControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secAllowClientControl.Location = new System.Drawing.Point(6, 6);
            this.secAllowClientControl.Name = "secAllowClientControl";
            this.secAllowClientControl.Size = new System.Drawing.Size(358, 16);
            this.secAllowClientControl.TabIndex = 0;
            this.secAllowClientControl.Text = "Allow client control (pause, frame advance, reset core, turbo, etc)";
            this.secAllowClientControl.UseVisualStyleBackColor = true;
            this.secAllowClientControl.CheckedChanged += new System.EventHandler(this.secAllowClientControl_CheckedChanged);
            //
            // saveSettingsButton
            //
            this.saveSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSettingsButton.Location = new System.Drawing.Point(12, 346);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(658, 24);
            this.saveSettingsButton.TabIndex = 6;
            this.saveSettingsButton.Text = "Save current settings as default";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            //
            // panelHeader
            //
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Location = new System.Drawing.Point(12, 12);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(658, 74);
            this.panelHeader.TabIndex = 1;
            //
            // statusLabel
            //
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Location = new System.Drawing.Point(12, 377);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(658, 17);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "No status available";
            //
            // ExternalToolForm
            //
            this.ClientSize = new System.Drawing.Size(682, 403);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.mainTabSelector);
            this.Name = "ExternalToolForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExternalToolForm_FormClosing);
            this.mainTabSelector.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.serverLifecycleButtonPanel.ResumeLayout(false);
            this.tabSecurity.ResumeLayout(false);
            this.tabSecurity.PerformLayout();
            this.ResumeLayout(false);

    }

    private TabControl mainTabSelector;
    private TabPage tabServer;
    private TabPage tabSecurity;
    private Panel panelHeader;
    private TableLayoutPanel serverLifecycleButtonPanel;
    private Button serverStartButton;
    private Button serverStopButton;
    private Label hostInfoLabel;
    private ComboBox hostSelectorComboBox;
    private Label portInfoLabel;
    private NumericUpDown portNumericUpDown;
    private TextBox customHostTextBox;
    private Button saveSettingsButton;
    private CheckBox secAllowJoypad;
    private CheckBox secAllowClientControl;
    private CheckBox secAllowROMLoad;
    private CheckBox secAllowSavestate;
    private CheckBox secAllowMemoryWrite;
    private CheckBox secAllowMemoryRead;
    private Label statusLabel;
    private CheckBox serverStartOnLoad;
    private CheckBox secAllowAVControl;
    private CheckBox secAllowMovie;
}