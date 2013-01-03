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
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    ///   Managed wrapper around native methods.
    /// </summary>
    /// 
    public static class SafeNativeMethods
    {

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

    }
}
