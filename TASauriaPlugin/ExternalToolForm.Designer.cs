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
            this.mainTabSelector = new System.Windows.Forms.TabControl();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.serverLifecycleButtonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.serverStartButton = new System.Windows.Forms.Button();
            this.serverStopButton = new System.Windows.Forms.Button();
            this.hostSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.hostInfoLabel = new System.Windows.Forms.Label();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.portInfoLabel = new System.Windows.Forms.Label();
            this.customHostTextBox = new System.Windows.Forms.TextBox();
            this.saveHostSettingsButton = new System.Windows.Forms.Button();
            this.mainTabSelector.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.serverLifecycleButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            //
            // mainTabSelector
            //
            this.mainTabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabSelector.Controls.Add(this.tabServer);
            this.mainTabSelector.Controls.Add(this.tabSettings);
            this.mainTabSelector.Location = new System.Drawing.Point(12, 92);
            this.mainTabSelector.Name = "mainTabSelector";
            this.mainTabSelector.SelectedIndex = 0;
            this.mainTabSelector.Size = new System.Drawing.Size(576, 295);
            this.mainTabSelector.TabIndex = 0;
            //
            // tabServer
            //
            this.tabServer.Controls.Add(this.saveHostSettingsButton);
            this.tabServer.Controls.Add(this.customHostTextBox);
            this.tabServer.Controls.Add(this.portInfoLabel);
            this.tabServer.Controls.Add(this.portNumericUpDown);
            this.tabServer.Controls.Add(this.hostInfoLabel);
            this.tabServer.Controls.Add(this.hostSelectorComboBox);
            this.tabServer.Controls.Add(this.serverLifecycleButtonPanel);
            this.tabServer.Location = new System.Drawing.Point(4, 22);
            this.tabServer.Name = "tabServer";
            this.tabServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabServer.Size = new System.Drawing.Size(568, 269);
            this.tabServer.TabIndex = 0;
            this.tabServer.Text = "Server";
            this.tabServer.UseVisualStyleBackColor = true;
            //
            // tabSettings
            //
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(568, 269);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            //
            // panelHeader
            //
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Location = new System.Drawing.Point(12, 12);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(576, 74);
            this.panelHeader.TabIndex = 1;
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
            this.serverLifecycleButtonPanel.Location = new System.Drawing.Point(0, 235);
            this.serverLifecycleButtonPanel.Name = "serverLifecycleButtonPanel";
            this.serverLifecycleButtonPanel.RowCount = 1;
            this.serverLifecycleButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.serverLifecycleButtonPanel.Size = new System.Drawing.Size(567, 33);
            this.serverLifecycleButtonPanel.TabIndex = 0;
            //
            // serverStartButton
            //
            this.serverStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverStartButton.Location = new System.Drawing.Point(3, 3);
            this.serverStartButton.Name = "serverStartButton";
            this.serverStartButton.Size = new System.Drawing.Size(277, 27);
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
            this.serverStopButton.Location = new System.Drawing.Point(286, 3);
            this.serverStopButton.Name = "serverStopButton";
            this.serverStopButton.Size = new System.Drawing.Size(278, 27);
            this.serverStopButton.TabIndex = 1;
            this.serverStopButton.Text = "Stop";
            this.serverStopButton.UseVisualStyleBackColor = true;
            this.serverStopButton.Click += new System.EventHandler(this.serverStopButton_Click);
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
            this.hostSelectorComboBox.Size = new System.Drawing.Size(425, 20);
            this.hostSelectorComboBox.TabIndex = 1;
            this.hostSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.hostSelectorComboBox_SelectedIndexChanged);
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
            // portNumericUpDown
            //
            this.portNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portNumericUpDown.Location = new System.Drawing.Point(437, 21);
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
            //
            // portInfoLabel
            //
            this.portInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portInfoLabel.AutoSize = true;
            this.portInfoLabel.Location = new System.Drawing.Point(435, 6);
            this.portInfoLabel.Name = "portInfoLabel";
            this.portInfoLabel.Size = new System.Drawing.Size(26, 12);
            this.portInfoLabel.TabIndex = 4;
            this.portInfoLabel.Text = "Port";
            //
            // customHostTextBox
            //
            this.customHostTextBox.Location = new System.Drawing.Point(7, 47);
            this.customHostTextBox.Name = "customHostTextBox";
            this.customHostTextBox.Size = new System.Drawing.Size(424, 19);
            this.customHostTextBox.TabIndex = 5;
            this.customHostTextBox.Text = "127.0.0.1";
            this.customHostTextBox.Visible = false;
            //
            // saveHostSettingsButton
            //
            this.saveHostSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveHostSettingsButton.Location = new System.Drawing.Point(8, 72);
            this.saveHostSettingsButton.Name = "saveHostSettingsButton";
            this.saveHostSettingsButton.Size = new System.Drawing.Size(554, 24);
            this.saveHostSettingsButton.TabIndex = 6;
            this.saveHostSettingsButton.Text = "Save as default";
            this.saveHostSettingsButton.UseVisualStyleBackColor = true;
            this.saveHostSettingsButton.Click += new System.EventHandler(this.saveHostSettingsButton_Click);
            //
            // TASauriaForm
            //
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.mainTabSelector);
            this.Name = "TASauriaForm";
            this.mainTabSelector.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.serverLifecycleButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);

    }

    private TabControl mainTabSelector;
    private TabPage tabServer;
    private TabPage tabSettings;
    private Panel panelHeader;
    private TableLayoutPanel serverLifecycleButtonPanel;
    private Button serverStartButton;
    private Button serverStopButton;
    private Label hostInfoLabel;
    private ComboBox hostSelectorComboBox;
    private Label portInfoLabel;
    private NumericUpDown portNumericUpDown;
    private TextBox customHostTextBox;
    private Button saveHostSettingsButton;
}