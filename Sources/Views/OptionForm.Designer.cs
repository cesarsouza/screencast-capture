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
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMouseClicks = new System.Windows.Forms.CheckBox();
            this.cbMouseCursor = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(291, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbCopyright);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(9);
            this.tabPage2.Size = new System.Drawing.Size(518, 283);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbCopyright
            // 
            this.tbCopyright.BackColor = System.Drawing.Color.White;
            this.tbCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCopyright.Location = new System.Drawing.Point(9, 9);
            this.tbCopyright.Name = "tbCopyright";
            this.tbCopyright.ReadOnly = true;
            this.tbCopyright.Size = new System.Drawing.Size(500, 265);
            this.tbCopyright.TabIndex = 0;
            this.tbCopyright.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(518, 283);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Capture";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Screencast Capture Lite";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(17, 254);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(225, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://screencast-capture.googlecode.com";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectFolder);
            this.groupBox2.Controls.Add(this.tbSavePath);
            this.groupBox2.Location = new System.Drawing.Point(34, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 53);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Save Path";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(253, 21);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolder.TabIndex = 3;
            this.btnSelectFolder.Text = "Choose";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbSavePath
            // 
            this.tbSavePath.Location = new System.Drawing.Point(6, 21);
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.Size = new System.Drawing.Size(241, 22);
            this.tbSavePath.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScreenCapture.Properties.Resources.screencast_128;
            this.pictureBox1.Location = new System.Drawing.Point(381, 151);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMouseClicks);
            this.groupBox1.Controls.Add(this.cbMouseCursor);
            this.groupBox1.Location = new System.Drawing.Point(34, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 75);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture";
            // 
            // cbMouseClicks
            // 
            this.cbMouseClicks.AutoSize = true;
            this.cbMouseClicks.Location = new System.Drawing.Point(6, 21);
            this.cbMouseClicks.Name = "cbMouseClicks";
            this.cbMouseClicks.Size = new System.Drawing.Size(93, 17);
            this.cbMouseClicks.TabIndex = 2;
            this.cbMouseClicks.Text = "Mouse Clicks";
            this.cbMouseClicks.UseVisualStyleBackColor = true;
            // 
            // cbMouseCursor
            // 
            this.cbMouseCursor.AutoSize = true;
            this.cbMouseCursor.Location = new System.Drawing.Point(6, 44);
            this.cbMouseCursor.Name = "cbMouseCursor";
            this.cbMouseCursor.Size = new System.Drawing.Size(98, 17);
            this.cbMouseCursor.TabIndex = 2;
            this.cbMouseCursor.Text = "Mouse Cursor";
            this.cbMouseCursor.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(526, 309);
            this.tabControl1.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(453, 321);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(538, 353);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Screencast Options";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
    }
}