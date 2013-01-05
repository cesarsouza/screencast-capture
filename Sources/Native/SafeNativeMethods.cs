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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    ///   Managed wrapper around native methods.
    /// </summary>
    /// 
    public static class SafeNativeMethods
    {

        /// <summary>
        ///   Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// 
        public static IWin32Window WindowFromPoint(Point point)
        {
            IntPtr ptr = NativeMethods.WindowFromPoint(point);

            return new WindowHandle(ptr, false);
        }

        /// <summary>
        ///   Retrieves the dimensions of the bounding rectangle of the specified
        ///   window. The dimensions are given in screen coordinates that are relative
        ///   to the upper-left corner of the screen.
        /// </summary>
        /// 
        public static bool TryGetWindowRect(IWin32Window window, out Rectangle rectangle)
        {
            rectangle = Rectangle.Empty;

            if (window == null)
                return false;

            RECT r;
            if (!NativeMethods.GetWindowRect(window.Handle, out r))
                return false;

            rectangle = new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1);

            return true;
        }


        /// <summary>
        ///   Registers a low-level global system hook.
        /// </summary>
        /// 
        public static HookHandle SetWindowHook(LowLevelMouseProcedure callback)
        {
            IntPtr hHook;
            NativeMethods.LowLevelMouseProc lpfn;

            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                lpfn = new NativeMethods.LowLevelMouseProc((nCode, wParam, lParam) =>
                {
                    // From 
                    // http://msdn.microsoft.com/en-us/library/windows/desktop/ms644986(v=vs.85).aspx
                    //
                    // wParam contains the identifier of the mouse message.
                    // lParam contains a pointer to a MOUSEHOOKSTRUCT structure.
                    //
                    // The wParam can be can be one of the following messages: WM_LBUTTONDOWN,
                    // WM_LBUTTONUP, WM_MOUSEMOVE, WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN,
                    // or WM_RBUTTONUP.

                    if (nCode < 0)
                        return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

                    MouseLowLevelHookStruct mouseInfo =
                        (MouseLowLevelHookStruct)Marshal.PtrToStructure(lParam,
                        typeof(MouseLowLevelHookStruct));

                    callback(wParam.ToInt32(), mouseInfo);

                    return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
                });

                hHook = NativeMethods.SetWindowsHookEx(
                    NativeMethods.HookType.WH_MOUSE_LL, lpfn, hModule, 0);
            }

            return new HookHandle(hHook, lpfn);
        }

        /// <summary>
        ///   Defines a system-wide hot key.
        /// </summary>
        /// 
        public static void RegisterHotKey(IWin32Window window, Keys key, int id)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            int error;
            if (!registerHotKey(window, key, id, out error))
                throw new Win32Exception(error);
        }

        /// <summary>
        ///   Unregisters a system-wide hot key.
        /// </summary>
        /// 
        public static void UnregisterHotKey(IWin32Window window, int id)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            if (!NativeMethods.UnregisterHotKey(window.Handle, id))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        ///   Attempts to register a system-wide hot key.
        /// </summary>
        /// 
        public static bool TryRegisterHotKey(IWin32Window window, Keys key, int id)
        {
            int error;
            return registerHotKey(window, key, id, out error);
        }


        private static bool registerHotKey(IWin32Window window, Keys key, int id, out int error)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            int modifiers = 0;

            if ((key & Keys.Alt) == Keys.Alt)
                modifiers = modifiers | NativeMethods.MOD_ALT;

            if ((key & Keys.Control) == Keys.Control)
                modifiers = modifiers | NativeMethods.MOD_CONTROL;

            if ((key & Keys.Shift) == Keys.Shift)
                modifiers = modifiers | NativeMethods.MOD_SHIFT;

            Keys vk = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;
            int fsModifiers = modifiers;

            error = 0;
            if (!NativeMethods.RegisterHotKey(window.Handle, id, (uint)modifiers, (uint)vk))
            {
                error = Marshal.GetLastWin32Error();
                return false;
            }

            return true;
        }

        /// <summary>
        ///   Gets the mouse button written in a windows message.
        /// </summary>
        /// 
        public static MouseButtons GetMouseButton(int message)
        {
            switch (message)
            {
                case NativeMethods.WM_LBUTTONDOWN:
                case NativeMethods.WM_LBUTTONUP:
                    return MouseButtons.Left;

                case NativeMethods.WM_RBUTTONUP:
                case NativeMethods.WM_RBUTTONDOWN:
                    return MouseButtons.Right;

                default:
                    return MouseButtons.None;
            }
        }
    }


    /// <summary>
    ///   Low-level mouse procedure delegate.
    /// </summary>
    /// 
    /// <param name="message">The identifier of the mouse message.</param>
    /// <param name="mouse">A pointer to an MSLLHOOKSTRUCT structure</param>
    public delegate void LowLevelMouseProcedure(int message, MouseLowLevelHookStruct mouse);

    /// <summary>
    ///   Managed wrapper around a native window handle.
    /// </summary>
    /// 
    public class WindowHandle : SafeHandle, IWin32Window
    {
        /// <summary>
        ///   Gets the handle for the hook.
        /// </summary>
        /// 
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HookHandle" /> class.
        /// </summary>
        /// 
        /// <param name="handle">The handle.</param>
        /// <param name="ownsHandle">True to reliably let <see cref="WindowHandle"/> release the
        ///     handle during the finalization phase; otherwise, false (not recommended).</param>
        /// 
        public WindowHandle(IntPtr handle, bool ownsHandle)
            : base(IntPtr.Zero, ownsHandle)
        {
            Handle = handle;
        }

        /// <summary>
        ///   When overridden in a derived class, gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// 
        /// <returns>true if the handle value is invalid; otherwise, false.</returns>
        /// 
        ///   <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        ///   </PermissionSet>
        ///   
        public override bool IsInvalid
        {
            get { return Handle == IntPtr.Zero; }
        }

        /// <summary>
        ///   When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// 
        /// <returns>
        ///   true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        /// 
        protected override bool ReleaseHandle()
        {
            return NativeMethods.DestroyWindow(this.handle);
        }
    }

    /// <summary>
    ///   Managed wrapper around an input hook pointer.
    /// </summary>
    /// 
    public class HookHandle : SafeHandle
    {
        /// <summary>
        ///   Gets the handle for the hook.
        /// </summary>
        /// 
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///   Gets the callback hook.
        /// </summary>
        /// 
        public Delegate Callback { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HookHandle" /> class.
        /// </summary>
        /// 
        /// <param name="handle">The handle.</param>
        /// <param name="callback">The callback function.</param>
        /// 
        public HookHandle(IntPtr handle, Delegate callback)
            : base(IntPtr.Zero, true)
        {
            Handle = handle;
            Callback = callback;
        }

        /// <summary>
        ///   When overridden in a derived class, gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// 
        /// <returns>true if the handle value is invalid; otherwise, false.</returns>
        /// 
        ///   <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        ///   </PermissionSet>
        ///   
        public override bool IsInvalid
        {
            get { return Handle == IntPtr.Zero; }
        }

        /// <summary>
        ///   When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// 
        /// <returns>
        ///   true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        /// 
        protected override bool ReleaseHandle()
        {
            return NativeMethods.UnhookWindowsHookEx(Handle);
        }
    }
}
