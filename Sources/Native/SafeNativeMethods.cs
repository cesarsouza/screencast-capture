using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;

namespace ScreenCapture.Native
{
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
