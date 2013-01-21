namespace ScreenCapture.Views
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbCopyright = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbConversion = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbContainer = new System.Windows.Forms.ComboBox();
            this.cbFrameRate = new System.Windows.Forms.ComboBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMouseClicks = new System.Windows.Forms.CheckBox();
            this.cbKeyboard = new System.Windows.Forms.CheckBox();
            this.cbMouseCursor = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDonate = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.tbCopyright);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbCopyright
            // 
            resources.ApplyResources(this.tbCopyright, "tbCopyright");
            this.tbCopyright.BackColor = System.Drawing.Color.White;
            this.tbCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCopyright.Name = "tbCopyright";
            this.tbCopyright.ReadOnly = true;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.lbVersion);
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.cbConversion);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // cbConversion
            // 
            resources.ApplyResources(this.cbConversion, "cbConversion");
            this.cbConversion.Name = "cbConversion";
            this.cbConversion.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbContainer);
            this.groupBox3.Controls.Add(this.cbFrameRate);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbContainer
            // 
            resources.ApplyResources(this.cbContainer, "cbContainer");
            this.cbContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContainer.FormattingEnabled = true;
            this.cbContainer.Name = "cbContainer";
            // 
            // cbFrameRate
            // 
            resources.ApplyResources(this.cbFrameRate, "cbFrameRate");
            this.cbFrameRate.FormattingEnabled = true;
            this.cbFrameRate.Items.AddRange(new object[] {
            resources.GetString("cbFrameRate.Items"),
            resources.GetString("cbFrameRate.Items1"),
            resources.GetString("cbFrameRate.Items2"),
            resources.GetString("cbFrameRate.Items3"),
            resources.GetString("cbFrameRate.Items4")});
            this.cbFrameRate.Name = "cbFrameRate";
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbVersion.Name = "lbVersion";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnSelectFolder);
            this.groupBox2.Controls.Add(this.tbSavePath);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnSelectFolder
            // 
            resources.ApplyResources(this.btnSelectFolder, "btnSelectFolder");
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbSavePath
            // 
            resources.ApplyResources(this.tbSavePath, "tbSavePath");
            this.tbSavePath.Name = "tbSavePath";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::ScreenCapture.Properties.Resources.screencast_128;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cbMouseClicks);
            this.groupBox1.Controls.Add(this.cbKeyboard);
            this.groupBox1.Controls.Add(this.cbMouseCursor);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbMouseClicks
            // 
            resources.ApplyResources(this.cbMouseClicks, "cbMouseClicks");
            this.cbMouseClicks.Name = "cbMouseClicks";
            this.cbMouseClicks.UseVisualStyleBackColor = true;
            // 
            // cbKeyboard
            // 
            resources.ApplyResources(this.cbKeyboard, "cbKeyboard");
            this.cbKeyboard.Name = "cbKeyboard";
            this.cbKeyboard.UseVisualStyleBackColor = true;
            // 
            // cbMouseCursor
            // 
            resources.ApplyResources(this.cbMouseCursor, "cbMouseCursor");
            this.cbMouseCursor.Name = "cbMouseCursor";
            this.cbMouseCursor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnDonate
            // 
            resources.ApplyResources(this.btnDonate, "btnDonate");
            this.btnDonate.BackColor = System.Drawing.Color.Transparent;
            this.btnDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDonate.FlatAppearance.BorderSize = 0;
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.UseVisualStyleBackColor = false;
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnDonate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbMouseClicks;
        private System.Windows.Forms.CheckBox cbMouseCursor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox tbCopyright;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox cbKeyboard;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFrameRate;
        private System.Windows.Forms.Button btnDonate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbContainer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbConversion;
    }
}