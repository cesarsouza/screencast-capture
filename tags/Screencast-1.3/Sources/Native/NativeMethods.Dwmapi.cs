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
    using System.CodeDom.Compiler;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Native Win32 methods.
    /// </summary>
    /// 
    internal static partial class NativeMethods
    {

        [DllImport("dwmapi.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref ThemeMargins margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DwmIsCompositionEnabled();

    }

    /// <summary>
    ///   MARGINS structure (Windows).
    /// </summary>
    /// 
    /// <remarks>
    ///   Returned by the GetThemeMargins function to define the 
    ///   margins of windows that have visual styles applied.
    ///   
    ///   http://msdn.microsoft.com/en-us/library/windows/desktop/bb773244(v=vs.85).aspx
    /// </remarks>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public struct ThemeMargins
    {
        /// <summary>
        ///   Width of the left border that retains its size.
        /// </summary>
        /// 
        public int LeftWidth;

        /// <summary>
        ///   Width of the right border that retains its size.
        /// </summary>
        /// 
        public int RightWidth;

        /// <summary>
        ///   Height of the top border that retains its size.
        /// </summary>
        /// 
        public int TopHeight;

        /// <summary>
        ///   Height of the bottom border that retains its size.
        /// </summary>
        /// 
        public int BottomHeight;
    }
}
