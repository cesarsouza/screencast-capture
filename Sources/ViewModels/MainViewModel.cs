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

namespace ScreenCapture.ViewModels
{
    using AForge.Controls;
    using AForge.Imaging.Filters;
    using AForge.Video;
    using AForge.Video.FFMPEG;
    using ScreenCapture.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    ///   Region capturing modes.
    /// </summary>
    /// 
    public enum CaptureRegionOption
    {
        /// <summary>
        ///   Captures from a fixed region on the screen.
        /// </summary>
        /// 
        Fixed,

        /// <summary>
        ///   Captures only from the primary screen.
        /// </summary>
        /// 
        Primary,

        /// <summary>
        ///   Captures from the current window.
        /// </summary>
        Window
    }

    /// <summary>
    ///   Main ViewModel to control the application.
    /// </summary>
    /// 
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {

        public ScreenCaptureStream ScreenStream { get; private set; }
        public VideoFileWriter VideoWriter { get; private set; }
        public VideoSourcePlayer Player { get; private set; }

        public IconViewModel Icons { get; private set; }


        public string CurrentDirectory { get; set; }
        public string CurrentFileName { get; private set; }
        public Rectangle CurrentRegion { get; set; }
        public IntPtr CurrentWindowHandle { get; set; }

        public CaptureRegionOption CaptureMode { get; set; }

        public DateTime RecordingStartTime { get; private set; }
        public TimeSpan RecordingDuration { get; private set; }

        public bool IsChoosingTarget { get; private set; }
        public bool IsRecording { get; private set; }
        public bool IsPlaying { get; private set; }

        public bool IsFramesVisible { get; private set; }
        public bool IsPreviewVisible { get; set; }

        public bool CaptureMouse { get; set; }

        public string Status { get; private set; }

        private Crop crop = new Crop(Rectangle.Empty);
        private CaptureCursor cursorCapture;

        public event EventHandler TargetWindowRequested;

        private object syncObj = new object();


        public MainViewModel(VideoSourcePlayer player)
        {
            Player = player;
            CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            CaptureMode = CaptureRegionOption.Primary;
            CaptureMouse = true;

            Player.NewFrame += new VideoSourcePlayer.NewFrameHandler(Player_NewFrame);
            PropertyChanged += new PropertyChangedEventHandler(MainViewModel_PropertyChanged);

            IsPreviewVisible = true;
            IsPlaying = false;
            IsRecording = false;
            IsFramesVisible = false;

            cursorCapture = new CaptureCursor();
            
            CurrentRegion = new Rectangle(0, 0, 640, 480);
            Icons = new IconViewModel(this);

            Status = "Ready";
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CaptureMode" || e.PropertyName == "IsPlaying")
                IsFramesVisible = (IsPlaying && CaptureMode == CaptureRegionOption.Fixed);
        }


        public void StartPlaying()
        {
            if (IsPlaying) return;

            if (!IsChoosingTarget)
            {
                if (CaptureMode == CaptureRegionOption.Window)
                {
                    IsChoosingTarget = true;
                    if (TargetWindowRequested != null)
                        TargetWindowRequested(this, EventArgs.Empty);
                    return;
                }
            }
            else
            {
                IsChoosingTarget = false;
            }

            CurrentRegion = Screen.PrimaryScreen.Bounds;

            int height = CurrentRegion.Height;
            int width = CurrentRegion.Width;
            int framerate = 24;

            ScreenStream = new ScreenCaptureStream(CurrentRegion, 1000 / framerate);
            Player.VideoSource = ScreenStream;
            Player.Start();

            IsPlaying = true;
        }

        public void StartRecording()
        {
            if (IsRecording || !IsPlaying) return;

            Rectangle area = CurrentRegion;
            CurrentFileName = newFileName();

            int height = area.Height;
            int width = area.Width;
            int framerate = 24;
            int bitrate = 10 * 1000 * 1000;

            string path = Path.Combine(CurrentDirectory, CurrentFileName);

            RecordingStartTime = DateTime.MinValue;
            VideoWriter = new VideoFileWriter();
            VideoWriter.Open(path, width, height, framerate, VideoCodec.H264, bitrate);

            IsRecording = true;
        }

        public void PausePlaying()
        {
            if (!IsPlaying)
                return;

            Player.SignalToStop();
            IsPlaying = false;
        }

        public void StopRecording()
        {
            if (!IsRecording)
                return;

            lock (syncObj)
            {
                if (VideoWriter != null && VideoWriter.IsOpen)
                    VideoWriter.Close();

                IsRecording = false;
            }
        }


        public void GetWindowUnderCursor()
        {
            CurrentWindowHandle = NativeMethods.WindowFromPoint(Cursor.Position);

            if (IsChoosingTarget)
                StartPlaying();
        }

        public void Close()
        {
            Player.SignalToStop();
            Player.WaitForStop();

            if (VideoWriter != null && VideoWriter.IsOpen)
                VideoWriter.Close();
        }



        private void Player_NewFrame(object sender, ref Bitmap image)
        {
            if (CaptureMouse)
            {
                Bitmap cursor = cursorCapture.GetBitmap();

                if (cursor != null)
                {
                    using (Graphics g = Graphics.FromImage(image))
                        g.DrawImage(cursor, cursorCapture.Position);
                }
            }


            CurrentRegion = adjustWindow();

            if (CaptureMode == CaptureRegionOption.Fixed ||
                CaptureMode == CaptureRegionOption.Window)
            {
                crop.Rectangle = CurrentRegion;
                image = crop.Apply(image);
            }


            lock (syncObj)
            {
                if (IsRecording)
                {
                    if (RecordingStartTime == DateTime.MinValue)
                        RecordingStartTime = DateTime.Now;

                    RecordingDuration = DateTime.Now - RecordingStartTime;
                    VideoWriter.WriteVideoFrame(image, RecordingDuration);
                }
            }
        }

        private Rectangle adjustWindow()
        {
            Rectangle area = CurrentRegion;

            if (CaptureMode == CaptureRegionOption.Window && !IsRecording)
                area = NativeMethods.GetWindowRect(CurrentWindowHandle);
            else if (CaptureMode == CaptureRegionOption.Primary)
                area = Screen.PrimaryScreen.Bounds;

            if (area.Width % 2 != 0)
                area.Width++;
            if (area.Height % 2 != 0)
                area.Height++;

            return area;
        }



        private string newFileName()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd-HH'h'mm'm'ss's'",
                System.Globalization.CultureInfo.CurrentCulture);

            string mode = String.Empty;
            if (CaptureMode == CaptureRegionOption.Primary)
                mode = "PrimaryScreen_";
            else if (CaptureMode == CaptureRegionOption.Fixed)
                mode = "Region_";
            else if (CaptureMode == CaptureRegionOption.Window)
                mode = "Window_";

            string name = mode + date + ".avi";

            return name;
        }


        #region IDisposable implementation

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, 
        ///   releasing, or resetting unmanaged resources.
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources and performs other cleanup operations 
        ///   before the <see cref="MainViewModel"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~MainViewModel()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// 
        /// <param name="disposing"><c>true</c> to release both managed
        /// and unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        ///
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (cursorCapture != null)
                {
                    cursorCapture.Dispose();
                    cursorCapture = null;
                }
            }
        }
        #endregion


        // The PropertyChanged event doesn't needs to be explicitly raised
        // from this application. The event raising is handled automatically
        // by the NotifyPropertyWeaver VS extension using IL injection.
        //
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
    }
}
