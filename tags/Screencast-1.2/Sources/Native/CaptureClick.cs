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

namespace ScreenCapture.Native
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    ///   Class to capture mouse clicks.
    /// </summary>
    /// 
    /// <remarks>
    ///   This class captures global mouse clicks by using a low-level global
    ///   hook. To prevent messing with the operational system responsiveness,
    ///   this class uses its own thread with its own message queue to process
    ///   global mouse events and dispatch them to the user interface thread.
    /// </remarks>
    /// 
    public class CaptureClick : IDisposable
    {

        private Pen penOuter;
        private Pen penInner;

        private Thread thread;
        private ApplicationContext context;

        private bool enabled;
        private bool pressed;
        private int currentRadius;
        private Point currentLocation;

        /// <summary>
        ///   Gets or sets the initial indicator 
        ///   radius for the click animation. 
        /// </summary>
        /// 
        public int Radius { get; set; }

        /// <summary>
        ///   Gets or sets the step size at which the <see cref="Radius"/>
        ///   is shrinked at each call to <see cref="Draw"/>.
        /// </summary>
        /// 
        public int StepSize { get; set; }

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


        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureClick" /> class.
        /// </summary>
        /// 
        public CaptureClick()
        {
            penOuter = new Pen(Brushes.Black, 3);
            penInner = new Pen(Color.FromArgb(200, Color.White), 5);

            Radius = 60;
            StepSize = 16;
        }

        /// <summary>
        ///   Draws the click animation into a Graphics object.
        /// </summary>
        /// 
        public void Draw(Graphics graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (currentRadius <= 0)
                return;

            drawCircle(graphics);

            if (!pressed)
            {
                currentRadius -= StepSize;
                if (currentRadius <= 0)
                    currentRadius = 0;
            }
        }

        private void drawCircle(Graphics graphics)
        {
            drawCircle(graphics, currentRadius, penOuter);
            drawCircle(graphics, currentRadius - 5, penInner);
            drawCircle(graphics, currentRadius - 10, penOuter);
        }

        private void drawCircle(Graphics graphics, int radius, Pen pen)
        {
            if (radius <= 0) return;
            int x = currentLocation.X - radius;
            int y = currentLocation.Y - radius;
            int d = radius * 2;

            graphics.DrawEllipse(pen, x, y, d, d);
        }



        private void thread_MouseUp()
        {
            this.pressed = false;
        }

        private void thread_MouseMove(Point location)
        {
            if (pressed)
                this.currentLocation = location;
        }

        private void thread_MouseDown(Point location)
        {
            this.pressed = true;
            this.currentLocation = location;
            this.currentRadius = Radius;
        }

        private void OnEnabledChanged(bool value)
        {
            enabled = value;

            if (value)
                threadStart();
            else
                threadStop();
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
            using (HookHandle hook = SafeNativeMethods.SetWindowHook(lowLevelMouseProcHook))
            {
                Application.Run(context);
            }
        }

        private void lowLevelMouseProcHook(int message, MouseLowLevelHookStruct info)
        {
            switch (message)
            {
                case NativeMethods.WM_LBUTTONUP:
                case NativeMethods.WM_RBUTTONUP:
                    thread_MouseUp();
                    break;

                case NativeMethods.WM_LBUTTONDOWN:
                case NativeMethods.WM_RBUTTONDOWN:
                    thread_MouseDown(info.pt);
                    break;

                case NativeMethods.WM_MOUSEMOVE:
                    thread_MouseMove(info.pt);
                    break;

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
        ///   before the <see cref="CaptureClick"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~CaptureClick()
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

                if (penOuter != null)
                {
                    penOuter.Dispose();
                    penInner.Dispose();

                    penOuter = null;
                    penInner = null;
                }
            }
        }
        #endregion

    }
}
