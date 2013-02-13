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
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.tbCopyright = new System.Windows.Forms.RichTextBox();
            this.tabCapture = new System.Windows.Forms.TabPage();
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
            this.tabAudioVideo = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbConversion = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.btnSizeDown = new System.Windows.Forms.Button();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.btnSizeUp = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAudioRate = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbContainer = new System.Windows.Forms.ComboBox();
            this.cbFrameRate = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDonate = new System.Windows.Forms.Button();
            this.tabAbout.SuspendLayout();
            this.tabCapture.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAudioVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // tabAbout
            // 
            resources.ApplyResources(this.tabAbout, "tabAbout");
            this.tabAbout.Controls.Add(this.tbCopyright);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // tbCopyright
            // 
            resources.ApplyResources(this.tbCopyright, "tbCopyright");
            this.tbCopyright.BackColor = System.Drawing.Color.White;
            this.tbCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCopyright.Name = "tbCopyright";
            this.tbCopyright.ReadOnly = true;
            // 
            // tabCapture
            // 
            resources.ApplyResources(this.tabCapture, "tabCapture");
            this.tabCapture.Controls.Add(this.lbVersion);
            this.tabCapture.Controls.Add(this.linkLabel1);
            this.tabCapture.Controls.Add(this.groupBox2);
            this.tabCapture.Controls.Add(this.pictureBox1);
            this.tabCapture.Controls.Add(this.groupBox1);
            this.tabCapture.Controls.Add(this.label1);
            this.tabCapture.Name = "tabCapture";
            this.tabCapture.UseVisualStyleBackColor = true;
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
            this.tabControl1.Controls.Add(this.tabCapture);
            this.tabControl1.Controls.Add(this.tabAudioVideo);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabAudioVideo
            // 
            resources.ApplyResources(this.tabAudioVideo, "tabAudioVideo");
            this.tabAudioVideo.Controls.Add(this.pictureBox2);
            this.tabAudioVideo.Controls.Add(this.groupBox4);
            this.tabAudioVideo.Controls.Add(this.groupBox6);
            this.tabAudioVideo.Controls.Add(this.groupBox5);
            this.tabAudioVideo.Controls.Add(this.groupBox3);
            this.tabAudioVideo.Name = "tabAudioVideo";
            this.tabAudioVideo.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::ScreenCapture.Properties.Resources.aktion1;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
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
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.cbFont);
            this.groupBox6.Controls.Add(this.btnSizeDown);
            this.groupBox6.Controls.Add(this.numFontSize);
            this.groupBox6.Controls.Add(this.btnSizeUp);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnPreviewOSD_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cbFont
            // 
            resources.ApplyResources(this.cbFont, "cbFont");
            this.cbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Name = "cbFont";
            this.cbFont.SelectedIndexChanged += new System.EventHandler(this.cbFont_SelectedIndexChanged);
            // 
            // btnSizeDown
            // 
            resources.ApplyResources(this.btnSizeDown, "btnSizeDown");
            this.btnSizeDown.Name = "btnSizeDown";
            this.btnSizeDown.UseVisualStyleBackColor = true;
            this.btnSizeDown.Click += new System.EventHandler(this.btnSizeDown_Click);
            // 
            // numFontSize
            // 
            resources.ApplyResources(this.numFontSize, "numFontSize");
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // btnSizeUp
            // 
            resources.ApplyResources(this.btnSizeUp, "btnSizeUp");
            this.btnSizeUp.Name = "btnSizeUp";
            this.btnSizeUp.UseVisualStyleBackColor = true;
            this.btnSizeUp.Click += new System.EventHandler(this.btnSizeUp_Click);
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.cbAudioRate);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cbAudioRate
            // 
            resources.ApplyResources(this.cbAudioRate, "cbAudioRate");
            this.cbAudioRate.FormattingEnabled = true;
            this.cbAudioRate.Items.AddRange(new object[] {
            resources.GetString("cbAudioRate.Items"),
            resources.GetString("cbAudioRate.Items1")});
            this.cbAudioRate.Name = "cbAudioRate";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabAbout.ResumeLayout(false);
            this.tabCapture.ResumeLayout(false);
            this.tabCapture.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabAudioVideo.ResumeLayout(false);
            this.tabAudioVideo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.TabPage tabCapture;
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
        private System.Windows.Forms.Button btnDonate;
        private System.Windows.Forms.TabPage tabAudioVideo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbContainer;
        private System.Windows.Forms.ComboBox cbFrameRate;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.Button btnSizeDown;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Button btnSizeUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbAudioRate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbConversion;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}