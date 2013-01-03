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
    using ScreenCapture.Properties;
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

        /// <summary>
        ///   Gets the notification area view-model.
        /// </summary>
        /// 
        public NotifyViewModel Notify { get; private set; }



        /// <summary>
        ///   Gets the current directory in the file browser.
        /// </summary>
        /// 
        public string CurrentDirectory { get; set; }

        /// <summary>
        ///   If currently recording a video, gets the filename of the current
        ///   current video. If not, gets the filename of the last recorded video.
        /// </summary>
        /// 
        public string CurrentFileName { get; private set; }

        /// <summary>
        ///   Gets or sets the current capture mode, if the capture area
        ///   should be the whole screen, a fixed region or a fixed window.
        /// </summary>
        /// 
        public CaptureRegionOption CaptureMode
        {
            get { return captureMode; }
            set { OnCaptureModeChanged(value); }
        }

        /// <summary>
        ///   Gets or sets the current capture region.
        /// </summary>
        /// 
        public Rectangle CaptureRegion { get; set; }

        /// <summary>
        ///   Gets or sets the current capture window.
        /// </summary>
        /// 
        public IntPtr CaptureWindow { get; set; }

        /// <summary>
        ///   Gets the initial recording time.
        /// </summary>
        /// 
        public DateTime RecordingStartTime { get; private set; }

        /// <summary>
        ///   Gets the current recording time.
        /// </summary>
        /// 
        public TimeSpan RecordingDuration { get; private set; }


        /// <summary>
        ///   Gets whether the view-model is waiting for the
        ///   user to select a target window to be recorded.
        /// </summary>
        /// 
        public bool IsWaitingForTargetWindow { get; private set; }

        /// <summary>
        ///   Gets whether the application is recording the screen.
        /// </summary>
        /// 
        public bool IsRecording { get; private set; }

        /// <summary>
        ///   Gets whether the application is grabbing frames from the screen.
        /// </summary>
        /// 
        public bool IsPlaying { get; private set; }

        /// <summary>
        ///   Gets whether the capture region frame should be visible.
        /// </summary>
        /// 
        public bool IsFramesVisible { get { return IsPlaying && CaptureMode == CaptureRegionOption.Fixed; } }

        /// <summary>
        ///   Gets whether the screen preview is visible.
        /// </summary>
        public bool IsPreviewVisible { get; set; }

        /// <summary>
        ///   Gets the current status of the application.
        /// </summary>
        /// 
        public string Status { get; private set; }

        /// <summary>
        ///   Occurs when the view-model needs a window to be recorded.
        /// </summary>
        /// 
        public event EventHandler TargetWindowRequested;


        private CaptureRegionOption captureMode;
        private ScreenCaptureStream screenStream;
        private VideoFileWriter videoWriter;
        private VideoSourcePlayer videoPlayer;
        private Crop crop = new Crop(Rectangle.Empty);
        private CaptureCursor cursorCapture;
        private CaptureClick clickCapture;
        private Object syncObj = new Object();


        /// <summary>
        ///   Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// 
        public MainViewModel(VideoSourcePlayer player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Notify = new NotifyViewModel(this);

            videoPlayer = player;
            videoPlayer.NewFrame += new VideoSourcePlayer.NewFrameHandler(Player_NewFrame);

            CurrentDirectory = Settings.Default.DefaultFolder;
            CaptureMode = CaptureRegionOption.Primary;
            IsPreviewVisible = true;

            clickCapture = new CaptureClick();
            cursorCapture = new CaptureCursor();
            CaptureRegion = new Rectangle(0, 0, 640, 480);

            Status = "Ready";
        }



        /// <summary>
        ///   Starts playing the preview screen, grabbing
        ///   frames, but not recording to a video file.
        /// </summary>
        /// 
        public void StartPlaying()
        {
            if (IsPlaying) return;

            // Checks if we were already waiting for a window
            // to be selected, in case the user had chosen to 
            // capture from a fixed window.

            if (IsWaitingForTargetWindow)
            {
                // Yes, we were. We will not be waiting anymore
                // since the user should have selected one now.
                IsWaitingForTargetWindow = false;
            }

            else
            {
                // No, this is the first time the user starts the
                // frame grabber. Let's check what the user wants

                if (CaptureMode == CaptureRegionOption.Window)
                {
                    // The user wants to capture from a window. So we
                    // need to ask which window we have to keep a look.

                    // We will return here and wait the user to respond; 
                    // when he finishes selecting he should signal back
                    // by calling SelectWindowUnderCursor().
                    OnTargetWindowRequested(); return;
                }
            }

            // All is well. Keep configuring and start
            CaptureRegion = Screen.PrimaryScreen.Bounds;

            int framerate = 24; // TODO: grab from options?
            int height = CaptureRegion.Height;
            int width = CaptureRegion.Width;

            screenStream = new ScreenCaptureStream(CaptureRegion, 1000 / framerate);
            screenStream.VideoSourceError += screenStream_VideoSourceError;
            videoPlayer.VideoSource = screenStream;
            videoPlayer.Start();

            IsPlaying = true;
        }

        void screenStream_VideoSourceError(object sender, VideoSourceErrorEventArgs eventArgs)
        {
            throw new VideoException(eventArgs.Description);
        }



        /// <summary>
        ///   Starts recording. Only works if the player has
        ///   already been started and is grabbing frames.
        /// </summary>
        /// 
        public void StartRecording()
        {
            if (IsRecording || !IsPlaying) return;

            Rectangle area = CaptureRegion;
            CurrentFileName = newFileName();

            int height = area.Height;
            int width = area.Width;
            int framerate = 24;
            int bitrate = 10 * 1000 * 1000;

            string path = Path.Combine(CurrentDirectory, CurrentFileName);

            RecordingStartTime = DateTime.MinValue;

            videoWriter = new VideoFileWriter();
            videoWriter.Open(path, width, height, framerate, VideoCodec.H264, bitrate);

            IsRecording = true;
        }

        /// <summary>
        ///   Pauses the frame grabber, but keeps recording
        ///   if the software has already started recording.
        /// </summary>
        /// 
        public void PausePlaying()
        {
            if (!IsPlaying) return;

            videoPlayer.SignalToStop();
            IsPlaying = false;
        }

        /// <summary>
        ///   Stops recording.
        /// </summary>
        /// 
        public void StopRecording()
        {
            if (!IsRecording) return;

            lock (syncObj)
            {
                if (videoWriter != null && videoWriter.IsOpen)
                    videoWriter.Close();

                IsRecording = false;
            }
        }

        /// <summary>
        ///   Raises a property changed on <see cref="CaptureMode"/>.
        /// </summary>
        /// 
        protected void OnCaptureModeChanged(CaptureRegionOption value)
        {
            if (IsRecording)
                return;

            captureMode = value;

            if (value == CaptureRegionOption.Window && IsPlaying)
                OnTargetWindowRequested();

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("CaptureMode"));
        }

        /// <summary>
        ///   Raises the <see cref="TargetWindowRequested"/> event.
        /// </summary>
        protected void OnTargetWindowRequested()
        {
            IsWaitingForTargetWindow = true;
            if (TargetWindowRequested != null)
                TargetWindowRequested(this, EventArgs.Empty);
        }

        /// <summary>
        ///   Grabs the handle of the window currently under
        ///   the cursor, and if the application is waiting
        ///   for a handle, immediately starts playing.
        /// </summary>
        /// 
        public void SelectWindowUnderCursor()
        {
            CaptureWindow = NativeMethods.WindowFromPoint(Cursor.Position);

            if (IsWaitingForTargetWindow) StartPlaying();
        }

        /// <summary>
        ///   Releases resources and prepares
        ///   the application for closing.
        /// </summary>
        /// 
        public void Close()
        {
            videoPlayer.SignalToStop();
            videoPlayer.WaitForStop();

            if (videoWriter != null && videoWriter.IsOpen)
                videoWriter.Close();
        }



        private void Player_NewFrame(object sender, ref Bitmap image)
        {
            bool captureMouse = Settings.Default.CaptureMouse;
            bool captureClick = Settings.Default.CaptureClick;

            if (captureMouse || captureClick)
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    if (captureMouse)
                        cursorCapture.Draw(g);

                    if (captureClick)
                        clickCapture.Draw(g);
                }
            }


            CaptureRegion = adjustWindow();

            if (CaptureMode == CaptureRegionOption.Fixed ||
                CaptureMode == CaptureRegionOption.Window)
            {
                crop.Rectangle = CaptureRegion;
                image = crop.Apply(image);
            }


            lock (syncObj)
            {
                if (IsRecording)
                {
                    if (RecordingStartTime == DateTime.MinValue)
                        RecordingStartTime = DateTime.Now;

                    RecordingDuration = DateTime.Now - RecordingStartTime;
                    videoWriter.WriteVideoFrame(image, RecordingDuration);
                }
            }
        }

        private Rectangle adjustWindow()
        {
            Rectangle area = CaptureRegion;

            if (CaptureMode == CaptureRegionOption.Window && !IsRecording)
                area = NativeMethods.GetWindowRect(CaptureWindow);
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
                mode = "Screen_";
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
                if (clickCapture != null)
                {
                    clickCapture.Dispose();
                    clickCapture = null;
                }

                if (cursorCapture != null)
                {
                    cursorCapture.Dispose();
                    cursorCapture = null;
                }

                if (videoWriter != null)
                {
                    videoWriter.Dispose();
                    videoWriter = null;
                }
            }
        }
        #endregion


        // The PropertyChanged event doesn't needs to be explicitly raised
        // from this application. The event raising is handled automatically
        // by the NotifyPropertyWeaver VS extension using IL injection.
        //
#pragma warning disable 0067
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
    }
}
