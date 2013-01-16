using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScreenCapture.Native.Handles
{
    public class IconInfo : IDisposable
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
        public Point HotSpot { get; private set; }

        /// <summary>
        ///   Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
        ///   this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is 
        ///   the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If 
        ///   this structure defines a color icon, this mask only defines the AND bitmask of the icon. 
        /// </summary>
        /// 
        public IntPtr MaskBitmap;

        /// <summary>
        ///   Handle to the icon color bitmap. This member can be optional if this 
        ///   structure defines a black and white icon. The AND bitmask of hbmMask is applied 
        ///   with the SRCAND flag to the destination; subsequently, the color bitmap is applied
        ///   (using XOR) to the destination by using the SRCINVERT flag. 
        /// </summary>
        /// 
        public IntPtr ColorBitmap;


        internal IconInfo(ICONINFO info)
        {
            this.IsIcon = info.fIcon;
            this.HotSpot = new Point(info.xHotspot, info.yHotspot);
            this.MaskBitmap = info.hbmMask;
            this.ColorBitmap = info.hbmColor;
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
        ///   before the <see cref="CursorIconInfo("/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~IconInfo()
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
            if (MaskBitmap != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(MaskBitmap);
                MaskBitmap = IntPtr.Zero;
            }

            if (ColorBitmap != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(ColorBitmap);
                ColorBitmap = IntPtr.Zero;
            }

            if (disposing)
            {
                // free managed resources
            }
        }
        #endregion

    }

}
