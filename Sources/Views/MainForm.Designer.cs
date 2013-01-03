namespace ScreenCapture.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (viewModel != null)
                    viewModel.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.explorerBrowser = new Controls.ExplorerBrowserEx();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.btnCaptureMode = new ScreenCapture.Controls.BindableToolStripDropDownButton();
            this.btnCapturePrimaryScreen = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnCaptureWindow = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnCaptureRegion = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnStartRecording = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStopRecording = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnPausePlaying = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStartPlaying = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnScreenPreview = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStorageFolder = new ScreenCapture.Controls.BindableToolStripButton();
            this.lbStatusRecording = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.lbStatusReady = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.iconPlayPause = new ScreenCapture.Controls.BindableNotifyIcon(this.components);
            this.keyPlayPause = new ScreenCapture.Controls.HotKey(this.components);
            this.keyStartStop = new ScreenCapture.Controls.HotKey(this.components);
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCaptureMode,
            this.btnStartRecording,
            this.btnStopRecording,
            this.btnPausePlaying,
            this.btnStartPlaying,
            this.toolStripSeparator1,
            this.btnScreenPreview,
            this.btnStorageFolder});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(656, 39);
            this.toolStrip.TabIndex = 2;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // explorerBrowser
            // 
            this.explorerBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerBrowser.Location = new System.Drawing.Point(0, 0);
            this.explorerBrowser.Name = "explorerBrowser";
            this.explorerBrowser.Size = new System.Drawing.Size(656, 342);
            this.explorerBrowser.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusRecording,
            this.lbStatusReady,
            this.toolStripStatusLabel1,
            this.btnSettings});
            this.statusStrip.Location = new System.Drawing.Point(0, 381);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(656, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(525, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::ScreenCapture.Properties.Resources.advancedsettings;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(65, 17);
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.videoSourcePlayer1);
            this.panel1.Controls.Add(this.explorerBrowser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 342);
            this.panel1.TabIndex = 4;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.BackColor = System.Drawing.Color.Black;
            this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoSourcePlayer1.KeepAspectRatio = true;
            this.videoSourcePlayer1.Location = new System.Drawing.Point(0, 0);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(656, 342);
            this.videoSourcePlayer1.TabIndex = 3;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // btnCaptureMode
            // 
            this.btnCaptureMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCapturePrimaryScreen,
            this.btnCaptureWindow,
            this.btnCaptureRegion});
            this.btnCaptureMode.Image = global::ScreenCapture.Properties.Resources.ksnapshot;
            this.btnCaptureMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCaptureMode.Name = "btnCaptureMode";
            this.btnCaptureMode.Size = new System.Drawing.Size(131, 36);
            this.btnCaptureMode.Text = "Primary Screen";
            // 
            // btnCapturePrimaryScreen
            // 
            this.btnCapturePrimaryScreen.Checked = true;
            this.btnCapturePrimaryScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnCapturePrimaryScreen.Image = global::ScreenCapture.Properties.Resources.ksnapshot;
            this.btnCapturePrimaryScreen.Name = "btnCapturePrimaryScreen";
            this.btnCapturePrimaryScreen.Size = new System.Drawing.Size(153, 22);
            this.btnCapturePrimaryScreen.Text = "Primary Screen";
            this.btnCapturePrimaryScreen.Click += new System.EventHandler(this.btnCapturePrimaryScreen_Click);
            // 
            // btnCaptureWindow
            // 
            this.btnCaptureWindow.Image = global::ScreenCapture.Properties.Resources.kpersonalizer;
            this.btnCaptureWindow.Name = "btnCaptureWindow";
            this.btnCaptureWindow.Size = new System.Drawing.Size(153, 22);
            this.btnCaptureWindow.Text = "Single Window";
            this.btnCaptureWindow.Click += new System.EventHandler(this.btnCaptureWindow_Click);
            // 
            // btnCaptureRegion
            // 
            this.btnCaptureRegion.Image = global::ScreenCapture.Properties.Resources.kview;
            this.btnCaptureRegion.Name = "btnCaptureRegion";
            this.btnCaptureRegion.Size = new System.Drawing.Size(153, 22);
            this.btnCaptureRegion.Text = "Fixed Region";
            this.btnCaptureRegion.Click += new System.EventHandler(this.btnCaptureRegion_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStartRecording.Image = global::ScreenCapture.Properties.Resources.camera;
            this.btnStartRecording.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(121, 36);
            this.btnStartRecording.Text = "Start recording";
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStopRecording.Image = global::ScreenCapture.Properties.Resources.player_stop;
            this.btnStopRecording.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(121, 36);
            this.btnStopRecording.Text = "Stop recording";
            this.btnStopRecording.Visible = false;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnPausePlaying
            // 
            this.btnPausePlaying.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPausePlaying.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPausePlaying.Image = global::ScreenCapture.Properties.Resources.player_pause;
            this.btnPausePlaying.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPausePlaying.Name = "btnPausePlaying";
            this.btnPausePlaying.Size = new System.Drawing.Size(36, 36);
            this.btnPausePlaying.Text = "Pause playing";
            this.btnPausePlaying.ToolTipText = "Start playing the capture preview";
            this.btnPausePlaying.Visible = false;
            this.btnPausePlaying.Click += new System.EventHandler(this.btnPausePlaying_Click);
            // 
            // btnStartPlaying
            // 
            this.btnStartPlaying.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStartPlaying.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartPlaying.Image = global::ScreenCapture.Properties.Resources.player_play;
            this.btnStartPlaying.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartPlaying.Name = "btnStartPlaying";
            this.btnStartPlaying.Size = new System.Drawing.Size(36, 36);
            this.btnStartPlaying.Text = "Start playing";
            this.btnStartPlaying.ToolTipText = "Start playing the capture preview";
            this.btnStartPlaying.Click += new System.EventHandler(this.btnStartPlaying_Click);
            // 
            // btnScreenPreview
            // 
            this.btnScreenPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnScreenPreview.Image = global::ScreenCapture.Properties.Resources.desktop;
            this.btnScreenPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScreenPreview.Name = "btnScreenPreview";
            this.btnScreenPreview.Size = new System.Drawing.Size(122, 36);
            this.btnScreenPreview.Text = "Screen preview";
            this.btnScreenPreview.Visible = false;
            this.btnScreenPreview.Click += new System.EventHandler(this.btnScreenPreview_Click);
            // 
            // btnStorageFolder
            // 
            this.btnStorageFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStorageFolder.Image = global::ScreenCapture.Properties.Resources.folder_video;
            this.btnStorageFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStorageFolder.Name = "btnStorageFolder";
            this.btnStorageFolder.Size = new System.Drawing.Size(117, 36);
            this.btnStorageFolder.Text = "Storage folder";
            this.btnStorageFolder.Click += new System.EventHandler(this.btnStorageFolder_Click);
            // 
            // lbStatusRecording
            // 
            this.lbStatusRecording.Image = ((System.Drawing.Image)(resources.GetObject("lbStatusRecording.Image")));
            this.lbStatusRecording.Name = "lbStatusRecording";
            this.lbStatusRecording.Size = new System.Drawing.Size(89, 20);
            this.lbStatusRecording.Text = "Recording";
            this.lbStatusRecording.Visible = false;
            // 
            // lbStatusReady
            // 
            this.lbStatusReady.Name = "lbStatusReady";
            this.lbStatusReady.Size = new System.Drawing.Size(51, 22);
            this.lbStatusReady.Text = "Ready";
            // 
            // iconPlayPause
            // 
            this.iconPlayPause.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.None;
            this.iconPlayPause.BalloonTipText = "";
            this.iconPlayPause.BalloonTipTitle = "";
            this.iconPlayPause.ContextMenu = null;
            this.iconPlayPause.ContextMenuStrip = null;
            this.iconPlayPause.Icon = null;
            this.iconPlayPause.Tag = null;
            this.iconPlayPause.Text = "";
            this.iconPlayPause.Visible = true;
            // 
            // keyPlayPause
            // 
            this.keyPlayPause.Enabled = true;
            this.keyPlayPause.Key = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.F9)));
            this.keyPlayPause.Pressed += new System.ComponentModel.HandledEventHandler(this.hotkeyPlayPause_Pressed);
            // 
            // keyStartStop
            // 
            this.keyStartStop.Enabled = true;
            this.keyStartStop.Key = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.F10)));
            this.keyStartStop.Pressed += new System.ComponentModel.HandledEventHandler(this.hotkeyStop_Pressed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Screencast Capture Lite";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScreenCapture.Controls.ExplorerBrowserEx explorerBrowser;
        private System.Windows.Forms.ToolStrip toolStrip;
        private ScreenCapture.Controls.BindableToolStripButton btnStartRecording;
        private System.Windows.Forms.StatusStrip statusStrip;
        private ScreenCapture.Controls.BindableToolStripStatusLabel lbStatusRecording;
        private ScreenCapture.Controls.BindableToolStripStatusLabel lbStatusReady;
        private ScreenCapture.Controls.BindableToolStripButton btnStopRecording;
        private ScreenCapture.Controls.BindableToolStripDropDownButton btnCaptureMode;
        private ScreenCapture.Controls.BindableToolStripMenuItem btnCapturePrimaryScreen;
        private ScreenCapture.Controls.BindableToolStripMenuItem btnCaptureWindow;
        private ScreenCapture.Controls.BindableToolStripMenuItem btnCaptureRegion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ScreenCapture.Controls.BindableToolStripButton btnStartPlaying;
        private ScreenCapture.Controls.BindableToolStripButton btnScreenPreview;
        private ScreenCapture.Controls.BindableToolStripButton btnStorageFolder;
        private System.Windows.Forms.Panel panel1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private ScreenCapture.Controls.BindableToolStripButton btnPausePlaying;
        private Controls.HotKey keyPlayPause;
        private Controls.HotKey keyStartStop;
        internal Controls.BindableNotifyIcon iconPlayPause;
        private System.Windows.Forms.ToolStripStatusLabel btnSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

