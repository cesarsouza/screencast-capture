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
    ///   Native Win32 methods.
    /// </summary>
    /// 
    internal static partial class NativeMethods
    {
        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        /// <summary>
        ///   Deletes a logical pen, brush, font, bitmap, region, or palette,
        ///   freeing all system resources associated with the object. After 
        ///   the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// 
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        ///   Selects an object into the specified device context (DC). The new
        ///   object replaces the previous object of the same type.
        /// </summary>
        /// 
        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        ///   Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// 
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        ///   Deletes the specified device context (DC).
        /// </summary>
        /// 
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteDC(IntPtr hdc);

        /// <summary>
        ///   Performs a bit-block transfer of the color data corresponding to a rectangle
        ///   of pixels from the specified source device context into a destination device
        ///   context.
        /// </summary>
        /// 
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

    }

    /// <summary>
    ///   Contains information about an icon or a cursor.
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public struct ICONINFO
    {
        /// <summary>
        ///   Specifies whether this structure defines an icon or a cursor.
        ///   A value of TRUE specifies an icon; FALSE specifies a cursor. 
        /// </summary>
        /// 
        public bool fIcon;

        /// <summary>
        ///   Specifies the x-coordinate of a cursor's hot spot. If this structure defines
        ///   an icon, the hot spot is always in the center of the icon, and this member is
        ///   ignored.
        /// </summary>
        /// 
        public Int32 xHotspot;

        /// <summary>
        ///   Specifies the y-coordinate of a cursor's hot spot. If this structure defines
        ///   an icon, the hot spot is always in the center of the icon, and this member is
        ///   ignored.
        /// </summary>
        /// 
        public Int32 yHotspot;

        /// <summary>
        ///   (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
        ///   this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is 
        ///   the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If 
        ///   this structure defines a color icon, this mask only defines the AND bitmask of the icon. 
        /// </summary>
        /// 
        public IntPtr hbmMask;

        /// <summary>
        ///   (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        ///   structure defines a black and white icon. The AND bitmask of hbmMask is applied 
        ///   with the SRCAND flag to the destination; subsequently, the color bitmap is applied
        ///   (using XOR) to the destination by using the SRCINVERT flag. 
        /// </summary>
        /// 
        public IntPtr hbmColor;
    }

    /// <summary>
    ///   Contains global cursor information.
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public struct CURSORINFO
    {
        /// <summary>
        ///   The size of the structure, in bytes. The caller must set this to sizeof(CURSORINFO).
        /// </summary>
        /// 
        public Int32 cbSize;

        /// <summary>
        ///   The cursor state.
        /// </summary>
        /// 
        public CursorState flags;

        /// <summary>
        ///   (HCURSOR) A handle to the cursor.
        /// </summary>
        /// 
        public IntPtr hCursor;

        /// <summary>
        ///   A structure that receives the screen coordinates of the cursor.
        /// </summary>
        /// 
        public Point ptScreenPos;
    }

    /// <summary>
    ///   The RECT structure defines the coordinates of the
    ///   upper-left and lower-right corners of a rectangle.
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public struct RECT
    {
        /// <summary>
        ///   The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        /// 
        public int Left;

        /// <summary>
        ///   The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        /// 
        public int Top;

        /// <summary>
        ///   The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        /// 
        public int Right;

        /// <summary>
        ///   The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        /// 
        public int Bottom;

        /// <summary>
        ///   Initializes a new instance of the <see cref="RECT"/> struct.
        /// </summary>
        /// 
        /// <param name="Rectangle">The rectangle coordinates.</param>
        /// 
        public RECT(RECT Rectangle)
            : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom) { }

        /// <summary>
        ///   Initializes a new instance of the <see cref="RECT"/> struct.
        /// </summary>
        /// 
        /// <param name="left">The x-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="right">The x-coordinate of the lower-right corner of the rectangle.</param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
        /// 
        public RECT(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        /// <summary>
        ///   Gets or sets the rectangle height.
        /// </summary>
        /// 
        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        /// <summary>
        ///   Gets or sets the rectangle width.
        /// </summary>
        /// 
        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        /// <summary>
        ///   Gets or sets the rectangle location.
        /// </summary>
        /// 
        public Point Location
        {
            get { return new Point(Left, Top); }
            set
            {
                Right -= (Left - value.X);
                Bottom -= (Top - value.Y);
                Left = value.X;
                Top = value.Y;
            }
        }

        /// <summary>
        ///   Gets or sets the rectangle size.
        /// </summary>
        /// 
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                Right = value.Width + Left;
                Bottom = value.Height + Top;
            }
        }
    }


    /// <summary>
    ///   The cursor state.
    /// </summary>
    /// 
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum CursorState
    {
        /// <summary>
        ///   The cursor is hidden.
        /// </summary>
        /// 
        CURSOR_HIDING = 0x00000000,

        /// <summary>
        ///  The cursor is showing.
        /// </summary>
        /// 
        CURSOR_SHOWING = 0x00000001,

        /// <summary>
        ///  (Windows 8) The cursor is suppressed. This flag indicates that the 
        ///  system is not drawing the cursor because the user is providing input
        ///  through touch or pen instead of the mouse.
        /// </summary>
        /// 
        CURSOR_SUPPRESSED = 0x00000002
    }


    /// <summary>
    ///     Specifies a raster-operation code. These codes define how the color data for the
    ///     source rectangle is to be combined with the color data for the destination
    ///     rectangle to achieve the final color.
    /// </summary>
    /// 
    [GeneratedCode("PInvoke", "1.0.0.0")]
    public enum TernaryRasterOperations : uint
    {
        /// <summary>dest = source</summary>
        SRCCOPY = 0x00CC0020,
        /// <summary>dest = source OR dest</summary>
        SRCPAINT = 0x00EE0086,
        /// <summary>dest = source AND dest</summary>
        SRCAND = 0x008800C6,
        /// <summary>dest = source XOR dest</summary>
        SRCINVERT = 0x00660046,
        /// <summary>dest = source AND (NOT dest)</summary>
        SRCERASE = 0x00440328,
        /// <summary>dest = (NOT source)</summary>
        NOTSRCCOPY = 0x00330008,
        /// <summary>dest = (NOT src) AND (NOT dest)</summary>
        NOTSRCERASE = 0x001100A6,
        /// <summary>dest = (source AND pattern)</summary>
        MERGECOPY = 0x00C000CA,
        /// <summary>dest = (NOT source) OR dest</summary>
        MERGEPAINT = 0x00BB0226,
        /// <summary>dest = pattern</summary>
        PATCOPY = 0x00F00021,
        /// <summary>dest = DPSnoo</summary>
        PATPAINT = 0x00FB0A09,
        /// <summary>dest = pattern XOR dest</summary>
        PATINVERT = 0x005A0049,
        /// <summary>dest = (NOT dest)</summary>
        DSTINVERT = 0x00550009,
        /// <summary>dest = BLACK</summary>
        BLACKNESS = 0x00000042,
        /// <summary>dest = WHITE</summary>
        WHITENESS = 0x00FF0062,
        /// <summary>
        /// Capture window as seen on screen.  This includes layered windows 
        /// such as WPF windows with AllowsTransparency="true"
        /// </summary>
        CAPTUREBLT = 0x40000000
    }
}
