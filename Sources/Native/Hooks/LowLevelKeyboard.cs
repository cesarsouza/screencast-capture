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

    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum LowLevelKeyboardMessage : int
    {
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
    }

    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public class KeyboardLowLevelHookStruct
    {
        public uint vkCode;
        public uint scanCode;
        public KeyboardLowLevelFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }

    [Flags]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum KeyboardLowLevelFlags : uint
    {
        LLKHF_EXTENDED = 0x01,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80,
    }
}
