// Screencast Capture, free screen recorder
// http://screencast-capture.googlecode.com
//
// Copyright © César Souza, 2012-2013
// cesarsouza at gmail.com
//
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation; either version 2 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// 

namespace ScreenCapture.Views
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using ScreenCapture.Properties;
    using ScreenCapture.ViewModels;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    ///   Options dialog.
    /// </summary>
    /// 
    public partial class OptionForm : Form
    {

        OptionViewModel viewModel = new OptionViewModel();


        /// <summary>
        ///   Initializes a new instance of the <see cref="OptionForm"/> class.
        /// </summary> 
        /// 
        public OptionForm()
        {
            InitializeComponent();
        }


        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbContainer.DataSource = OptionViewModel.SupportedContainers;

            tbSavePath.Bind(b => b.Text, viewModel, m => m.DefaultSaveFolder);
            cbMouseCursor.Bind(b => b.Checked, viewModel, m => m.CaptureMouse);
            cbMouseClicks.Bind(b => b.Checked, viewModel, m => m.CaptureClick);
            cbKeyboard.Bind(b => b.Checked, viewModel, m => m.CaptureKeys);
            cbFrameRate.Bind(b => b.Text, viewModel, m => m.FrameRate);
            cbContainer.Bind(b => b.Text, viewModel, m => m.Container);
            cbConversion.Bind(b => b.Checked, viewModel, m => m.AutoConversionDialog);
            
            showCopyrightText();

            lbVersion.Text = String.Format(CultureInfo.CurrentCulture, Resources.About_Version,
                Assembly.GetExecutingAssembly().GetName().Version);
        }


        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.EnsurePathExists = true;
                dialog.InitialDirectory = tbSavePath.Text;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    viewModel.DefaultSaveFolder = dialog.FileName;
                }
            }
        }


        private void showCopyrightText()
        {
            // TODO: Move this code to a better location

            String copyright = @"..\Copyright.txt";

            if (!File.Exists(copyright))
            {
                copyright = @"..\..\..\Copyright.txt";

                if (!File.Exists(copyright))
                {
                    tbCopyright.Text = Resources.Error_Missing_Copyright;
                    return;
                }
            }

            using (TextReader reader = new StreamReader(copyright, Encoding.Default))
            {
                tbCopyright.Text = reader.ReadToEnd();
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            viewModel.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            viewModel.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://code.google.com/p/screencast-capture/");
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MPU4U4NZZSG86");
        }

    }
}
