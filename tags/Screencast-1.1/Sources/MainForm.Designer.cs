namespace ScreenCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.explorerBrowser = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatusRecording = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.lbStatusReady = new ScreenCapture.Controls.BindableToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCaptureMode,
            this.btnStartRecording,
            this.btnStopRecording,
            this.btnPausePlaying,
            this.btnStartPlaying,
            this.toolStripSeparator1,
            this.btnScreenPreview,
            this.btnStorageFolder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(656, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            this.btnPausePlaying.Text = "toolStripButton1";
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
            this.btnStartPlaying.Text = "toolStripButton1";
            this.btnStartPlaying.ToolTipText = "Start playing the capture preview";
            this.btnStartPlaying.Click += new System.EventHandler(this.btnStartPlaying_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
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
            // explorerBrowser
            // 
            this.explorerBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerBrowser.Location = new System.Drawing.Point(0, 0);
            this.explorerBrowser.Name = "explorerBrowser";
            this.explorerBrowser.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.explorerBrowser.Size = new System.Drawing.Size(656, 342);
            this.explorerBrowser.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusRecording,
            this.lbStatusReady});
            this.statusStrip1.Location = new System.Drawing.Point(0, 381);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(656, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatusRecording
            // 
            this.lbStatusRecording.Image = ((System.Drawing.Image)(resources.GetObject("lbStatusRecording.Image")));
            this.lbStatusRecording.Name = "lbStatusRecording";
            this.lbStatusRecording.Size = new System.Drawing.Size(89, 22);
            this.lbStatusRecording.Text = "Recording";
            this.lbStatusRecording.Visible = false;
            // 
            // lbStatusReady
            // 
            this.lbStatusReady.Name = "lbStatusReady";
            this.lbStatusReady.Size = new System.Drawing.Size(51, 22);
            this.lbStatusReady.Text = "Ready";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Screencast Capture Lite";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser explorerBrowser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ScreenCapture.Controls.BindableToolStripButton btnStartRecording;
        private System.Windows.Forms.StatusStrip statusStrip1;
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
    }
}

