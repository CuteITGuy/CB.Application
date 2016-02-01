using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Pixels
{
    public static class Bitmap
    {
        #region Import
        /// <summary>
        ///     Performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the
        ///     specified source device context into a destination device context.
        /// </summary>
        /// <param name="hdc">Handle to the destination device context.</param>
        /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        /// <param name="hdcSrc">Handle to the source device context.</param>
        /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        /// <param name="dwRop">A raster-operation code.</param>
        /// <returns>
        ///     <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information,
        ///     call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error" />.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
            [In] IntPtr hdcSrc, int nXSrc, int nYSrc, RasterOperations dwRop);

        /// <summary>
        ///     Creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to a device context.</param>
        /// <param name="nWidth">The bitmap width, in pixels.</param>
        /// <param name="nHeight">The bitmap height, in pixels.</param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If
        ///     the function fails, the return value is <see cref="System.IntPtr.Zero" />.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

        /// <summary>
        ///     The GetPixel function retrieves the red, green, blue (RGB) color value of the pixel at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="nXPos">The x-coordinate, in logical units, of the pixel to be examined.</param>
        /// <param name="nYPos">The y-coordinate, in logical units, of the pixel to be examined.</param>
        /// <returns>
        ///     The return value is the COLORREF value that specifies the RGB of the pixel. If the pixel is
        ///     outside of the current clipping region, the return value is CLR_INVALID (0xFFFFFFFF defined in Wingdi.h).
        /// </returns>
        /// <remarks>
        ///     The pixel must be within the boundaries of the current clipping region.
        ///     Not all devices support GetPixel. An application should call GetDeviceCaps to determine whether a
        ///     specified device supports this function.
        ///     A bitmap must be selected within the device context, otherwise, CLR_INVALID is returned on all pixels.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd144909%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("gdi32.dll")]
        public static extern Pixel GetPixel(IntPtr hdc, int nXPos, int nYPos);
        #endregion
    }
}