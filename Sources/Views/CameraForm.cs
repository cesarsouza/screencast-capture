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
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Accord.Video;
    using Accord.Video.DirectShow;
    using ScreenCapture.ViewModels;
    using System.Linq;

    /// <summary>
    ///   Displays a webcam always on top of the screen.
    /// </summary>
    /// 
    public partial class CameraForm : Form
    {

        MainViewModel viewModel;
        VideoCaptureDevice videoSource;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CameraForm"/> class.
        /// </summary>
        /// 
        /// <param name="viewModel">The view model.</param>
        /// 
        public CameraForm(MainViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CameraForm"/> class.
        /// </summary>
        /// 
        public CameraForm()
        {
            InitializeComponent();
        }

        private void VideoSourcePlayer_Click(object sender, EventArgs e)
        {
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.SignalToStop();
                viewModel.IsWebcamEnabled = false;
                return;
            }

            using (CaptureDeviceDialog form = new CaptureDeviceDialog())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // set busy cursor
                    this.Cursor = Cursors.WaitCursor;

                    Stop();

                    // create video source
                    videoSource = new VideoCaptureDevice(form.VideoDevice);
                    videoSource.VideoResolution = videoSource.VideoCapabilities
                        .Where(x => x.FrameSize == new Size(320, 240)).First();

                    // start new video source
                    videoSourcePlayer.VideoSource = new AsyncVideoSource(videoSource);
                    videoSourcePlayer.Start();

                    Cursor = Cursors.Default;
                    lbClickToConfig.Visible = false;
                    viewModel.IsWebcamEnabled = true;
                }
            }
        }

        private void Stop()
        {
            // set busy cursor
            Cursor = Cursors.WaitCursor;

            // stop current video source
            videoSourcePlayer.SignalToStop();

            // wait 2 seconds until camera stops
            for (int i = 0; i < 50 && videoSourcePlayer.IsRunning; i++)
                Thread.Sleep(100);

            if (videoSourcePlayer.IsRunning)
                videoSourcePlayer.Stop();

            if (videoSource != null)
                videoSource.Dispose();

            Cursor = Cursors.Default;
            videoSourcePlayer.BorderColor = Color.Black;
            viewModel.IsWebcamEnabled = false;
        }

        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
    }
}
