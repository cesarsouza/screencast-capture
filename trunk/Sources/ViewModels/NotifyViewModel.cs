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
    using System.Drawing;
    using ScreenCapture.Properties;
    using System.Windows.Forms;

    /// <summary>
    ///   ViewModel bridging the notification area and the model.
    /// </summary>
    /// 
    public class NotifyViewModel : INotifyPropertyChanged
    {

        private MainViewModel main;

        private Icon playIcon = Resources.icon_play_overlay;
        private Icon stopIcon = Resources.icon_stop_overlay;
        private Icon recIcon = Resources.icon_record_overlay;

        /// <summary>
        ///   Gets or sets the current icon to be 
        ///   displayed in the notification area.
        /// </summary>
        /// 
        public Icon CurrentIcon { get; private set; }

        /// <summary>
        ///   Gets or sets the current text to be 
        ///   displayed in the notification area.
        /// </summary>
        /// 
        public string CurrentText { get; private set; }

        /// <summary>
        ///   Occurs when the view-model needs to show a balloon.
        /// </summary>
        /// 
        public event EventHandler<BalloonEventArgs> ShowBalloon;

        /// <summary>
        ///   Initializes a new instance of the <see cref="NotifyViewModel"/> class.
        /// </summary>
        /// 
        public NotifyViewModel(MainViewModel main)
        {
            if (main == null)
                throw new ArgumentNullException("main");

            this.main = main;
            this.main.PropertyChanged += main_PropertyChanged;
            this.Update();

           
        }

        private void main_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRecording" ||
                e.PropertyName == "IsPlaying")
                Update();
        }

        /// <summary>
        ///   Notifies the view-model that the
        ///   view has completed loading.
        /// </summary>
        /// 
        public void Loaded()
        {
            if (main.Options.FirstRun)
                showGreetings();
        }

        /// <summary>
        ///   Analyzes the current program state and
        ///   updates the notification area accordingly.
        /// </summary>
        /// 
        public void Update()
        {
            if (main.IsRecording)
            {
                CurrentIcon = stopIcon;
                CurrentText = "Stop recording (F10)";
            }
            else if (main.IsPlaying)
            {
                CurrentIcon = recIcon;
                CurrentText = "Start recording (F10)";
            }
            else
            {
                CurrentIcon = playIcon;
                CurrentText = "Start playing (F9)";
            }
        }

        private void showGreetings()
        {
            var args = new BalloonEventArgs()
            {
                Title = "Hi there!",
                Text = "Perhaps you would like to know that this software is more useful if"
                + " you enable always-visible notification icons for it. Please click"
                + " the wench button above and enable them if you wish!",
                Icon = ToolTipIcon.Info,
                Milliseconds = 15000
            };

            if (ShowBalloon != null)
                ShowBalloon(this, args);
        }


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

    /// <summary>
    ///   Balloon Tip Event Args.
    /// </summary>
    /// 
    public class BalloonEventArgs : EventArgs
    {
        /// <summary>
        ///   Gets or sets the balloon icon.
        /// </summary>
        /// 
        public ToolTipIcon Icon { get; set; }

        /// <summary>
        ///   Gets or sets the balloon time period.
        /// </summary>
        /// 
        public int Milliseconds { get; set; }

        /// <summary>
        ///   Gets or sets the balloon text.
        /// </summary>
        /// 
        public string Text { get; set; }

        /// <summary>
        ///   Gets or sets the balloon title.
        /// </summary>
        /// 
        public string Title { get; set; }
    }

}
