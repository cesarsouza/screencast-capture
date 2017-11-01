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
    using System.Globalization;
    using System.Windows.Forms;
    using Accord.DirectSound;
    using ScreenCapture.Properties;
    using ScreenCapture.ViewModels;
    using ScreenCapture.Controls;

    /// <summary>
    ///   Main window for the Screencast Capture application.
    /// </summary>
    /// 
    public partial class MainForm : Form
    {

        MainViewModel viewModel;

        // Those are the windows shown when the user selects the
        // "Capture Region" or "Capture Window" application modes.
        //
        CaptureRegion regionWindow;
        CaptureWindow windowWindow;

        CameraForm webcamWindow;


        /// <summary>
        ///   Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// 
        public MainForm()
        {
            InitializeComponent();

            viewModel = new MainViewModel(videoSourcePlayer1);
            regionWindow = new CaptureRegion(viewModel.Recorder);
            windowWindow = new CaptureWindow(viewModel.Recorder);
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            // This section configures all the bindings between 
            // form controls and properties from the view model.
            //
            explorerBrowser.Bind(b => b.CurrentDirectory, viewModel, m => m.CurrentDirectory);
            explorerBrowser.Bind(b => b.CurrentFileName, viewModel, m => m.CurrentSelection);
            explorerBrowser.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible, value => !value);
            explorerBrowser.Bind(b => b.DefaultDirectory, Settings.Default, m => m.DefaultFolder);

            bindToolStrip();
            bindStatusBar();

            iconPlayPause.Bind(b => b.Text, viewModel.Notifier, m => m.CurrentText);
            iconPlayPause.Bind(b => b.Icon, viewModel.Notifier, m => m.CurrentIcon);

            viewModel.ShowConversionDialog += Converter_ShowDialog;
            viewModel.Notifier.ShowBalloon += Notifier_ShowBalloon;

            viewModel.Notifier.Loaded();
        }




        private void bindToolStrip()
        {
            btnStartRecording.Bind(b => b.Enabled, viewModel, m => m.IsRecordingEnabled);
            btnStartRecording.Bind(b => b.Visible, viewModel.Recorder, m => m.IsRecording, value => !value);
            btnStopRecording.Bind(b => b.Visible, viewModel.Recorder, m => m.IsRecording);
            btnStartPlaying.Bind(b => b.Visible, viewModel.Recorder, m => m.IsPlaying, value => !value);
            btnPausePlaying.Bind(b => b.Visible, viewModel.Recorder, m => m.IsPlaying);

            videoSourcePlayer1.Bind(v => v.Visible, viewModel, m => m.IsPreviewVisible);
            btnScreenPreview.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible, value => !value);
            btnStorageFolder.Bind(b => b.Visible, viewModel, m => m.IsPreviewVisible);

            btnCapturePrimaryScreen.Bind(b => b.Checked, viewModel.Recorder, m => m.CaptureMode, value => value == CaptureRegionOption.Primary);
            btnCaptureRegion.Bind(b => b.Checked, viewModel.Recorder, m => m.CaptureMode, value => value == CaptureRegionOption.Fixed);
            btnCaptureWindow.Bind(b => b.Checked, viewModel.Recorder, m => m.CaptureMode, value => value == CaptureRegionOption.Window);

            btnCaptureMode.Bind(b => b.Text, viewModel.Recorder, m => m.CaptureMode, x => getModeButton(x).Text);
            btnCaptureMode.Bind(b => b.Image, viewModel.Recorder, m => m.CaptureMode, x => getModeButton(x).Image);
            btnCaptureMode.Bind(b => b.Enabled, viewModel.Recorder, m => m.IsRecording, value => !value);
        }

        private void bindStatusBar()
        {
            lbStatusRecording.Bind(b => b.Visible, viewModel.Recorder, m => m.IsRecording);
            lbStatusTime.Bind(b => b.Visible, viewModel.Recorder, m => m.IsRecording);
            lbStatusTime.Bind(b => b.Text, viewModel.Recorder, m => m.RecordingDuration, value => value.ToString(@"hh\:mm\:ss", CultureInfo.CurrentCulture));
            lbStatusReady.Bind(b => b.Visible, viewModel.Recorder, m => m.IsRecording, value => !value);
            lbStatusReady.Bind(b => b.Text, viewModel, m => m.StatusText);

            btnConvert.Bind(b => b.Visible, viewModel, m => m.IsConversionVisible);
            btnCancel.Bind(b => b.Visible, viewModel.Converter, m => m.IsConverting);
            toolStripProgressBar1.ProgressBar.Bind(b => b.Visible, viewModel.Converter, m => m.IsConverting);
            toolStripProgressBar1.ProgressBar.Bind(b => b.Value, viewModel.Converter, m => m.Progress);

            lbSeparator.Bind(b => b.Visible, b => btnConvert.VisibleChanged += b, () => btnCancel.Visible || btnConvert.Visible);
            lbSeparator.Bind(b => b.Visible, b => btnCancel.VisibleChanged += b, () => btnCancel.Visible || btnConvert.Visible);

            btnWebcam.Bind(b => b.Image, viewModel, m => m.IsWebcamEnabled, value => value ? Resources.agt_family : Resources.agt_family_off);
            btnAudio.Bind(b => b.Enabled, viewModel.Recorder, m => m.IsRecording, value => !value);
            btnAudio.Bind(b => b.Image, viewModel.Recorder, m => m.AudioCaptureDevices, c => c == null ? Resources.kmixdocked_mute : Resources.kmixdocked);
            btnNoAudio.Bind(b => b.Checked, viewModel.Recorder, m => m.AudioCaptureDevices, value => value == null);

            // Create audio menu items for each audio device
            foreach (var dev in viewModel.Recorder.AudioCaptureDevices)
            {
                var item = new BindableToolStripMenuItem(dev.DeviceInfo.Description);
                item.Tag = dev; item.Click += btnAudioDevice_Click;
                item.Bind(b => b.Checked, dev, m => m.Checked);
                btnAudio.DropDownItems.Add(item);
            }
        }



        // Video player controls
        private void btnStartPlaying_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.StartPlaying();
        }

        private void btnPausePlaying_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.PausePlaying();
        }


        // Video recording controls
        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.StartRecording();
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.StopRecording();
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
            viewModel.Recorder.CaptureMode = CaptureRegionOption.Primary;
        }

        private void btnCaptureWindow_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.CaptureMode = CaptureRegionOption.Window;
        }

        private void btnCaptureRegion_Click(object sender, EventArgs e)
        {
            viewModel.Recorder.CaptureMode = CaptureRegionOption.Fixed;
        }


        // Global HotKey actions
        private void hotkeyPlayPause_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            if (viewModel.Recorder.IsPlaying)
                viewModel.Recorder.PausePlaying();
            else viewModel.Recorder.StartPlaying();
        }

        private void hotkeyStop_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            if (viewModel.Recorder.IsRecording)
                viewModel.Recorder.StopRecording();
            else viewModel.Recorder.StartRecording();
        }


        // Notification area icons
        private void iconPlayPause_Click(object sender, EventArgs e)
        {
            viewModel.Notifier.Click();
        }

        private void Notifier_ShowBalloon(object sender, BalloonEventArgs e)
        {
            iconPlayPause.ShowBalloonTip(e.Milliseconds, e.Title, e.Text, e.Icon);
        }


        // Status bar controls
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (OptionForm form = new OptionForm())
                form.ShowDialog(this);
        }

        private void Converter_ShowDialog(object sender, EventArgs e)
        {
            using (ConvertFormatDialog form = new ConvertFormatDialog(viewModel.Converter))
                form.ShowDialog(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            viewModel.Converter.Cancel();
        }

        private void btnAudioDevice_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            var device = item.Tag as AudioCaptureDeviceViewModel;

            if (device != null)
                device.Checked = !device.Checked;

            else
            {
                var devices = viewModel.Recorder.AudioCaptureDevices;
                foreach (AudioCaptureDeviceViewModel dev in devices)
                    dev.Checked = false;
            }
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            regionWindow.Close();
            windowWindow.Close();
            viewModel.Recorder.Close();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // this.videoSourcePlayer1.Visible = (WindowState != FormWindowState.Minimized);
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

        private void btnWebcam_Click(object sender, EventArgs e)
        {
            if (webcamWindow == null || webcamWindow.IsDisposed)
            {
                webcamWindow = new CameraForm(viewModel);
                webcamWindow.Show(this);
            }
        }

    }
}
