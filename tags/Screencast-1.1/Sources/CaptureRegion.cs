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

    public partial class CaptureRegion : Form
    {

        MainViewModel viewModel;

        public bool Pinned { get; set; }


        public CaptureRegion(MainViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;

            this.Bind("BackColor", viewModel, "IsRecording", (bool value) =>
                value ? Color.Firebrick : Color.Gold);
        }

        public CaptureRegion()
        {
            InitializeComponent();
            Pinned = false;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            update();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            update();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            update();
        }

        private void update()
        {
            if (viewModel == null)
                return;

            Rectangle rectangle = viewModel.CurrentRegion;

            if (!viewModel.IsRecording)
            {
                Rectangle client = this.ClientRectangle;
                Rectangle border = this.DesktopBounds;
                Rectangle area = this.RectangleToScreen(panel1.ClientRectangle);

                area.X += 3;
                area.Y += 3;
                area.Width -= 2;
                area.Height -= 2;

                viewModel.CurrentRegion = area;
            }
        }

    }
}
