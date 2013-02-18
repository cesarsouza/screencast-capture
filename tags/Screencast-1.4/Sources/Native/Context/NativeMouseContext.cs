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
    ///   Windows application context for grabbing global mouse information.
    /// </summary>
    /// 
    public class NativeMouseContext : IDisposable
    {
        private Thread thread;
        private ApplicationContext context;

        /// <summary>
        ///   Gets the current mouse position.
        /// </summary>
        /// 
        public Point Current { get; private set; }

        /// <summary>
        ///   Occurs when the mouse button is released.
        /// </summary>
        /// 
        public event EventHandler MouseUp;

        /// <summary>
        ///   Occurs when the mouse button is pressed.
        /// </summary>
        /// 
        public event EventHandler MouseDown;

        /// <summary>
        ///   Ocurrs when the mouse moves.
        /// </summary>
        /// 
        public event EventHandler MouseMove;


        /// <summary>
        ///   Initializes a new instance of the <see cref="NativeMouseContext"/> class.
        /// </summary>
        /// 
        public NativeMouseContext()
        {
        }

        /// <summary>
        ///   Install the global hook and starts
        ///   listening to global mouse events.
        /// </summary>
        /// 
        public void Start()
        {
            if (context != null)
                return;

            context = new ApplicationContext();
            thread = new Thread(run);

#if !DEBUG
            thread.Start();
#endif
        }

        /// <summary>
        ///   Detaches the global mouse hook
        ///   and stops listening for events.
        /// </summary>
        /// 
        public void Stop()
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

        private void lowLevelMouseProcHook(LowLevelMouseMessage message, MouseLowLevelHookStruct info)
        {
            Current = info.pt;

            switch (message)
            {
                case LowLevelMouseMessage.WM_LBUTTONUP:
                case LowLevelMouseMessage.WM_RBUTTONUP:
                    if (MouseUp != null) MouseUp(this, EventArgs.Empty);
                    break;

                case LowLevelMouseMessage.WM_LBUTTONDOWN:
                case LowLevelMouseMessage.WM_RBUTTONDOWN:
                    if (MouseDown != null) MouseDown(this, EventArgs.Empty);
                    break;

                case LowLevelMouseMessage.WM_MOUSEMOVE:
                    if (MouseMove != null) MouseMove(this, EventArgs.Empty);
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
        ///   before the <see cref="NativeMouseContext"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~NativeMouseContext()
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
