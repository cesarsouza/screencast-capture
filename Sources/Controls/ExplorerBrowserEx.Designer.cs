namespace ScreenCapture.Controls
{
    partial class ExplorerBrowserEx
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browser = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBackward = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.btnParent = new System.Windows.Forms.ToolStripButton();
            this.tbAddress = new ScreenCapture.Controls.BindableToolStripTextBox();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 39);
            this.browser.Name = "browser";
            this.browser.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.browser.Size = new System.Drawing.Size(465, 256);
            this.browser.TabIndex = 1;
            this.browser.NavigationPending += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationPendingEventArgs>(this.browser_NavigationPending);
            this.browser.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.browser_NavigationComplete);
            this.browser.NavigationFailed += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationFailedEventArgs>(this.browser_NavigationFailed);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBackward,
            this.btnForward,
            this.btnParent,
            this.tbAddress,
            this.btnHome});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(465, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBackward
            // 
            this.btnBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackward.Image = global::ScreenCapture.Properties.Resources.player_rew;
            this.btnBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(36, 36);
            this.btnBackward.Text = "Go Back";
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnForward
            // 
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnForward.Image = global::ScreenCapture.Properties.Resources.player_fwd;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(36, 36);
            this.btnForward.Text = "Go Forward";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnParent
            // 
            this.btnParent.AutoSize = false;
            this.btnParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnParent.Image = global::ScreenCapture.Properties.Resources.top;
            this.btnParent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnParent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnParent.Name = "btnParent";
            this.btnParent.Size = new System.Drawing.Size(27, 27);
            this.btnParent.Text = "Go to Parent";
            this.btnParent.Click += new System.EventHandler(this.btnParent_Click);
            // 
            // tbAddress
            // 
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(296, 39);
            this.tbAddress.Spring = true;
            // 
            // btnHome
            // 
            this.btnHome.AutoSize = false;
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHome.Image = global::ScreenCapture.Properties.Resources.home;
            this.btnHome.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(27, 27);
            this.btnHome.Text = "Go to Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // ExplorerBrowserEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.browser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ExplorerBrowserEx";
            this.Size = new System.Drawing.Size(465, 295);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser browser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnBackward;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripButton btnParent;
        private BindableToolStripTextBox tbAddress;
        private System.Windows.Forms.ToolStripButton btnHome;
    }
}
