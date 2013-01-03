using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ScreenCapture.Native
{
    public class CaptureClick : IDisposable
    {

        private Pen pen;
        private Brush brush;
        private NativeMethods.LowLevelMouseProc myCallbackDelegate = null;
        private Thread thread;
        private ApplicationContext ctx;

        private int currentRadius;
        private Point location;

        public int Radius { get; set; }
        public int StepSize { get; set; }

        public bool Enabled { get; set; }

        public CaptureClick()
        {
            Enabled = true;
            pen = new Pen(Brushes.Black, 5);
            Radius = 100;
            StepSize = 10;

            myCallbackDelegate = new NativeMethods.LowLevelMouseProc(MyCallbackFunction);

            thread = new Thread(threadBody);
            thread.Start();
        }

        public void threadBody()
        {
            IntPtr hHook;

            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                hHook = NativeMethods.SetWindowsHookEx(
                    NativeMethods.HookType.WH_MOUSE_LL, myCallbackDelegate, hModule, 0);
            }

            ctx = new ApplicationContext();
            Application.Run(ctx);

            NativeMethods.UnhookWindowsHookEx(hHook);
        }

        public void Draw(Graphics g)
        {
            if (currentRadius <= 0)
                return;

            int x = location.X - currentRadius;
            int y = location.Y - currentRadius;
            int width = currentRadius * 2;
            int height = currentRadius * 2;

            g.DrawEllipse(pen, x, y, width, height);

            if (!pressed)
            {
                currentRadius -= StepSize;
                if (currentRadius <= 0)
                    currentRadius = 0;
            }
        }


        private bool pressed;

        protected void OnMouseUp(MouseButtons button, Point point)
        {
            pressed = false;
        }

        protected void OnMouseDown(MouseButtons button, Point point)
        {
            pressed = true;
            location = point;
            currentRadius = Radius;
        }

        protected void OnMouseMove(MouseButtons button, Point point)
        {
            if (pressed)
                location = point;
        }

        private int MyCallbackFunction(int code, IntPtr wParam, IntPtr lParam)
        {
            if (!Enabled)
                return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            // From 
            // http://msdn.microsoft.com/en-us/library/windows/desktop/ms644986(v=vs.85).aspx
            //
            // wParam contains the identifier of the mouse message.
            // lParam contains a pointer to a MOUSEHOOKSTRUCT structure.
            //
            // The wParam can be can be one of the following messages: WM_LBUTTONDOWN,
            // WM_LBUTTONUP, WM_MOUSEMOVE, WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN,
            // or WM_RBUTTONUP.

            if (code < 0)
                return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            NativeMethods.MouseHookStruct MyMouseHookStruct =
                    (NativeMethods.MouseHookStruct)Marshal.PtrToStructure(lParam,
                    typeof(NativeMethods.MouseHookStruct));

            // Parse the message identifier 
            int msg = wParam.ToInt32();

            if (msg == NativeMethods.WM_LBUTTONDOWN)
                OnMouseDown(MouseButtons.Left, MyMouseHookStruct.pt);

            else if (msg == NativeMethods.WM_LBUTTONUP)
                OnMouseUp(MouseButtons.Left, MyMouseHookStruct.pt);

            else if (msg == NativeMethods.WM_MOUSEMOVE)
                OnMouseMove(MouseButtons.Left, MyMouseHookStruct.pt);

            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
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
                if (ctx != null)
                {
                    ctx.ExitThread();
                    ctx.Dispose();
                }

                // free managed resources
                pen.Dispose();
                pen = null;
            }
        }
        #endregion

    }
}
