// Screencast Capture Lite 
// http://www.crsouza.com
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

namespace ScreenCapture
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class CursorCapture : IDisposable
    {
        private Point position;

        private Graphics desktopGraphics;
        private IntPtr desktopHdc;
        private IntPtr maskHdc;
        private int cursorInfoSize;

        public CursorCapture()
        {
            desktopGraphics = Graphics.FromHwnd(NativeMethods.GetDesktopWindow());
            desktopHdc = desktopGraphics.GetHdc();
            maskHdc = NativeMethods.CreateCompatibleDC(desktopHdc);
            NativeMethods.CURSORINFO cursorInfo = new NativeMethods.CURSORINFO();
            cursorInfoSize = Marshal.SizeOf(cursorInfo);
        }

        public Point Position { get { return position; } }

        public Bitmap GetBitmap()
        {
            // Based on answer from Tarsier in SO question "C# - Capturing the Mouse cursor image"
            // http://stackoverflow.com/questions/918990/c-sharp-capturing-the-mouse-cursor-image

            NativeMethods.CURSORINFO cursorInfo;
            cursorInfo.cbSize = cursorInfoSize;

            if (!NativeMethods.GetCursorInfo(out cursorInfo))
                return null;

            if (cursorInfo.flags != NativeMethods.CURSOR_SHOWING)
                return null;

            IntPtr hicon = NativeMethods.CopyIcon(cursorInfo.hCursor);

            if (hicon == IntPtr.Zero)
                return null;

            NativeMethods.ICONINFO iconInfo;
            if (!NativeMethods.GetIconInfo(hicon, out iconInfo))
                return null;

            position.X = cursorInfo.ptScreenPos.x - ((int)iconInfo.xHotspot);
            position.Y = cursorInfo.ptScreenPos.y - ((int)iconInfo.yHotspot);

            // Note: an alternative way would be to just return 
            //
            //   Icon icon = Icon.FromHandle(hicon);
            //
            // However, this seems to fail for monochrome cursors such as
            // the I-Beam cursor (text cursor). The following takes care
            // of returning the correct bitmap.

            Bitmap resultBitmap;

            using (Bitmap maskBitmap = Bitmap.FromHbitmap(iconInfo.hbmMask))
            {
                // Is this a monochrome cursor?
                if (maskBitmap.Height == maskBitmap.Width * 2)
                {
                    resultBitmap = new Bitmap(maskBitmap.Width, maskBitmap.Width);

                    IntPtr maskPtr = NativeMethods.SelectObject(maskHdc, maskBitmap.GetHbitmap());

                    using (Graphics resultGraphics = Graphics.FromImage(resultBitmap))
                    {
                        IntPtr resultHdc = resultGraphics.GetHdc();

                        // These two operation will result in a black cursor over a white background. Later
                        //   in the code, a call to MakeTransparent() will get rid of the white background.
                        NativeMethods.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 32, NativeMethods.TernaryRasterOperations.SRCCOPY);
                        NativeMethods.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 0, NativeMethods.TernaryRasterOperations.SRCINVERT);

                        resultGraphics.ReleaseHdc(resultHdc);
                    }

                    NativeMethods.DeleteObject(maskPtr);

                    // Remove the white background from the BitBlt calls,
                    // resulting in a black cursor over a transparent background.
                    resultBitmap.MakeTransparent(Color.White);
                }
                else
                {
                    using (Icon icon = Icon.FromHandle(hicon))
                        resultBitmap = icon.ToBitmap();
                }
            }

            // Clean allocated resources
            NativeMethods.DeleteObject(iconInfo.hbmColor);
            NativeMethods.DeleteObject(iconInfo.hbmMask);
            NativeMethods.DestroyIcon(hicon);

            return resultBitmap;
        }


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
        ///   before the <see cref="Signal"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~CursorCapture()
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
            // free native resources
            desktopGraphics.ReleaseHdc(desktopHdc);
            NativeMethods.DeleteDC(maskHdc);

            if (disposing)
            {
                // free managed resources
                desktopGraphics.Dispose();
                desktopGraphics = null;
            }
        }
    }
}
