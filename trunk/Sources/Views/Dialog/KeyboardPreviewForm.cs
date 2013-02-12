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

    public partial class KeyboardPreviewForm : Form
    {

        CaptureKeyboard capture;
        OptionViewModel viewModel;

        public KeyboardPreviewForm(OptionViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
        }

        public KeyboardPreviewForm()
        {
            InitializeComponent();
        }

        private void PreviewOnScreeDisplay_Load(object sender, EventArgs e)
        {
            capture = new CaptureKeyboard()
            {
                Preview = true
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            capture.Font = new Font(viewModel.FontFamily, viewModel.FontSize);

            Invalidate();
        }

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
