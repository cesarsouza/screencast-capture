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
    using System.Threading;
    using System.Windows.Forms;
    using ScreenCapture.Native;
    using ScreenCapture.Native.Context;

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

        private bool enabled;
        private NativeMouseContext context;

        private Pen penOuter;
        private Pen penInner;

        private bool pressed;
        private int currentRadius;

        private Point currentLocation;
        private Point relativeLocation;

        /// <summary>
        ///   Gets or sets the current capture region
        ///   (region of interest) of the processor.
        /// </summary>
        /// 
        public Rectangle CaptureRegion { get; set; }

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
            context = new NativeMouseContext();
            context.MouseUp += thread_MouseUp;
            context.MouseDown += thread_MouseDown;
            context.MouseMove += thread_MouseMove;

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

            if (!CaptureRegion.Contains(currentLocation))
                return;

            relativeLocation.X = currentLocation.X - CaptureRegion.X;
            relativeLocation.Y = currentLocation.Y - CaptureRegion.Y;

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
            int x = relativeLocation.X - radius;
            int y = relativeLocation.Y - radius;
            int d = radius * 2;

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        private void OnEnabledChanged(bool value)
        {
            enabled = value;

            if (value)
                context.Start();
            else context.Stop();
        }




        private void thread_MouseUp(object sender, EventArgs e)
        {
            this.pressed = false;
        }

        private void thread_MouseMove(object sender, EventArgs e)
        {
            if (pressed)
                this.currentLocation = context.Position;
        }

        private void thread_MouseDown(object sender, EventArgs e)
        {
            this.pressed = true;
            this.currentLocation = context.Position;
            this.currentRadius = Radius;
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
