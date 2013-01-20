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
    using System.Text;

    /// <summary>
    ///   Managed wrapper around native methods.
    /// </summary>
    /// 
    public static class SafeNativeMethods
    {

        /// <summary>
        ///   Extends the window frame into the client area.
        /// </summary>
        /// 
        /// <param name="window">
        ///   The handle to the window in which the frame will
        ///   be extended into the client area.</param>
        /// <param name="margins">
        ///   A pointer to a <see cref="ThemeMargins"/> structure that describes
        ///   the margins to use when extending the frame into the client area.</param>
        ///   
        public static void ExtendAeroGlassIntoClientArea(IWin32Window window, ThemeMargins margins)
        {
            if (window == null) throw new ArgumentNullException("window");
            NativeMethods.DwmExtendFrameIntoClientArea(window.Handle, ref margins);
        }

        /// <summary>
        ///   Obtains a value that indicates whether Desktop
        ///   Window Manager (DWM) composition is enabled. 
        /// </summary>
        /// 
        public static bool IsAeroEnabled
        {
            get { return NativeMethods.DwmIsCompositionEnabled(); }
        }

        /// <summary>
        ///   Gets a handle to a cursor icon.
        /// </summary>
        /// 
        public static IconHandle GetCursorIcon(CursorInfo cursor)
        {
            IntPtr hIcon = NativeMethods.CopyIcon(cursor.hCursor);

            if (hIcon == IntPtr.Zero)
                return null;

            return new IconHandle(hIcon);
        }

        /// <summary>
        ///   Gets information about a icon from a icon handle.
        /// </summary>
        /// 
        public static IconHandleInfo GetIconInfo(IconHandle iconHandle)
        {
            if (iconHandle == null)
                return null;

            NativeMethods.ICONINFO iconInfo;
            if (!NativeMethods.GetIconInfo(iconHandle.Handle, out iconInfo))
                return null;
            return new IconHandleInfo(iconInfo);
        }

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

            Rect r;
            if (!NativeMethods.GetWindowRect(window.Handle, out r))
                return false;

            rectangle = new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1);

            return true;
        }


        /// <summary>
        ///   Registers a low-level global mouse system hook.
        /// </summary>
        /// 
        public static HookHandle SetWindowHook(LowLevelMouseProcedure callback)
        {
            IntPtr hHook;
            NativeMethods.LowLevelHookProc lpfn;

            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                lpfn = new NativeMethods.LowLevelHookProc((nCode, wParam, lParam) =>
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

                    callback((LowLevelMouseMessage)(wParam.ToInt32()), mouseInfo);

                    return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
                });

                hHook = NativeMethods.SetWindowsHookEx(
                    NativeMethods.HookType.WH_MOUSE_LL, lpfn, hModule, 0);
            }

            return new HookHandle(hHook, lpfn);
        }

        /// <summary>
        ///   Registers a low-level global keyboard system hook.
        /// </summary>
        /// 
        public static HookHandle SetWindowHook(LowLevelKeyboardProcedure callback)
        {
            IntPtr hHook;
            NativeMethods.LowLevelHookProc lpfn;

            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                lpfn = new NativeMethods.LowLevelHookProc((nCode, wParam, lParam) =>
                {
                    // From 
                    // http://msdn.microsoft.com/en-us/library/windows/desktop/ms644985(v=vs.85).aspx
                    //
                    // wParam contains the identifier of the keyboard message.
                    // lParam contains a pointer to a KBDLLHOOKSTRUCT structure.
                    //
                    // The wParam can be can be one of the following messages: WM_KEYDOWN,
                    // WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP.

                    if (nCode < 0)
                        return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

                    KeyboardLowLevelHookStruct keyboardInfo =
                        (KeyboardLowLevelHookStruct)Marshal.PtrToStructure(lParam,
                        typeof(KeyboardLowLevelHookStruct));

                    callback((LowLevelKeyboardMessage)(wParam.ToInt32()), keyboardInfo);

                    return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
                });

                hHook = NativeMethods.SetWindowsHookEx(
                    NativeMethods.HookType.WH_KEYBOARD_LL, lpfn, hModule, 0);
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
        public static MouseButtons GetMouseButton(LowLevelMouseMessage message)
        {
            switch (message)
            {
                case LowLevelMouseMessage.WM_LBUTTONDOWN:
                case LowLevelMouseMessage.WM_LBUTTONUP:
                    return MouseButtons.Left;

                case LowLevelMouseMessage.WM_RBUTTONUP:
                case LowLevelMouseMessage.WM_RBUTTONDOWN:
                    return MouseButtons.Right;

                default:
                    return MouseButtons.None;
            }
        }

        /// <summary>
        ///   Creates a device context compatible with the specified surface.
        /// </summary>
        /// 
        public static DeviceHandle CreateCompatibleDC(Graphics graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            IntPtr source = graphics.GetHdc();

            IntPtr handle = NativeMethods.CreateCompatibleDC(source);
            DeviceHandle dc = new DeviceHandle(handle);

            graphics.ReleaseHdc(source);

            return dc;
        }
    }



}
