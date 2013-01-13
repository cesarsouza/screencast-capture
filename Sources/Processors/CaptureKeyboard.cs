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

    public class CaptureKeyboard
    {

        private Thread thread;
        private ApplicationContext context;
        private bool enabled;


        public CaptureKeyboard()
        {


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
        }

        private void thread_KeyDown(Keys key)
        {
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
                    thread_KeyUp((Keys)info.vkCode);
                    break;

                case LowLevelKeyboardMessage.WM_KEYDOWN:
                case LowLevelKeyboardMessage.WM_SYSKEYDOWN:
                    thread_KeyDown((Keys)info.vkCode);
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
