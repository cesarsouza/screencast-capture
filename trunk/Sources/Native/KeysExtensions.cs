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
    using System.Windows.Forms;

    /// <summary>
    ///   Extension methods for <see cref="Keys"/>.
    /// </summary>
    /// 
    public static class KeysExtensions
    {

        /// <summary>
        ///   Windows logo modifier key.
        /// </summary>
        public const Keys Windows = (Keys)524288;

        /// <summary>
        ///   Removes all modifiers from a key.
        /// </summary>
        /// 
        public static Keys RemoveModifiers(this Keys key)
        {
            // Zero all modifier flags
            return key & ~Keys.Modifiers;
        }

        /// <summary>
        ///   Sets the modifiers of <paramref name="key"/> to be the same given in <paramref name="modifiers"/>.
        /// </summary>
        /// 
        public static Keys SetModifiers(this Keys key, Keys modifiers)
        {
            // Zero all old modifier bit flags
            key &= ~Keys.Modifiers;

            // Zero key and extract previous modifiers
            modifiers &= Keys.Modifiers;

            // Combine new key with old modifiers
            return key | modifiers;
        }

        /// <summary>
        ///   Set the modifiers of <paramref name="key"/>.
        /// </summary>
        /// 
        public static Keys SetModifiers(this Keys key, bool shift, bool alt, bool ctrl, bool win)
        {
            // Zero modifier part
            key &= Keys.Modifiers;

            // Set modifiers
            if (shift) key |= Keys.Shift;
            if (alt) key |= Keys.Alt;
            if (ctrl) key |= Keys.Control;
            if (win) key |= Windows;

            return key;
        }

    }
}
