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
    using ScreenCapture.ViewModels;

    /// <summary>
    ///   Capture Window Window.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   This is the (almost) invisible window which follows the mouse
    ///   when the user selects the "Capture from a Window" option. It
    ///   is an almost fully transparent window, excepts for the text
    ///   message which accompanies the mouse cursor and a two pixel
    ///   wide square which is directly under the mouse cursor.</para>
    ///   
    /// <para>
    ///   This window contains a timer which continuously relocates
    ///   the window to the cursor's position. When the user clicks,
    ///   the user actually clicks on a two pixel wide square panel
    ///   and the click is intercepted.</para>
    /// </remarks>
    /// 
    [System.Security.SecurityCritical]
    public partial class CaptureWindow : Form
    {

        RecorderViewModel viewModel;


        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureWindow"/> class.
        /// </summary>
        /// 
        /// <param name="viewModel">The main view model.</param>
        /// 
        public CaptureWindow(RecorderViewModel viewModel)
            : this()
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            this.viewModel = viewModel;
            this.viewModel.TargetWindowRequested += viewModel_TargetWindowRequested;
        }
        

        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureWindow"/>
        ///   class. Should not be called without passing a view model.
        /// </summary>
        /// 
        public CaptureWindow()
        {
            InitializeComponent();
        }


        private void viewModel_TargetWindowRequested(object sender, EventArgs e)
        {
            Show();
        }


        /// <summary>
        ///   Triggers when the user clicks the mouse when the window is being shown.
        /// </summary>
        /// 
        private void squarePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Hide();

                viewModel.SelectWindowUnderCursor();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged"/> event.
        /// </summary>
        /// 
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            Location = Cursor.Position;
            timer.Enabled = Visible;
        }


        /// <summary>
        ///   Relocates the window when a timer ticks.
        /// </summary>
        /// 
        private void OnTimerTick(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
            this.Focus();
        }
        
        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"/> event.
        /// </summary>
        ///
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Location = Cursor.Position;
        }

        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
        /// </summary>
        ///
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Location = Cursor.Position;
        }

        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        ///
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = Cursor.Position;
        }

        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown"/> event.
        /// </summary>
        ///
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            if (e.KeyCode == Keys.Escape)
                this.Hide();

            base.OnPreviewKeyDown(e);
        }

    }
}
