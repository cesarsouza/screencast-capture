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
            this.btnCaptureMode = new ScreenCapture.Controls.BindableToolStripDropDownButton();
            this.btnCapturePrimaryScreen = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnCaptureWindow = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnCaptureRegion = new ScreenCapture.Controls.BindableToolStripMenuItem();
            this.btnStartRecording = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStopRecording = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnPausePlaying = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStartPlaying = new ScreenCapture.Controls.BindableToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnScreenPreview = new ScreenCapture.Controls.BindableToolStripButton();
            this.btnStorageFolder = new ScreenCapture.Controls.BindableToolStripButton();
            this.explorerBrowser = new ScreenCapture.Controls.ExplorerBrowserCustom();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbStatusRecording = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.lbStatusTime = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.lbStatusReady = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
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
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // btnCaptureMode
            // 
            this.btnCaptureMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCapturePrimaryScreen,
            this.btnCaptureWindow,
            this.btnCaptureRegion});
            this.btnCaptureMode.Image = global::ScreenCapture.Properties.Resources.ksnapshot;
            resources.ApplyResources(this.btnCaptureMode, "btnCaptureMode");
            this.btnCaptureMode.Name = "btnCaptureMode";
            // 
            // btnCapturePrimaryScreen
            // 
            this.btnCapturePrimaryScreen.Checked = true;
            this.btnCapturePrimaryScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnCapturePrimaryScreen.Image = global::ScreenCapture.Properties.Resources.ksnapshot;
            this.btnCapturePrimaryScreen.Name = "btnCapturePrimaryScreen";
            resources.ApplyResources(this.btnCapturePrimaryScreen, "btnCapturePrimaryScreen");
            this.btnCapturePrimaryScreen.Click += new System.EventHandler(this.btnCapturePrimaryScreen_Click);
            // 
            // btnCaptureWindow
            // 
            this.btnCaptureWindow.Image = global::ScreenCapture.Properties.Resources.kpersonalizer;
            this.btnCaptureWindow.Name = "btnCaptureWindow";
            resources.ApplyResources(this.btnCaptureWindow, "btnCaptureWindow");
            this.btnCaptureWindow.Click += new System.EventHandler(this.btnCaptureWindow_Click);
            // 
            // btnCaptureRegion
            // 
            this.btnCaptureRegion.Image = global::ScreenCapture.Properties.Resources.kview;
            this.btnCaptureRegion.Name = "btnCaptureRegion";
            resources.ApplyResources(this.btnCaptureRegion, "btnCaptureRegion");
            this.btnCaptureRegion.Click += new System.EventHandler(this.btnCaptureRegion_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStartRecording.Image = global::ScreenCapture.Properties.Resources.camera;
            resources.ApplyResources(this.btnStartRecording, "btnStartRecording");
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStopRecording.Image = global::ScreenCapture.Properties.Resources.player_stop;
            resources.ApplyResources(this.btnStopRecording, "btnStopRecording");
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnPausePlaying
            // 
            this.btnPausePlaying.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPausePlaying.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPausePlaying.Image = global::ScreenCapture.Properties.Resources.player_pause;
            resources.ApplyResources(this.btnPausePlaying, "btnPausePlaying");
            this.btnPausePlaying.Name = "btnPausePlaying";
            this.btnPausePlaying.Click += new System.EventHandler(this.btnPausePlaying_Click);
            // 
            // btnStartPlaying
            // 
            this.btnStartPlaying.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStartPlaying.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartPlaying.Image = global::ScreenCapture.Properties.Resources.player_play;
            resources.ApplyResources(this.btnStartPlaying, "btnStartPlaying");
            this.btnStartPlaying.Name = "btnStartPlaying";
            this.btnStartPlaying.Click += new System.EventHandler(this.btnStartPlaying_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnScreenPreview
            // 
            this.btnScreenPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnScreenPreview.Image = global::ScreenCapture.Properties.Resources.desktop;
            resources.ApplyResources(this.btnScreenPreview, "btnScreenPreview");
            this.btnScreenPreview.Name = "btnScreenPreview";
            this.btnScreenPreview.Click += new System.EventHandler(this.btnScreenPreview_Click);
            // 
            // btnStorageFolder
            // 
            this.btnStorageFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStorageFolder.Image = global::ScreenCapture.Properties.Resources.folder_video;
            resources.ApplyResources(this.btnStorageFolder, "btnStorageFolder");
            this.btnStorageFolder.Name = "btnStorageFolder";
            this.btnStorageFolder.Click += new System.EventHandler(this.btnStorageFolder_Click);
            // 
            // explorerBrowser
            // 
            this.explorerBrowser.CurrentDirectory = null;
            this.explorerBrowser.DefaultDirectory = "C:\\Users\\Cesar\\Desktop";
            resources.ApplyResources(this.explorerBrowser, "explorerBrowser");
            this.explorerBrowser.Name = "explorerBrowser";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusRecording,
            this.lbStatusTime,
            this.lbStatusReady,
            this.toolStripStatusLabel1,
            this.btnSettings});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // lbStatusRecording
            // 
            resources.ApplyResources(this.lbStatusRecording, "lbStatusRecording");
            this.lbStatusRecording.Name = "lbStatusRecording";
            // 
            // lbStatusTime
            // 
            this.lbStatusTime.Name = "lbStatusTime";
            resources.ApplyResources(this.lbStatusTime, "lbStatusTime");
            // 
            // lbStatusReady
            // 
            this.lbStatusReady.Name = "lbStatusReady";
            resources.ApplyResources(this.lbStatusReady, "lbStatusReady");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Spring = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::ScreenCapture.Properties.Resources.advancedsettings;
            this.btnSettings.Name = "btnSettings";
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.videoSourcePlayer1);
            this.panel1.Controls.Add(this.explorerBrowser);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.videoSourcePlayer1, "videoSourcePlayer1");
            this.videoSourcePlayer1.KeepAspectRatio = true;
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
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
            this.iconPlayPause.Click += new System.EventHandler(this.iconPlayPause_Click);
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
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
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

        private ScreenCapture.Controls.ExplorerBrowserCustom explorerBrowser;
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
        private ScreenCapture.Controls.BindableToolStripStatusLabel lbStatusTime;
    }
}

