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

    public class CaptureKeyboard
    {

        private Thread thread;
        private ApplicationContext context;
        private bool enabled;

        private Font textFont;
        private Brush textBrush;

        private Pen backPen;
        private Brush backBrush;

        private Bitmap lastBitmap;
        private ColorMatrix matrix;
        private ImageAttributes attributes;

        private Keys current;
        private Point location;

        private float currentTransparency;
        private float transparencyStep;

        CustomKeysConverter conv = new CustomKeysConverter();

        StringBuilder builder = new StringBuilder(100);


        public CaptureKeyboard()
        {
            textFont = new Font("Segoe UI", 14);
            currentTransparency = 0f;
            transparencyStep = 0.3f;

            textBrush = new SolidBrush(Color.White);

            backPen = new Pen(Color.White);
            backBrush = new SolidBrush(Color.DarkBlue);

            matrix = new ColorMatrix();
            matrix.Matrix33 = currentTransparency;

            attributes = new ImageAttributes();

            location = new Point(5, 5);
        }


        /// <summary>
        ///   Gets or sets whether the hook is installed and running.
        ///   Has no effect on debug mode, as stopping at a breakpoint
        ///   would freeze the mouse.
        /// </summary>
        /// 
        public bool Enabled
        {
            get { return enabled; }
            set { OnEnabledChanged(value); }
        }



        public void Draw(Graphics graphics)
        {


            string text = conv.ToStringWithModifiers(current);

            if (!String.IsNullOrEmpty(text))
            {
                currentTransparency = 0.8f;
                createBitmap(graphics, text);
            }
            else
            {
                currentTransparency -= transparencyStep;
                if (currentTransparency <= 0)
                    currentTransparency = 0;
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
            lastBitmap = new Bitmap((int)size.Width + 20, (int)size.Height + 20);

            using (Graphics g = Graphics.FromImage(lastBitmap))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                RectangleF rect = new RectangleF(0, 0, size.Width + 10, size.Height + 10);

                // Draw container background
                g.FillRectangle(backBrush, rect);
                g.DrawRectangle(backPen, rect.X, rect.Y, rect.Width, rect.Height);

                // Draw text
                g.DrawString(text, textFont, textBrush, 5, 5, StringFormat.GenericTypographic);
            }
        }



        private void OnEnabledChanged(bool value)
        {
            enabled = value;

            if (value)
                threadStart();
            else
                threadStop();
        }



        private void thread_KeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    current &= ~Keys.Control;
                    return;

                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    current &= ~Keys.Shift;
                    return;

                case Keys.RMenu:
                case Keys.LMenu:
                    current &= ~Keys.Alt;
                    return;

                case Keys.RWin:
                case Keys.LWin:
                    current &= ~KeysExtensions.Windows;
                    return;
            }

            current = Keys.None.SetModifiers(current);
        }

        private void thread_KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.Control:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    current |= Keys.Control;
                    return;

                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    current |= Keys.Shift;
                    return;

                case Keys.RMenu:
                case Keys.LMenu:
                    current |= Keys.Alt;
                    return;

                case Keys.RWin:
                case Keys.LWin:
                    current |= KeysExtensions.Windows;
                    return;
            }

            current = key.SetModifiers(current);
        }


        private void threadStart()
        {
            if (context != null)
                return;

            context = new ApplicationContext();
            thread = new Thread(run);

            thread.Start();
        }

        private void threadStop()
        {
            if (context == null)
                return;

            context.ExitThread();
            context.Dispose();
            context = null;
        }

        private void run()
        {
            // Install the global mouse hook and starts its own message pump
            using (HookHandle hook = SafeNativeMethods.SetWindowHook(lowLevelKeyboardProcHook))
            {
                Application.Run(context);
            }
        }

        private void lowLevelKeyboardProcHook(LowLevelKeyboardMessage message, KeyboardLowLevelHookStruct info)
        {
            switch (message)
            {
                case LowLevelKeyboardMessage.WM_KEYUP:
                case LowLevelKeyboardMessage.WM_SYSKEYUP:
                    thread_KeyUp((Keys)info.vkCode); break;

                case LowLevelKeyboardMessage.WM_KEYDOWN:
                case LowLevelKeyboardMessage.WM_SYSKEYDOWN:
                    thread_KeyDown((Keys)info.vkCode); break;

                default: break;
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
                    context.ExitThread();
                    context.Dispose();
                    context = null;
                }

            }
        }
        #endregion

    }
}
