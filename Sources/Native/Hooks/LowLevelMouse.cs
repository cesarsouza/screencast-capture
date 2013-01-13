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
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Low-level mouse procedure delegate.
    /// </summary>
    /// 
    /// <param name="message">The identifier of the mouse message.</param>
    /// <param name="mouse">A pointer to an MSLLHOOKSTRUCT structure</param>
    /// 
    public delegate void LowLevelMouseProcedure(LowLevelMouseMessage message, MouseLowLevelHookStruct mouse);

    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum LowLevelMouseMessage : int
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSEHWHEEL = 0x020E,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDOWN = 0x0204,
    }

    /// <summary>
    ///   Contains information about a low-level mouse input event (MSLLHOOKSTRUCT).
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public struct MouseLowLevelHookStruct
    {
        /// <summary>
        ///   The x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        /// 
        public Point pt;

        /// <summary>
        /// <para>
        ///   If the message is WM_MOUSEWHEEL, the high-order word of this
        ///   member is the wheel delta. The low-order word is reserved. A 
        ///   positive value indicates that the wheel was rotated forward, 
        ///   away from the user; a negative value indicates that the wheel
        ///   was rotated backward, toward the user. One wheel click is defined
        ///   as WHEEL_DELTA, which is 120.</para>
        ///   
        /// <para>
        ///   If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, 
        ///   WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the high-
        ///   order word specifies which X button was pressed or released, and 
        ///   the low-order word is reserved. This value can be one or more of 
        ///   the following values. Otherwise, mouseData is not used.</para>
        /// </summary>
        /// 
        public int mouseData;

        /// <summary>
        ///   The event-injected flag. An application can use the following
        ///   value to test the mouse flags.
        /// </summary>
        /// 
        public int flags;

        /// <summary>
        ///   The time stamp for this message.
        /// </summary>
        /// 
        public int time;

        /// <summary>
        ///   Additional information associated with the message.
        /// </summary>
        /// 
        public UIntPtr dwExtraInfo;
    }
}
