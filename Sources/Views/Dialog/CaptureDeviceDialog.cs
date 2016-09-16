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
//
// This file has been based on the work by
// Copyright © Andrew Kirillov, 2005-2009
// andrew.kirillov@aforgenet.com
//

namespace ScreenCapture.Views
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Accord.Video.DirectShow;

    /// <summary>
    ///   Capture device selection dialog.
    /// </summary>
    /// 
    public partial class CaptureDeviceDialog : Form
    {
        private FilterInfoCollection videoDevices;
        private string device;

        /// <summary>
        ///   Gets the selected video device.
        /// </summary>
        /// 
        public string VideoDevice
        {
            get { return device; }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureDeviceDialog"/> class.
        /// </summary>
        /// 
        public CaptureDeviceDialog()
        {
            InitializeComponent();


            try // show device list
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    noDevices();

                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    devicesCombo.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                noDevices();
            }

            devicesCombo.SelectedIndex = 0;
        }

        private void noDevices()
        {
            devicesCombo.Items.Add("No local capture devices");
            devicesCombo.Enabled = false;
            okButton.Enabled = false;
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            device = videoDevices[devicesCombo.SelectedIndex].MonikerString;
        }
    }
}
