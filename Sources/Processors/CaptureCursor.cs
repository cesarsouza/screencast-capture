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

namespace ScreenCapture.Processors
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using ScreenCapture.Native;

    /// <summary>
    ///   Class to capture the cursor's bitmap.
    /// </summary>
    /// 
    [System.Security.SecurityCritical]
    public class CaptureCursor : IDisposable
    {
        private Point position;

        private DeviceHandle mask;
        private int cursorInfoSize;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CaptureCursor"/> class.
        /// </summary>
        /// 
        public CaptureCursor()
        {
            // By preallocating those we will prevent extra 
            // object allocations in middle of processing.

            IntPtr desk = NativeMethods.GetDesktopWindow();
            using (Graphics desktop = Graphics.FromHwnd(desk))
                mask = SafeNativeMethods.CreateCompatibleDC(desktop);

            cursorInfoSize = Marshal.SizeOf(typeof(CursorInfo));
        }

        /// <summary>
        ///   Gets the current cursor position, adjusted 
        ///   considering the current cursor's type.
        /// </summary>
        /// 
        public Point Position { get { return position; } }

        /// <summary>
        ///   Gets or sets the current capture region
        ///   (region of interest) of the processor.
        /// </summary>
        /// 
        public Rectangle CaptureRegion { get; set; }

        /// <summary>
        ///   Gets the current cursor bitmap, supporting
        ///   transparency and handling monochrome cursors.
        /// </summary>
        /// 
        /// <returns>A <see cref="Bitmap"/> containing the cursor's bitmap image.</returns>
        /// 
        public Bitmap GetBitmap()
        {
            // Based on answer from Tarsier in SO question "C# - Capturing the Mouse cursor image"
            // http://stackoverflow.com/questions/918990/c-sharp-capturing-the-mouse-cursor-image

            CursorInfo cursorInfo;
            cursorInfo.cbSize = cursorInfoSize;

            if (!NativeMethods.GetCursorInfo(out cursorInfo))
                return null;

            if (cursorInfo.flags != CursorState.CURSOR_SHOWING)
                return null;

            using (IconHandle hicon = SafeNativeMethods.GetCursorIcon(cursorInfo))
            using (IconHandleInfo iconInfo = SafeNativeMethods.GetIconInfo(hicon))
            {
                if (iconInfo == null)
                    return null;

                position.X = cursorInfo.ptScreenPos.X - iconInfo.Hotspot.X;
                position.Y = cursorInfo.ptScreenPos.Y - iconInfo.Hotspot.Y;

                if (!CaptureRegion.Contains(position))
                    return null;

                position.X -= CaptureRegion.X;
                position.Y -= CaptureRegion.Y;

                // Note: an alternative way would be to just return 
                //
                //   Icon icon = Icon.FromHandle(hicon);
                //
                // However, this seems to fail for monochrome cursors such as
                // the I-Beam cursor (text cursor). The following takes care
                // of returning the correct bitmap.

                Bitmap resultBitmap = null;

                using (Bitmap bitmapMask = Bitmap.FromHbitmap(iconInfo.MaskBitmap.Handle))
                {
                    // Here we have to determine if the current cursor is monochrome in order
                    // to do a proper processing. If we just extracted the cursor icon from
                    // the icon handle, monochrome cursors would appear garbled.

                    if (bitmapMask.Height == bitmapMask.Width * 2)
                    {
                        // Yes, this is a monochrome cursor. We will have to manually copy
                        // the bitmap and the bitmak layers of the cursor into the bitmap.

                        resultBitmap = new Bitmap(bitmapMask.Width, bitmapMask.Width);
                        NativeMethods.SelectObject(mask.Handle, iconInfo.MaskBitmap.Handle);

                        using (Graphics resultGraphics = Graphics.FromImage(resultBitmap))
                        {
                            IntPtr resultHandle = resultGraphics.GetHdc();

                            // These two operation will result in a black cursor over a white background. Later
                            //   in the code, a call to MakeTransparent() will get rid of the white background.
                            NativeMethods.BitBlt(resultHandle, 0, 0, 32, 32, mask.Handle, 0, 32, CopyPixelOperation.SourceCopy);
                            NativeMethods.BitBlt(resultHandle, 0, 0, 32, 32, mask.Handle, 0, 0, CopyPixelOperation.SourceInvert);

                            resultGraphics.ReleaseHdc(resultHandle);
                        }

                        // Remove the white background from the BitBlt calls,
                        // resulting in a black cursor over a transparent background.
                        resultBitmap.MakeTransparent(Color.White);
                    }
                    else
                    {
                        // This isn't a monochrome cursor.
                        using (Icon icon = Icon.FromHandle(hicon.Handle))
                            resultBitmap = icon.ToBitmap();
                    }
                }

                return resultBitmap;
            }
        }


        /// <summary>
        ///   Draws the cursor icon into a graphics object.
        /// </summary>
        /// 
        public void Draw(Graphics graphics, float scaleWidth, float scaleHeight)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            Bitmap cursor = GetBitmap();

            if (cursor != null)
            {
                //graphics.DrawImage(cursor, Position);
                graphics.DrawImage(cursor, 
                    Position.X * scaleWidth, Position.Y * scaleHeight,
                    cursor.Width * scaleWidth, cursor.Height * scaleHeight);
            }
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
        ///   before the <see cref="CaptureCursor"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~CaptureCursor()
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
                mask.Dispose();
                mask = null;
            }
        }
        #endregion

    }
}
