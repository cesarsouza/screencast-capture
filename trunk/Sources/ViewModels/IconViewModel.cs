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

    /// <summary>
    ///   ViewModel bridging the notification area and the model.
    /// </summary>
    /// 
    public class IconViewModel : INotifyPropertyChanged
    {

        private MainViewModel main;

        private Icon playIcon = Resources.icon_play_overlay;
        private Icon stopIcon = Resources.icon_stop_overlay;
        private Icon recIcon = Resources.icon_record_overlay;


        public Icon CurrentIcon { get; set; }
        public string CurrentText { get; set; }

        public IconViewModel(MainViewModel main)
        {
            if (main == null)
                throw new ArgumentNullException("main");

            this.main = main;
            this.main.PropertyChanged += main_PropertyChanged;

            CurrentIcon = playIcon;
            CurrentText = "Start playing (F9)";
        }

        private void main_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRecording" || e.PropertyName == "IsPlaying")
            {
                if (main.IsRecording)
                {
                    CurrentIcon = stopIcon;
                    CurrentText = "Stop recording (F10)";
                    return;
                }

                if (main.IsPlaying)
                {
                    CurrentIcon = recIcon;
                    CurrentText = "Start recording (F10)";
                    return;
                }

                CurrentIcon = playIcon;
                CurrentText = "Start playing (F9)";
            }
        }


        // The PropertyChanged event doesn't needs to be explicitly raised
        // from this application. The event raising is handled automatically
        // by the NotifyPropertyWeaver VS extension using IL injection.
        //
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
    }
}
