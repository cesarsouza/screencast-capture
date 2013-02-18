namespace ScreenCapture.Controls
{
    partial class ExplorerBrowserCustom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerBrowserCustom));
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
            resources.ApplyResources(this.browser, "browser");
            this.browser.Name = "browser";
            this.browser.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.browser.SelectionChanged += new System.EventHandler(this.browser_SelectionChanged);
            this.browser.NavigationPending += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationPendingEventArgs>(this.browser_NavigationPending);
            this.browser.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.browser_NavigationComplete);
            this.browser.NavigationFailed += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationFailedEventArgs>(this.browser_NavigationFailed);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBackward,
            this.btnForward,
            this.btnParent,
            this.tbAddress,
            this.btnHome});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnBackward
            // 
            resources.ApplyResources(this.btnBackward, "btnBackward");
            this.btnBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackward.Image = global::ScreenCapture.Properties.Resources.player_rew;
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnForward
            // 
            resources.ApplyResources(this.btnForward, "btnForward");
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnForward.Image = global::ScreenCapture.Properties.Resources.player_fwd;
            this.btnForward.Name = "btnForward";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnParent
            // 
            resources.ApplyResources(this.btnParent, "btnParent");
            this.btnParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnParent.Image = global::ScreenCapture.Properties.Resources.top;
            this.btnParent.Name = "btnParent";
            this.btnParent.Click += new System.EventHandler(this.btnParent_Click);
            // 
            // tbAddress
            // 
            resources.ApplyResources(this.tbAddress, "tbAddress");
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Spring = true;
            // 
            // btnHome
            // 
            resources.ApplyResources(this.btnHome, "btnHome");
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHome.Image = global::ScreenCapture.Properties.Resources.home;
            this.btnHome.Name = "btnHome";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // ExplorerBrowserCustom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.browser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ExplorerBrowserCustom";
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
