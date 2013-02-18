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
    using ScreenCapture.Processors;
    using ScreenCapture.ViewModels;

    /// <summary>
    ///   Preview display for the keyboard logger.
    /// </summary>
    /// 
    public partial class KeyboardPreviewForm : Form, IDisposable
    {

        CaptureKeyboard capture;
        OptionViewModel viewModel;

        /// <summary>
        ///   Initializes a new instance of the <see cref="KeyboardPreviewForm"/> class.
        /// </summary>
        /// 
        /// <param name="viewModel">The view model.</param>
        /// 
        public KeyboardPreviewForm(OptionViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="KeyboardPreviewForm"/> class.
        /// </summary>
        /// 
        public KeyboardPreviewForm()
        {
            InitializeComponent();

            capture = new CaptureKeyboard()
            {
                Preview = true,
                Enabled = false
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            capture.Font = new Font(viewModel.FontFamily, viewModel.FontSize);

            Invalidate();
        }

        /// <summary>
        ///   Paints the control.
        /// </summary>
        /// 
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        /// 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (capture != null)
                capture.Draw(e.Graphics);
        }

        private void PreviewOnScreeDisplay_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
