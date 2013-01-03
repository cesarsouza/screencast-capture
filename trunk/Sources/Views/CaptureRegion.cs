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

namespace ScreenCapture.Views
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ScreenCapture.Native;
    using ScreenCapture.ViewModels;

    /// <summary>
    ///   Capture Region Window.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   This is the window which shows up when the user selects the
    ///   "Capture From a Fixed Region" option. It is an almost fully
    ///   transparent window, with exceptions for the borders.</para>
    ///   
    /// <para>
    ///   The window can be fixed at the user screen at the start of
    ///   the recording.</para>
    /// </remarks>
    /// 
    [System.Security.SecurityCritical]
    public partial class CaptureRegion : Form
    {

        MainViewModel viewModel;

        /// <summary>
        ///   Gets or sets whether the window is currently
        ///   pinned (fixed) on the user's desktop and can
        ///   not be moved.
        /// </summary>
        /// 
        public bool Pinned { get; set; }


        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureRegion"/> class.
        /// </summary>
        /// 
        /// <param name="viewModel">The main view model in the application.</param>
        /// 
        public CaptureRegion(MainViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;

            // Call CreateControl, otherwise 
            // binding to Visible won't work.

            this.ForceCreateControl();
        }



        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureRegion"/>
        ///   class. Should not be called without passing a view model.
        /// </summary>
        /// 
        public CaptureRegion()
        {
            InitializeComponent();
            Pinned = false;
        }


        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged"/> event.
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            update();
        }

        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            update();
        }

        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged"/> event.
        /// </summary>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            update();
        }

        /// <summary>
        ///   The overriding of this method allows for complete control over
        ///   the window message parsing. By handling and supressing messages
        ///   related to resizing and moving, the window will stay fixed on
        ///   screen.
        /// </summary>
        /// 
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // The process implemented here is based on the original 2006 post by Vaibhav Gaikwad
            // http://vaibhavgaikwad.wordpress.com/2006/06/05/creating-a-immovable-windows-form-in-c/

            if (m.Msg == NativeMethods.WM_INITMENUPOPUP)
            {
                if ((m.LParam.ToInt32() / 65536) != 0)
                {
                    Int32 AbleFlags = NativeMethods.MF_ENABLED;

                    if (Pinned)
                        AbleFlags = NativeMethods.MF_DISABLED | NativeMethods.MF_GRAYED;

                    NativeMethods.EnableMenuItem(m.WParam, NativeMethods.SC_MOVE,
                        NativeMethods.MF_BYCOMMAND | AbleFlags);
                }
            }

            if (Pinned)
            {
                if ((m.Msg == NativeMethods.WM_NCLBUTTONDOWN && m.WParam.ToInt32() == NativeMethods.HTCAPTION) ||
                    (m.Msg == NativeMethods.WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == NativeMethods.SC_MOVE))
                    return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        ///  This method updates the current region registered in the
        ///  main View-Model based on the window's size and location.
        /// </summary>
        /// 
        private void update()
        {
            if (viewModel == null)
                return;

            Rectangle rectangle = viewModel.CaptureRegion;

            if (!viewModel.IsRecording)
            {
                Rectangle client = this.ClientRectangle;
                Rectangle border = this.DesktopBounds;
                Rectangle area = this.RectangleToScreen(panel1.ClientRectangle);

                area.X += 3;
                area.Y += 3;
                area.Width -= 2;
                area.Height -= 2;

                viewModel.CaptureRegion = area;
            }
        }


        /// <summary>
        ///   Raises the CreateControl event.
        /// </summary>
        /// 
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Bind(b => b.BackColor, viewModel, m => m.IsRecording,
                value => value ? Color.Firebrick : Color.Gold);
            this.Bind(b => b.Visible, viewModel, m => m.IsFramesVisible);
            this.Bind(b => b.Pinned, viewModel, m => m.IsRecording);
        }

        private void CaptureRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewModel.CaptureMode = CaptureRegionOption.Primary;

            e.Cancel = e.CloseReason == CloseReason.UserClosing;
        }

    }
}
