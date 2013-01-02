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
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    /// <summary>
    ///   Native Win32 methods.
    /// </summary>
    /// 
    internal static partial class NativeMethods
    {

        public static int MOD_ALT = 0x1;
        public static int MOD_CONTROL = 0x2;
        public static int MOD_SHIFT = 0x4;
        public static int WM_HOTKEY = 0x312;

        public const uint ERROR_HOTKEY_ALREADY_REGISTERED = 1409;


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        ///   Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        ///   Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// 
        [DllImport("user32.dll"), SuppressMessage("Microsoft.Portability",
            "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0")]
        internal static extern IntPtr WindowFromPoint(Point Point);

        /// <summary>
        ///   Retrieves the dimensions of the bounding rectangle of the specified
        ///   window. The dimensions are given in screen coordinates that are relative
        ///   to the upper-left corner of the screen.
        /// </summary>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        ///   Retrieves a handle to the desktop window. The desktop window 
        ///   covers the entire screen. The desktop window is the area on top
        ///   of which other windows are painted.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        /// <summary>
        ///   Retrieves information about the global cursor.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorInfo(out CURSORINFO pci);

        /// <summary>
        ///   Copies the specified icon from another module to the current module.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        internal static extern IntPtr CopyIcon(IntPtr hIcon);

        /// <summary>
        ///   Retrieves information about the specified icon or cursor.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        /// <summary>
        ///   Enables, disables, or grays the specified menu item.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableMenuItem(System.IntPtr hMenu, Int32 uIDEnableItem, Int32 uEnable);

        /// <summary>
        ///   Retrieves the dimensions of the bounding rectangle of the specified
        ///   window. The dimensions are given in screen coordinates that are relative
        ///   to the upper-left corner of the screen.
        /// </summary>
        /// 
        public static Rectangle GetWindowRect(IntPtr hwnd)
        {
            if (hwnd == null)
                throw new ArgumentNullException("hWnd");

            RECT r;
            if (!NativeMethods.GetWindowRect(hwnd, out r))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1);
        }

        /// <summary>
        ///   Title bar position.
        /// </summary>
        /// 
        public static int HTCAPTION = 0x00000002;


        /// <summary>
        ///   Moves the window.
        /// </summary>
        /// 
        public static int SC_MOVE = 0xF010;


        /// <summary>
        ///   Posted when the user presses the left mouse button while the cursor is
        ///   within the nonclient area of a window. This message is posted to the 
        ///   window that contains the cursor. If a window has captured the mouse, 
        ///   this message is not posted.
        /// </summary>
        /// 
        public static int WM_NCLBUTTONDOWN = 0xA1;

        /// <summary>
        ///   A window receives this message when the user chooses a command from the 
        ///   Window menu (formerly known as the system or control menu) or when the user
        ///   chooses the maximize button, minimize button, restore button, or close button.
        /// </summary>
        /// 
        public static int WM_SYSCOMMAND = 0x112;

        /// <summary>
        ///   Sent when a drop-down menu or submenu is about to become active. This allows
        ///   an application to modify the menu before it is displayed, without changing the
        ///   entire menu.
        /// </summary>
        /// 
        public static int WM_INITMENUPOPUP = 0x117;



        /// <summary>
        ///   Indicates that uIDEnableItem gives the identifier of the menu item. If neither
        ///   the MF_BYCOMMAND nor MF_BYPOSITION flag is specified, the MF_BYCOMMAND flag is
        ///   the default flag.
        /// </summary>
        /// 
        public static int MF_BYCOMMAND = 0x00000000;

        /// <summary>
        ///   Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.
        /// </summary>
        /// 
        public static int MF_ENABLED = 0x00000000;

        /// <summary>
        ///   Indicates that the menu item is disabled and grayed so that it cannot be selected.
        /// </summary>
        /// 
        public static int MF_GRAYED = 0x00000001;

        /// <summary>
        ///   Indicates that the menu item is disabled, but not grayed, so it cannot be selected.
        /// </summary>
        /// 
        public static int MF_DISABLED = 0x00000002;

    }
}
