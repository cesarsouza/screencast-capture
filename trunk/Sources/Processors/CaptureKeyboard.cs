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

namespace ScreenCapture.Processors
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using ScreenCapture.Native;
    using ScreenCapture.Native.Context;

    public class CaptureKeyboard
    {

        private bool enabled;
        private NativeKeyboardContext context;

        private Font textFont;
        private Brush textBrush;

        private Pen backPen;
        private Brush backBrush;

        private Bitmap lastBitmap;
        private ColorMatrix matrix;
        private ImageAttributes attributes;

        private Point location;

        private int counter;
        private float currentTransparency;
        private float transparencyStep;

        private CustomKeysConverter conv;

       

        public CaptureKeyboard()
        {
            context = new NativeKeyboardContext();
            conv = new CustomKeysConverter();

            textFont = new Font("Segoe UI", 20);
            currentTransparency = 0f;
            transparencyStep = 0.3f;

            textBrush = new SolidBrush(Color.White);

            backPen = new Pen(Color.White, 4f);
            backBrush = new SolidBrush(Color.DarkBlue);

            matrix = new ColorMatrix();
            matrix.Matrix33 = currentTransparency;

            attributes = new ImageAttributes();

            location = new Point(5, 5);
        }

        public bool Enabled
        {
            get { return enabled; }
            set { OnEnabledChanged(value); }
        }

        private void OnEnabledChanged(bool value)
        {
            enabled = value;

            if (value)
                context.Start();
            else context.Stop();
        }



        public void Draw(Graphics graphics)
        {
            string text = conv.ToStringWithModifiers(context.Current);

            if (!String.IsNullOrEmpty(text))
            {
                currentTransparency = 0.8f;
                createBitmap(graphics, text);
                counter = 0;
            }
            else
            {
                if (counter++ > 10)
                {
                    currentTransparency -= transparencyStep;
                    if (currentTransparency <= 0)
                        currentTransparency = 0;
                }
            }

            if (lastBitmap != null)
            {
                matrix.Matrix33 = currentTransparency;
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                graphics.DrawImage((Image)lastBitmap,
                    new Rectangle(location.X, location.Y, lastBitmap.Width, lastBitmap.Height),
                    0, 0, lastBitmap.Width, lastBitmap.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        private void createBitmap(Graphics graphics, string text)
        {
            // Compute size of container
            SizeF size = graphics.MeasureString(text, textFont);

            lastBitmap = new Bitmap((int)size.Width + 50, (int)size.Height + 25);

            using (Graphics g = Graphics.FromImage(lastBitmap))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                RectangleF rect = new RectangleF(0, 0, size.Width + 20, size.Height + 10);

                // Draw container background
                g.FillRectangle(backBrush, rect);
                g.DrawRectangle(backPen, rect.X, rect.Y, rect.Width, rect.Height);

                // Draw text
                g.DrawString(text, textFont, textBrush, 15, 5, StringFormat.GenericTypographic);
            }
        }





        #region IDisposable implementation

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, 
        ///   releasing, or resetting unmanaged resources.
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources and performs other cleanup operations 
        ///   before the <see cref="CaptureKeyboard"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~CaptureKeyboard()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// 
        /// <param name="disposing"><c>true</c> to release both managed
        /// and unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        ///
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }

            }
        }
        #endregion

    }
}
