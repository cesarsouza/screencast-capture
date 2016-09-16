namespace ScreenCapture.Views
{
    partial class CameraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            this.videoSourcePlayer = new Accord.Controls.VideoSourcePlayer();
            this.lbClickToConfig = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // videoSourcePlayer
            // 
            resources.ApplyResources(this.videoSourcePlayer, "videoSourcePlayer");
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            this.videoSourcePlayer.Click += new System.EventHandler(this.videoSourcePlayer_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.lbClickToConfig, "label1");
            this.lbClickToConfig.ForeColor = System.Drawing.Color.White;
            this.lbClickToConfig.Name = "label1";
            this.lbClickToConfig.Click += new System.EventHandler(this.videoSourcePlayer_Click);
            // 
            // CameraForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lbClickToConfig);
            this.Controls.Add(this.videoSourcePlayer);
            this.Name = "CameraForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private Accord.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.Label lbClickToConfig;
    }
}