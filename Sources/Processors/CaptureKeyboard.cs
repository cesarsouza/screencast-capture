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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading;
    using ScreenCapture.Native;
    using System.Drawing;

    public class CaptureKeyboard
    {

        private Thread thread;
        private ApplicationContext context;
        private bool enabled;

        private Font textFont;
        private Brush textBrush;

        private Pen backPen;
        private Brush backBrush;

        private bool hasAlt;
        private bool hasCtrl;
        private bool hasShift;
        private bool hasMeta;
        private Keys current;

        private int fade = 0;
        private bool fadingIn;
        private bool fadingOut;
        private int currentTransparency;
        private int transparencyStep;

        StringBuilder builder = new StringBuilder(100);


        public Point location;


        public CaptureKeyboard()
        {
            textFont = new Font("Segoe UI", 14);
            textBrush = new SolidBrush(Color.White);

            backPen = new Pen(Color.White);
            backBrush = new SolidBrush(Color.FromArgb(200, Color.DarkBlue));

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
            if (current == Keys.None && !hasAlt && !hasCtrl && !hasShift && !hasMeta)
                return;

            string text = getString();

            SizeF size = graphics.MeasureString(text, textFont);

            // Compute size of container
            RectangleF rect = new RectangleF(location.X, location.Y, size.Width + 10, size.Height + 10);

            // Draw container background
            graphics.FillRectangle(backBrush, rect);
            graphics.DrawRectangle(backPen, rect.X, rect.Y, rect.Width, rect.Height);

            // Draw text
            graphics.DrawString(text, textFont, textBrush, location.X + 5, location.Y + 5, StringFormat.GenericTypographic);
        }


        private string getString()
        {
            builder.Clear();

            if (hasShift)
            {
                builder.Append("Shift");
            }
            if (hasCtrl)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Control");
            }
            if (hasAlt)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Alt");
            }
            if (hasMeta)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Meta");
            }

            if (current != Keys.None)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");

                string raw = getRawKeyChar();
                string mod = getKeyCharWithModifiers(hasShift, hasAlt);

                builder.Append(raw);

                if (raw != mod)
                    builder.AppendFormat(" ({0})", mod);
            }

            return builder.ToString();
        }



        private byte[] stateInitial = new byte[256];
        private byte[] stateCurrent = new byte[256];
        StringBuilder charBuffer = new StringBuilder(256);

        private string getRawKeyChar()
        {
            return SafeNativeMethods.GetCharsFromKeys(current, stateInitial, charBuffer);
        }

        private string getKeyCharWithModifiers(bool shift, bool altGr)
        {
            stateCurrent[(int)Keys.ShiftKey] = shift ? (byte)0xff : (byte)0x00;
            stateCurrent[(int)Keys.ControlKey] = altGr ? (byte)0xff : (byte)0x00;
            stateCurrent[(int)Keys.Menu] = altGr ? (byte)0xff : (byte)0x00;

            return SafeNativeMethods.GetCharsFromKeys(current, stateCurrent, charBuffer);
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
            if (!setModifierKeys(key, false))
                current = Keys.None;
        }

        private void thread_KeyDown(Keys key)
        {
            if (!setModifierKeys(key, true))
                current = key;
        }

        private bool setModifierKeys(Keys key, bool modifier)
        {
            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    hasCtrl = modifier;
                    return true;

                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    hasShift = modifier;
                    return true;

                case Keys.RMenu:
                case Keys.LMenu:
                    hasAlt = modifier;
                    return true;

                case Keys.RWin:
                case Keys.LWin:
                    hasMeta = modifier;
                    return true;
            }

            return false;
        }


        private void threadStart()
        {
            if (context != null)
                return;

            context = new ApplicationContext();
            thread = new Thread(run);

#if !DEBUG
            thread.Start();
#endif
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
