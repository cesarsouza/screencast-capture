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

namespace ScreenCapture.Properties
{
    using System;
    using System.Configuration;

    internal sealed partial class Settings
    {

        public Settings()
        {
            this.SettingsLoaded += new SettingsLoadedEventHandler(Settings_SettingsLoaded);
            this.SettingsSaving += new SettingsSavingEventHandler(Settings_SettingsSaving);
        }


        private void Settings_SettingsLoaded(object sender, SettingsLoadedEventArgs e)
        {
            if (Settings.Default.CanUpgrade)
                Settings.Default.Upgrade();
            
            CanUpgrade = false;

            // Check if the default folder is null or empty 
            // and set it to the My Videos folder by default.

            if (String.IsNullOrWhiteSpace(DefaultFolder))
                DefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        private void Settings_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // TODO: If needed, add a FirstRunOverride setting to disable
            // the automatic setting of this property on each save.

            FirstRun = false;
        }
    }
}
