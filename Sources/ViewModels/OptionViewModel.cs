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
    using ScreenCapture.Properties;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    /// <summary>
    ///   ViewModel for binding option windows.
    /// </summary>
    /// 
    public class OptionViewModel : INotifyPropertyChanged
    {

        /// <summary>
        ///   Gets or sets the default save folder, which is
        ///   opened by default when the application starts.
        /// </summary>
        /// 
        public string DefaultSaveFolder { get; set; }

        /// <summary>
        ///   Gets or sets whether to capture the mouse cursor image.
        /// </summary>
        /// 
        public bool CaptureMouse { get; set; }

        /// <summary>
        ///   Gets or sets whether to capture mouse clicks.
        /// </summary>
        /// 
        public bool CaptureClick { get; set; }

        /// <summary>
        ///   Gets or sets whether the application has been opened before.
        /// </summary>
        /// 
        public bool FirstRun { get; set; }

        /// <summary>
        ///   Gets or sets whether to capture keyboard key presses.
        /// </summary>
        /// 
        public bool CaptureKeys { get; set; }

        /// <summary>
        ///   Gets or sets whether to capture audio.
        /// </summary>
        /// 
        public bool CaptureAudio { get; set; }

        /// <summary>
        ///   Gets or sets the framerate of the video recordings.
        /// </summary>
        /// 
        public double FrameRate { get; set; }

        /// <summary>
        ///   Gets or sets the sample rate of the audio recordings.
        /// </summary>
        /// 
        public int AudioRate { get; set; }

        /// <summary>
        ///   Gets or sets the current storage container format.
        /// </summary>
        /// 
        public string Container { get; set; }

        public string FontFamily { get; set; }

        public float FontSize { get; set; }

        /// <summary>
        ///   Gets or sets whether to offer a conversion 
        ///   dialog automatically when recording stops.
        /// </summary>
        /// 
        public bool AutoConversionDialog { get; set; }

        /// <summary>
        ///   Gets a list of supported container formats.
        /// </summary>
        /// 
        public static ICollection<string> SupportedContainers { get; private set; }

        public static ICollection<string> InstalledFonts { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="OptionViewModel"/> class.
        /// </summary>
        /// 
        public OptionViewModel()
        {
            Load();
        }

        /// <summary>
        ///   Initializes the <see cref="OptionViewModel" /> class.
        /// </summary>
        /// 
        static OptionViewModel()
        {
            SupportedContainers = new[] 
            {
                "avi", "mkv", "m4v", "mp4", "mov"
            };

            List<string> fonts = new List<string>();
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
                fonts.Add(font.Name);
            InstalledFonts = fonts;
        }



        /// <summary>
        ///   Saves the contents of the view-model 
        ///   into the application settings file.
        /// </summary>
        /// 
        public void Save()
        {
            Settings.Default.FirstRun = FirstRun;
            Settings.Default.DefaultFolder = DefaultSaveFolder;
            Settings.Default.CaptureAudio = CaptureAudio;
            Settings.Default.CaptureMouse = CaptureMouse;
            Settings.Default.CaptureClick = CaptureClick;
            Settings.Default.CaptureKeys = CaptureKeys;
            Settings.Default.FrameRate = FrameRate;
            Settings.Default.Container = Container;
            Settings.Default.ShowConversionOnFinish = AutoConversionDialog;
            Settings.Default.KeyboardFont = new Font(FontFamily, FontSize);

            Settings.Default.Save();
        }

        /// <summary>
        ///   Loads the contents of the application
        ///   settings file into this view-model.
        /// </summary>
        /// 
        public void Load()
        {
            FirstRun = Settings.Default.FirstRun;
            DefaultSaveFolder = Settings.Default.DefaultFolder;
            CaptureAudio = Settings.Default.CaptureAudio;
            CaptureMouse = Settings.Default.CaptureMouse;
            CaptureClick = Settings.Default.CaptureClick;
            CaptureKeys = Settings.Default.CaptureKeys;
            FrameRate = Settings.Default.FrameRate;
            Container = Settings.Default.Container;
            AutoConversionDialog = Settings.Default.ShowConversionOnFinish;
            FontFamily = Settings.Default.KeyboardFont.FontFamily.Name;
            FontSize = Settings.Default.KeyboardFont.Size;
            AudioRate = Settings.Default.SampleRate;
        }



        // The PropertyChanged event doesn't needs to be explicitly raised
        // from this application. The event raising is handled automatically
        // by the NotifyPropertyWeaver VS extension using IL injection.
        //
#pragma warning disable 0067
        /// <summary>
        ///   Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
    }
}
