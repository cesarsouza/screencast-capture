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
    using System;
    using System.ComponentModel;
    using Accord.Controls;
    using ScreenCapture.Properties;
    using Accord.Controls;

    /// <summary>
    ///   Main ViewModel to control the application.
    /// </summary>
    /// 
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {

        /// <summary>
        ///   Gets the recorder view-model.
        /// </summary>
        public RecorderViewModel Recorder { get; private set; }

        /// <summary>
        ///   Gets the notification area view-model.
        /// </summary>
        /// 
        public NotifyViewModel Notifier { get; private set; }

        /// <summary>
        ///   Gets the format converter view-model.
        /// </summary>
        /// 
        public ConvertViewModel Converter { get; private set; }



        /// <summary>
        ///   Gets the current directory in the file browser.
        /// </summary>
        /// 
        public string CurrentDirectory { get; set; }

        /// <summary>
        ///   Gets the currently selected file in the file browser.
        /// </summary>
        /// 
        public string CurrentSelection { get; set; }

        /// <summary>
        ///   Gets whether the screen preview is visible.
        /// </summary>
        /// 
        public bool IsPreviewVisible { get; set; }

        /// <summary>
        ///   Gets or whether the webcam window is visible.
        /// </summary>
        public bool IsWebcamEnabled { get; set; }

        /// <summary>
        ///   Gets the current status of the application.
        /// </summary>
        /// 
        [LocalizableAttribute(true)]
        public string StatusText { get; set; }



        /// <summary>
        ///   Occurs when the format conversion dialog is requested.
        /// </summary>
        /// 
        public event EventHandler ShowConversionDialog;


        /// <summary>
        ///   Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// 
        public MainViewModel(VideoSourcePlayer2 player)
        {
            if (player == null) throw new ArgumentNullException("player");

            Recorder = new RecorderViewModel(this, player);
            Notifier = new NotifyViewModel(Recorder);
            Converter = new ConvertViewModel(this);

            IsPreviewVisible = true;
            CurrentDirectory = Settings.Default.DefaultFolder;
            StatusText = Resources.Status_Ready;



            PropertyChanged += MainViewModel_PropertyChanged;
            Recorder.PropertyChanged += recorder_PropertyChanged;
            Converter.PropertyChanged += Convert_PropertyChanged;
        }



        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentSelection" && !IsPreviewVisible)
                Converter.InputPath = CurrentSelection;
            else if (e.PropertyName == "IsPreviewVisible")
                raise("IsConversionVisible");
        }

        private void recorder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasRecorded" && Recorder.HasRecorded)
            {
                Converter.InputPath = Recorder.OutputPath;
                if (Settings.Default.ShowConversionOnFinish)
                    raiseShowConversionDialog();
            }
            else if (e.PropertyName == "IsPlaying")
                raise("IsRecordingEnabled");
            else if (e.PropertyName == "IsRecording")
                raise("IsConversionVisible");
        }

        private void raiseShowConversionDialog()
        {
            if (ShowConversionDialog != null)
                ShowConversionDialog(this, EventArgs.Empty);
        }

        private void Convert_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            raise("IsConversionVisible");
            raise("IsRecordingEnabled");
        }


        /// <summary>
        ///   Gets whether the option for converting
        ///   a video should be presented to the user.
        /// </summary>
        /// 
        public bool IsConversionVisible
        {
            get
            {
                if (Recorder.IsRecording)
                    return false;

                if (IsPreviewVisible)
                {
                    if (Recorder.HasRecorded)
                        return Converter.CanConvert;
                    return false;
                }

                return Converter.CanConvert;
            }
        }

        /// <summary>
        ///   Gets whether the option for recording
        ///   a video should be enabled in the UI.
        /// </summary>
        /// 
        public bool IsRecordingEnabled
        {
            get
            {
                if (Converter.IsConverting)
                    return false;
                return Recorder.IsPlaying;
            }
        }




        private void raise(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                Recorder.Dispose();
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
