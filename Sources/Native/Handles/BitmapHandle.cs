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
    using System.Runtime.InteropServices;
    using System.Drawing;

    /// <summary>
    ///   Managed wrapper around a Bitmap handle.
    /// </summary>
    /// 
    public class BitmapHandle : SafeHandle
    {
        /// <summary>
        ///   Gets the handle for the GDI+ device context.
        /// </summary>
        /// 
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HookHandle" /> class.
        /// </summary>
        /// 
        /// <param name="handle">The handle.</param>
        /// 
        public BitmapHandle(IntPtr handle)
            : base(IntPtr.Zero, true)
        {
            Handle = handle;
        }

        /// <summary>
        ///   When overridden in a derived class, gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// 
        /// <returns>true if the handle value is invalid; otherwise, false.</returns>
        /// 
        ///   <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        ///   </PermissionSet>
        ///   
        public override bool IsInvalid
        {
            get { return Handle == IntPtr.Zero; }
        }

        /// <summary>
        ///   When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// 
        /// <returns>
        ///   true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        /// 
        protected override bool ReleaseHandle()
        {
            return NativeMethods.DeleteObject(Handle);
        }
    }
}
