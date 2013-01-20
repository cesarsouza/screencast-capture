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

namespace ScreenCapture.Controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Microsoft.WindowsAPICodePack.Controls;
    using Microsoft.WindowsAPICodePack.Shell;

    /// <summary>
    ///   Extended version of the Explorer Browser control.
    /// </summary>
    /// 
    public partial class ExplorerBrowserCustom : UserControl, INotifyPropertyChanged
    {

        /// <summary>
        ///   Gets or sets the current directory in 
        ///   the embedded Windows Explorer window. 
        /// </summary>
        /// 
        public string CurrentDirectory
        {
            get
            {
                if (browser.NavigationLog.CurrentLocation == null)
                    return String.Empty;
                return browser.NavigationLog.CurrentLocation.ParsingName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    browser.Navigate(ShellFileSystemFolder.FromFolderPath(value));
            }
        }

        /// <summary>
        ///   Gets or sets the current filename selected
        ///   in the embedded Windows Explorer Window.
        /// </summary>
        /// 
        public string CurrentFileName
        {
            get
            {
                if (browser.SelectedItems.Count > 0)
                    return browser.SelectedItems[0].ParsingName;
                return String.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    browser.Navigate(ShellFile.FromParsingName(value));
            }
        }

        /// <summary>
        ///   Gets or sets the default directory.
        /// </summary>
        /// 
        public string DefaultDirectory { get; set; }

        /// <summary>
        ///   Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        ///   Initializes a new instance of the <see cref="ExplorerBrowserCustom"/> class.
        /// </summary>
        /// 
        public ExplorerBrowserCustom()
        {
            InitializeComponent();

            browser.ContentOptions.ViewMode = ExplorerBrowserViewMode.Details;
            browser.NavigationOptions.PaneVisibility.Query = PaneVisibilityState.Hide;
            browser.NavigationOptions.PaneVisibility.Preview = PaneVisibilityState.Hide;
            browser.NavigationOptions.PaneVisibility.AdvancedQuery = PaneVisibilityState.Hide;
            browser.NavigationOptions.PaneVisibility.CommandsOrganize = PaneVisibilityState.Hide;
            browser.NavigationOptions.PaneVisibility.Details = PaneVisibilityState.Hide;
            browser.NavigationOptions.PaneVisibility.CommandsView = PaneVisibilityState.Show;
            browser.NavigationOptions.PaneVisibility.Commands = PaneVisibilityState.Show;
            browser.NavigationOptions.PaneVisibility.Navigation = PaneVisibilityState.Show;

            DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }


        /// <summary>
        ///   Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// 
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }




        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (browser.NavigationLog.CanNavigateBackward)
                browser.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (browser.NavigationLog.CanNavigateForward)
                browser.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void btnParent_Click(object sender, EventArgs e)
        {
            ShellObject parent = browser.NavigationLog.CurrentLocation.Parent;
            browser.Navigate(parent);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            CurrentDirectory = DefaultDirectory;
        }



        private void browser_NavigationPending(object sender, NavigationPendingEventArgs e)
        {
            updateButtonStatus();
        }

        private void browser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            updateButtonStatus();
        }

        private void browser_NavigationComplete(object sender, NavigationCompleteEventArgs e)
        {
            OnPropertyChanged("CurrentDirectory");
            OnPropertyChanged("CurrentFileName");
            updateButtonStatus();
        }

        private void browser_SelectionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("CurrentFileName");
        }



        private void updateButtonStatus()
        {
            tbAddress.Text = CurrentDirectory;

            btnBackward.Enabled = browser.NavigationLog.CanNavigateBackward;
            btnForward.Enabled = browser.NavigationLog.CanNavigateForward;

            btnParent.Enabled =
                browser.NavigationLog.CurrentLocation != null &&
                browser.NavigationLog.CurrentLocation.Parent != null;
        }

    }
}
