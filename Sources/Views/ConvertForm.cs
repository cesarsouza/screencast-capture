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
    ///   Video conversion options.
    /// </summary>
    /// 
    public partial class ConvertForm : Form
    {

        ConvertViewModel viewModel;
        ThemeMargins margins;


        /// <summary>
        ///   Initializes a new instance of the <see cref="ConvertForm"/> class.
        /// </summary>
        /// 
        public ConvertForm(ConvertViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ConvertForm"/> class.
        /// </summary>
        /// 
        public ConvertForm()
        {
            InitializeComponent();
        }


        /// <summary>
        ///   Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cbTheora.Bind(b => b.Checked, viewModel, m => m.ToOgg);
            cbWebM.Bind(b => b.Checked, viewModel, m => m.ToWebM);

            // Perform special processing to enable aero
            if (SafeNativeMethods.IsAeroEnabled)
            {
                margins = new ThemeMargins();
                margins.TopHeight = panel1.Top + 1;
                margins.LeftWidth = panel1.Left + 1;
                margins.RightWidth = ClientRectangle.Right - panel1.Right + 1;
                margins.BottomHeight = ClientRectangle.Bottom - panel1.Bottom + 1;

                // Extend the Frame into client area
                SafeNativeMethods.ExtendAeroGlassIntoClientArea(this, margins);
            }
        }

        /// <summary>
        ///   Paints the background of the control.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (SafeNativeMethods.IsAeroEnabled)
            {
                // paint background black to enable include glass regions
                e.Graphics.Clear(Color.FromArgb(0, this.BackColor));

                // revert the non-glass rectangle back to it's original colour
                Rectangle clientArea = new Rectangle(
                    margins.LeftWidth, margins.TopHeight,
                    this.ClientRectangle.Width - margins.LeftWidth - margins.RightWidth,
                    this.ClientRectangle.Height - margins.TopHeight - margins.BottomHeight
                );
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            viewModel.Start();

            Close();
        }
    }
}
