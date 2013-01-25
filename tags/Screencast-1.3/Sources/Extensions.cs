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

namespace ScreenCapture.Views
{
    using System.Windows.Forms;
    using System;

    /// <summary>
    ///   Misc extension methods.
    /// </summary>
    /// 
    public static class Extensions
    {
        /// <summary>
        ///   Forces the creation of a control or form
        ///   so data-binding may work on all properties.
        /// </summary>
        /// 
        public static void ForceCreateControl(this Control control)
        {
            if (control == null) 
                throw new ArgumentNullException("control");

            var method = control.GetType().GetMethod("CreateControl",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic);
            var parameters = method.GetParameters();

            method.Invoke(control, new object[] { true });
        }
    }
}
