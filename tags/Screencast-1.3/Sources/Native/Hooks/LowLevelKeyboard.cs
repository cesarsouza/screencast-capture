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
    ///   Low-level keyboard procedure delegate.
    /// </summary>
    /// 
    /// <param name="message">The identifier of the keyboard message.</param>
    /// <param name="keyboard">A pointer to a KBDLLHOOKSTRUCT structure.</param>
    /// 
    public delegate void LowLevelKeyboardProcedure(LowLevelKeyboardMessage message, KeyboardLowLevelHookStruct keyboard);

    /// <summary>
    ///   Low-level hook keyboard message.
    /// </summary>
    /// 
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum LowLevelKeyboardMessage : int
    {
        /// <summary>A key has been pressed.</summary>
        WM_KEYDOWN = 0x0100,
        /// <summary>A key has been released.</summary>
        WM_KEYUP = 0x0101,
        /// <summary>A modifier key has been pressed.</summary>
        WM_SYSKEYDOWN = 0x0104,
        /// <summary>A modifier key has been released.</summary>
        WM_SYSKEYUP = 0x0105,
    }

    /// <summary>
    ///   Contains information about a low-level mouse input event (KBDLLHOOKSTRUCT).
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public class KeyboardLowLevelHookStruct
    {
        /// <summary>
        ///   A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        /// 
        public uint vkCode;

        /// <summary>
        ///   A hardware scan code for the key.
        /// </summary>
        /// 
        public uint scanCode;

        /// <summary>
        ///   The extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        /// 
        public uint flags;

        /// <summary>
        ///   The time stamp for this message, equivalent to what GetMessageTime would return for this message.
        /// </summary>
        /// 
        public uint time;

        /// <summary>
        ///   Additional information associated with the message.
        /// </summary>
        /// 
        public UIntPtr dwExtraInfo;
    }
    
}
