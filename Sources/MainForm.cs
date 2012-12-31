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

namespace ScreenCapture
{
    using System;
    using System.Windows.Forms;
    using Microsoft.WindowsAPICodePack.Shell;

    /// <summary>
    ///   Main window for the Screencast Capture application.
    /// </summary>
    /// 
    public partial class MainForm : Form
    {

        MainViewModel viewModel;

        // Those are the windows shown when the user selects the
        // "Capture Region" or "Capture Window" application modes.
        CaptureRegion regionWindow;
        CaptureWindow windowWindow;


        public MainForm()
        {
            InitializeComponent();

            viewModel = new MainViewModel(videoSourcePlayer1);
            regionWindow = new CaptureRegion(viewModel);
            windowWindow = new CaptureWindow(viewModel);
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            explorerBrowser.ContentOptions.ViewMode = Microsoft.WindowsAPICodePack.Controls.ExplorerBrowserViewMode.Details;
            explorerBrowser.NavigationOptions.PaneVisibility.Commands = Microsoft.WindowsAPICodePack.Controls.PaneVisibilityState.Hide;

            //
            // This section configures all the bindings between 
            // form controls and properties from the view model.
            //
            this.Bind("BrowserDirectory", viewModel, "CurrentDirectory");
            btnStartRecording.Bind("Enabled", viewModel, "IsPlaying");
            btnStartRecording.Bind("Visible", viewModel, "IsRecording", (bool value) => !value);
            btnStopRecording.Bind("Visible", viewModel, "IsRecording");
            btnStartPlaying.Bind("Visible", viewModel, "IsPlaying", (bool value) => !value);
            btnPausePlaying.Bind("Visible", viewModel, "IsPlaying");
            lbStatusRecording.Bind("Visible", viewModel, "IsRecording");
            lbStatusReady.Bind("Visible", viewModel, "IsRecording", (bool value) => !value);
            btnCaptureMode.Bind("Enabled", viewModel, "IsRecording", (bool value) => !value);

            videoSourcePlayer1.Bind("Visible", viewModel, "IsPreviewVisible");
            explorerBrowser.Bind("Visible", viewModel, "IsPreviewVisible", (bool value) => !value);
            btnScreenPreview.Bind("Visible", viewModel, "IsPreviewVisible", (bool value) => !value);
            btnStorageFolder.Bind("Visible", viewModel, "IsPreviewVisible");

            btnCapturePrimaryScreen.Bind("Checked", viewModel, "CaptureMode",
                (CaptureRegionOption value) => value == CaptureRegionOption.Primary);
            btnCaptureRegion.Bind("Checked", viewModel, "CaptureMode",
                (CaptureRegionOption value) => value == CaptureRegionOption.Fixed);
            btnCaptureWindow.Bind("Checked", viewModel, "CaptureMode",
                (CaptureRegionOption value) => value == CaptureRegionOption.Window);

            regionWindow.Show();
            regionWindow.Bind("Visible", viewModel, "IsFramesVisible");
            regionWindow.Bind("Pinned", viewModel, "IsRecording");

            windowWindow.Show();
            windowWindow.Bind("Following", viewModel, "IsChoosingTarget");
            windowWindow.Bind("Visible", viewModel, "IsChoosingTarget");
        }


        // Video player controls
        private void btnStartPlaying_Click(object sender, EventArgs e)
        {
            viewModel.StartPlaying();
        }

        private void btnPausePlaying_Click(object sender, EventArgs e)
        {
            viewModel.PausePlaying();
        }


        // Video recording controls
        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            viewModel.StartRecording();
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            viewModel.StopRecording();
        }


        // Current visualization controls
        private void btnStorageFolder_Click(object sender, EventArgs e)
        {
            viewModel.IsPreviewVisible = false;
        }

        private void btnScreenPreview_Click(object sender, EventArgs e)
        {
            viewModel.IsPreviewVisible = true;
        }


        // Capture mode controls
        private void btnCapturePrimaryScreen_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Primary;
            updateCaptureMode(sender as ToolStripMenuItem);
        }

        private void btnCaptureWindow_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Window;
            updateCaptureMode(sender as ToolStripMenuItem);
        }

        private void btnCaptureRegion_Click(object sender, EventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Fixed;
            updateCaptureMode(sender as ToolStripMenuItem);
        }

        private void updateCaptureMode(ToolStripMenuItem item)
        {
            btnCaptureMode.Text = item.Text;
            btnCaptureMode.Image = item.Image;
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewModel.Close();
        }


        /// <summary>
        ///   Gets or sets the current directory in the embedded
        ///   Windows Explorer window. This property exists only
        ///   to provide an automatic binding between the window
        ///   and the ViewModel.
        /// </summary>
        /// 
        public string BrowserDirectory
        {
            get
            {
                if (explorerBrowser.NavigationLog.CurrentLocation == null) return null;
                return explorerBrowser.NavigationLog.CurrentLocation.Name;
            }
            set { explorerBrowser.Navigate(ShellFileSystemFolder.FromFolderPath(value)); }
        }

    }
}
