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

namespace ScreenCapture
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Native Win32 methods.
    /// </summary>
    /// 
    public static partial class NativeMethods
    {

        /// <summary>
        ///   Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        ///   Retrieves the position of the mouse cursor, in screen coordinates.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out NativeMethods.POINT lpPoint);

        /// <summary>
        ///   Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        /// <summary>
        ///   Retrieves the dimensions of the bounding rectangle of the specified
        ///   window. The dimensions are given in screen coordinates that are relative
        ///   to the upper-left corner of the screen.
        /// </summary>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        ///   Retrieves a handle to the desktop window. The desktop window 
        ///   covers the entire screen. The desktop window is the area on top
        ///   of which other windows are painted.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        ///   Retrieves a handle to a device context (DC) for the client area 
        ///   of a specified window or for the entire screen.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);

        /// <summary>
        ///   Retrieves the specified system metric or system configuration setting.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int abc);

        /// <summary>
        ///   The GetWindowDC function retrieves the device context (DC) for the entire
        ///   window, including title bar, menus, and scroll bars.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        /// <summary>
        ///   Releases a device context (DC), freeing it for use by other applications. The
        ///   effect of the ReleaseDC function depends on the type of DC. It frees only common
        ///   and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        /// <summary>
        ///   Retrieves information about the global cursor.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        /// <summary>
        ///   Copies the specified icon from another module to the current module.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        /// <summary>
        ///   Retrieves information about the specified icon or cursor.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        /// <summary>
        ///   Creates a cursor based on data contained in a file.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        ///   Enables, disables, or grays the specified menu item.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        public static extern Int32 EnableMenuItem(System.IntPtr hMenu, Int32 uIDEnableItem, Int32 uEnable);

        /// <summary>
        ///   Retrieves the dimensions of the bounding rectangle of the specified
        ///   window. The dimensions are given in screen coordinates that are relative
        ///   to the upper-left corner of the screen.
        /// </summary>
        /// 
        public static Rectangle GetWindowRect(IntPtr hwnd)
        {
            NativeMethods.RECT r;
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
