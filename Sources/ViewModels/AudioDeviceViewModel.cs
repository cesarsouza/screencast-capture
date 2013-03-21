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
    using Accord.DirectSound;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System;

    /// <summary>
    ///   Audio Devices View Model
    /// </summary>
    public class AudioCaptureDeviceViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///   Gets or sets the associated device info.
        /// </summary>
        /// 
        public AudioDeviceInfo DeviceInfo { get; set; }

        /// <summary>
        ///   Gets or sets whether this device should be used.
        /// </summary>
        /// 
        public bool Checked { get; set; }


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
    ///   Collection of View Models for configuring audio capture devices.
    /// </summary>
    /// 
    public class AudioViewModelCollection : BindingList<AudioCaptureDeviceViewModel>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="AudioViewModelCollection"/> class.
        /// </summary>
        /// 
        /// <param name="devices">The devices used to initialize the list.</param>
        /// 
        public AudioViewModelCollection(IEnumerable<AudioDeviceInfo> devices)
        {
            if (devices == null) 
                throw new ArgumentNullException("devices");

            foreach (AudioDeviceInfo info in devices)
            {
                Add(new AudioCaptureDeviceViewModel() { DeviceInfo = info });
            }
        }
    }
}
