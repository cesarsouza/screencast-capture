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
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    ///   Provides conversions from <see cref="Keys"/> to String representations,
    ///   with support for key names, unicode chars, modifier and non-char keys.
    /// </summary>
    /// 
    public class CustomKeysConverter
    {

        private byte[] stateInitial = new byte[256];
        private byte[] stateCurrent = new byte[256];
        StringBuilder charBuffer = new StringBuilder(256);

        /// <summary>
        ///   Converts a <see cref="Keys">key</see> into a string containing the key name.
        /// </summary>
        /// 
        public string ToKeyNameString(Keys key)
        {
            switch (key)
            {
                case Keys.Cancel: return "Cancel";
                case Keys.Back: return "Backspace";
                case Keys.Tab: return "Tab";
                case Keys.LineFeed: return "Line Feed";
                case Keys.Clear: return "Clear";
                case Keys.Enter: return "Enter";
                case Keys.ShiftKey: return "Shift";
                case Keys.ControlKey: return "Control";
                case Keys.Menu: return "Alt";
                case Keys.Pause: return "Pause";
                case Keys.CapsLock: return "Caps Lock";
                case Keys.Escape: return "Escape";
                case Keys.Space: return "Space";
                case Keys.PageUp: return "Page Up";
                case Keys.PageDown: return "Page Down";
                case Keys.End: return "End";
                case Keys.Home: return "Home";
                case Keys.Left: return "Arrow Left";
                case Keys.Up: return "Arrow Up";
                case Keys.Right: return "Arrow Right";
                case Keys.Down: return "Arrow Down";
                case Keys.Select: return "Select";
                case Keys.Print: return "Print";
                case Keys.Execute: return "Execute";
                case Keys.PrintScreen: return "Print Screen";
                case Keys.Insert: return "Insert";
                case Keys.Delete: return "Delete";
                case Keys.Help: return "Help";
                case Keys.LWin: return "Windows";
                case Keys.RWin: return "Windows";
                case Keys.Apps: return "Apps";
                case Keys.Sleep: return "Sleep";
                case Keys.NumPad0: return "Num 0";
                case Keys.NumPad1: return "Num 1";
                case Keys.NumPad2: return "Num 2";
                case Keys.NumPad3: return "Num 3";
                case Keys.NumPad4: return "Num 4";
                case Keys.NumPad5: return "Num 5";
                case Keys.NumPad6: return "Num 6";
                case Keys.NumPad7: return "Num 7";
                case Keys.NumPad8: return "Num 8";
                case Keys.NumPad9: return "Num 9";
                case Keys.Multiply: return "Num *";
                case Keys.Add: return "Num +";
                case Keys.Separator: return "Num Separator";
                case Keys.Subtract: return "Num -";
                case Keys.Decimal: return "Num Decimal";
                case Keys.Divide: return "/";
                case Keys.F1: return "F1";
                case Keys.F2: return "F2";
                case Keys.F3: return "F3";
                case Keys.F4: return "F4";
                case Keys.F5: return "F5";
                case Keys.F6: return "F6";
                case Keys.F7: return "F7";
                case Keys.F8: return "F8";
                case Keys.F9: return "F9";
                case Keys.F10: return "F10";
                case Keys.F11: return "F11";
                case Keys.F12: return "F12";
                case Keys.NumLock: return "Num Lock";
                case Keys.Scroll: return "Scroll Lock";
                case Keys.LShiftKey: return "Left Shift";
                case Keys.RShiftKey: return "Right Shift";
                case Keys.LControlKey: return "Left Control";
                case Keys.RControlKey: return "Right Control";
                case Keys.LMenu: return "Left Alt";
                case Keys.RMenu: return "Right Alt";
                case Keys.BrowserBack: return "Browser Back";
                case Keys.BrowserForward: return "Browser Forward";
                case Keys.BrowserRefresh: return "Browser Refresh";
                case Keys.BrowserStop: return "Browser Stop";
                case Keys.BrowserSearch: return "Browser Search";
                case Keys.BrowserFavorites: return "Browser Favorites";
                case Keys.BrowserHome: return "Browser Home";
                case Keys.VolumeMute: return "Vol. Mute";
                case Keys.VolumeDown: return "Vol. Down";
                case Keys.VolumeUp: return "Vol. Up";
                case Keys.MediaNextTrack: return "Media Next Track";
                case Keys.MediaPreviousTrack: return "Media Previous Track";
                case Keys.MediaStop: return "Media Stop";
                case Keys.MediaPlayPause: return "Media Play/Pause";
                case Keys.LaunchMail: return "Launch Mail";
                case Keys.SelectMedia: return "Select Media";
                case Keys.LaunchApplication1: return "Launch App 1";
                case Keys.LaunchApplication2: return "Launch App 2";
                case Keys.Play: return "Play";
                case Keys.Zoom: return "Zoom";
                case Keys.Shift: return "Shift";
                case Keys.Control: return "Control";
                case Keys.Alt: return "Alt";
            }

            string unicode = getCharsFromKeys(key, stateInitial, charBuffer);

            return unicode;
        }

        /// <summary>
        ///   Converts a <see cref="Keys">key</see> into a string
        ///   containing likely result of the user pressing those keys.
        /// </summary>
        /// 
        public string ToUnicodeCharString(Keys key, bool shift, bool altGraph)
        {
            stateCurrent[(int)Keys.ShiftKey] = shift ? (byte)0xff : (byte)0x00;
            stateCurrent[(int)Keys.ControlKey] = altGraph ? (byte)0xff : (byte)0x00;
            stateCurrent[(int)Keys.Menu] = altGraph ? (byte)0xff : (byte)0x00;

            return getCharsFromKeys(key, stateCurrent, charBuffer);
        }

        /// <summary>
        ///   Gives a formatted version of the key listing all active
        ///   modifiers, the key name and the likely value for the key.
        /// </summary>
        /// 
        public string ToStringWithModifiers(Keys key)
        {
            StringBuilder builder = new StringBuilder(100);

            bool shift = key.HasFlag(Keys.Shift);
            bool alt = key.HasFlag(Keys.Alt);
            bool ctrl = key.HasFlag(Keys.Control);
            bool win = key.HasFlag(KeysExtensions.Windows);

            key = key.RemoveModifiers();

            if (shift)
            {
                builder.Append("Shift");
            }

            if (ctrl)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Control");
            }

            if (alt)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Alt");
            }

            if (win)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");
                builder.Append("Win");
            }

            if (key != Keys.None)
            {
                if (builder.Length > 0)
                    builder.Append(" + ");

                string raw = ToKeyNameString(key);

                if (raw == null)
                    return builder.ToString();

                string mod = ToUnicodeCharString(key, shift, alt);

                builder.Append(raw);

                if (raw != mod && !String.IsNullOrWhiteSpace(mod))
                    builder.AppendFormat(" ({0})", mod);
            }

            return builder.ToString();
        }


        private static string getCharsFromKeys(Keys keys, byte[] state, StringBuilder buffer)
        {
            int ret = NativeMethods.ToUnicode((uint)keys, 0, state, buffer, 256, 0);

            string result;

            if (ret == -1)
            {
                StringBuilder sb = new StringBuilder(256);
                NativeMethods.ToUnicode((uint)keys, 0, state, sb, 256, 0);
                result = buffer.ToString();
            }
            else if (ret == 0)
            {
                return null;
            }
            else if (ret == 1)
            {
                result = buffer.ToString();
            }
            else if (ret >= 2)
            {
                buffer.Remove(0, ret - 1);
                result = buffer.ToString();
            }
            else
            {
                throw new InvalidOperationException("Native To Unicode function returned unexpected result.");
            }

            UnicodeCategory category = Char.GetUnicodeCategory(result, 0);

            if (category == UnicodeCategory.Control)
                return null;

            return result;
        }
    }
}
