// Screencast Capture Lite 
// http://www.crsouza.com
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
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CaptureWindow : Form
    {

        MainViewModel viewModel;

        public bool Following
        {
            get { return timer1.Enabled; }
            set
            {
                timer1.Enabled = value;
                this.Focus();
            }
        }

        public Point Position
        {
            get
            {
                Point point = this.Location;
                point.Offset(-2, -2);
                return point;
            }
        }

        public CaptureWindow(MainViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
            this.timer1.Start();
        }

        public CaptureWindow()
        {
            InitializeComponent();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
            this.Focus();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Location = Cursor.Position;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Location = Cursor.Position;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = Cursor.Position;
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            base.OnPreviewKeyDown(e);
        }


        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.timer1 != null)
                    this.timer1.Stop();

                this.Hide();

                viewModel.GetWindowUnderCursor();
            }
        }


    }
}
