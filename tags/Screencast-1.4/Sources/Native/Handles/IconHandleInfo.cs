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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    /// <summary>
    ///   Managed version of the ICONINFO Win32 structure.
    /// </summary>
    /// 
    public class IconHandleInfo : IDisposable
    {
        /// <summary>
        ///   Specifies whether this structure defines an icon or a cursor.
        ///   A value of TRUE specifies an icon; FALSE specifies a cursor. 
        /// </summary>
        /// 
        public bool IsIcon { get; private set; }

        /// <summary>
        ///   Specifies the cursor's hot spot. If this structure defines an icon, the
        ///   hot spot is always in the center of the icon, and this member is ignored.
        /// </summary>
        /// 
        public Point Hotspot { get; private set; }

        /// <summary>
        ///   Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
        ///   this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is 
        ///   the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If 
        ///   this structure defines a color icon, this mask only defines the AND bitmask of the icon. 
        /// </summary>
        /// 
        public BitmapHandle MaskBitmap { get; private set; }

        /// <summary>
        ///   Handle to the icon color bitmap. This member can be optional if this 
        ///   structure defines a black and white icon. The AND bitmask of hbmMask is applied 
        ///   with the SRCAND flag to the destination; subsequently, the color bitmap is applied
        ///   (using XOR) to the destination by using the SRCINVERT flag. 
        /// </summary>
        /// 
        public BitmapHandle ColorBitmap { get; private set; }


        internal IconHandleInfo(NativeMethods.ICONINFO info)
        {
            this.IsIcon = info.fIcon;
            this.Hotspot = new Point(info.xHotspot, info.yHotspot);
            this.MaskBitmap = new BitmapHandle(info.hbmMask);
            this.ColorBitmap = new BitmapHandle(info.hbmColor);
        }


        #region IDisposable implementation

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, 
        ///   releasing, or resetting unmanaged resources.
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources and performs other cleanup operations 
        ///   before the <see cref="IconHandleInfo"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~IconHandleInfo()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// 
        /// <param name="disposing"><c>true</c> to release both managed
        /// and unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        ///
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                MaskBitmap.Dispose();
                ColorBitmap.Dispose();
            }
        }
        #endregion

    }

}
