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
    using System;
    using System.Windows.Forms;
    using ScreenCapture.ViewModels;
    using ScreenCapture.Properties;

    /// <summary>
    ///   Main window for the Screencast Capture application.
    /// </summary>
    /// 
    public partial class MainForm : Form
    {

        MainViewModel viewModel;

        // Those are the windows shown when the user selects the
        // "Capture Region" or "Capture Window" application modes.
        CaptureRegion regionWindow;
        CaptureWindow windowWindow;


        /// <summary>
        ///   Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// 
        public MainForm()
        {
            InitializeComponent();

            viewModel = new MainViewModel(videoSourcePlayer1);
            regionWindow = new CaptureRegion(viewModel);
            windowWindow = new CaptureWindow(viewModel);

            viewModel.Notify.ShowBalloon += new EventHandler<BalloonEventArgs>(Notify_ShowBalloon);
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            // This section configures all the bindings between 
            // form controls and properties from the view model.
            //
            var binding =
            explorerBrowser.Bind(b => b.CurrentDirectory, viewModel, m => m.CurrentDirectory);
            binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;

            btnStartRecording.Bind(b => b.Enabled, viewModel, m => m.IsPlaying);
            btnStartRecording.Bind(b => b.Visible, viewModel, m => m.IsRecording, value => !value);
            btnStopRecording.Bind(b => b.Visible, viewModel, m => m.IsRecording);
            btnStartPlaying.Bind(b => b.Visible, viewModel, m => m.IsPlaying, value => !value);
            btnPausePlaying.Bind(b => b.Visible, viewModel, m => m.IsPlaying);

            videoSourcePlayer1.Bind(v => v.Visible, viewModel, m => m.IsPreviewVisible);
            explorerBrowser.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible, value => !value);
            btnScreenPreview.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible, value => !value);
            btnStorageFolder.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible);
            explorerBrowser.Bind(b => b.DefaultDirectory, Settings.Default, m => m.DefaultFolder);

            btnCapturePrimaryScreen.Bind(b => b.Checked, viewModel, m => m.CaptureMode, value => value == CaptureRegionOption.Primary);
            btnCaptureRegion.Bind(b => b.Checked, viewModel, m => m.CaptureMode, value => value == CaptureRegionOption.Fixed);
            btnCaptureWindow.Bind(b => b.Checked, viewModel, m => m.CaptureMode, value => value == CaptureRegionOption.Window);

            btnCaptureMode.Bind(b => b.Text, viewModel, m => m.CaptureMode, x => getModeButton(x).Text);
            btnCaptureMode.Bind(b => b.Image, viewModel, m => m.CaptureMode, x => getModeButton(x).Image);

            lbStatusRecording.Bind(b => b.Visible, viewModel, m => m.IsRecording);
            lbStatusReady.Bind(b => b.Visible, viewModel, m => m.IsRecording, value => !value);
            btnCaptureMode.Bind(b => b.Enabled, viewModel, m => m.IsRecording, value => !value);

            iconPlayPause.Bind(b => b.Text, viewModel.Notify, m => m.CurrentText);
            iconPlayPause.Bind(b => b.Icon, viewModel.Notify, m => m.CurrentIcon);

            viewModel.Notify.Loaded();
        }


        private void Notify_ShowBalloon(object sender, BalloonEventArgs e)
        {
            iconPlayPause.ShowBalloonTip(e.Milliseconds, e.Title, e.Text, e.Icon);
        }


        // Video player controls
        private void btnStartPlaying_Click(object sender, EventArgs e)
        {
            viewModel.StartPlaying();
        }

        private void btnPausePlaying_Click(object sender, EventArgs e)
        {
            viewModel.PausePlaying();
        }


        // Video recording controls
        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            viewModel.StartRecording();
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            viewModel.StopRecording();
        }


        // Current visualization controls
        private void btnStorageFolder_Click(object sender, EventArgs e)
        {
            viewModel.IsPreviewVisible = false;
        }

        private void btnScreenPreview_Click(object sender, EventArgs e)
        {
            viewModel.IsPreviewVisible = true;
        }


        // Capture mode controls
        private void btnCapturePrimaryScreen_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Primary;
        }

        private void btnCaptureWindow_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Window;
        }

        private void btnCaptureRegion_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Fixed;
        }

        private ToolStripMenuItem getModeButton(CaptureRegionOption mode)
        {
            switch (mode)
            {
                case CaptureRegionOption.Fixed:
                    return btnCaptureRegion;
                case CaptureRegionOption.Primary:
                    return btnCapturePrimaryScreen;
                case CaptureRegionOption.Window:
                    return btnCaptureWindow;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            regionWindow.Close();
            windowWindow.Close();
            viewModel.Close();
        }




        private void hotkeyPlayPause_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            if (viewModel.IsPlaying)
                viewModel.PausePlaying();
            else viewModel.StartPlaying();
        }

        private void hotkeyStop_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            if (viewModel.IsRecording)
                viewModel.StopRecording();
            else viewModel.StartRecording();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (OptionForm form = new OptionForm())
                form.ShowDialog(this);
        }


    }
}
