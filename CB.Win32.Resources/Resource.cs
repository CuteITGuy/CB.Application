using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Resources
{
    public static class Resource
    {
        #region Import
        /// <summary>
        ///     Creates a new image (icon, cursor, or bitmap) and copies the attributes of the specified image to the new
        ///     one. If necessary, the function stretches the bits to fit the desired size of the new image.
        /// </summary>
        /// <param name="hImage">A handle to the image to be copied.</param>
        /// <param name="uType">The type of image to be copied.</param>
        /// <param name="cxDesired">
        ///     The desired width, in pixels, of the image. If this is zero, then the returned image
        ///     will have the same width as the original hImage.
        /// </param>
        /// <param name="cyDesired">
        ///     The desired height, in pixels, of the image. If this is zero, then the returned image
        ///     will have the same height as the original hImage.
        /// </param>
        /// <param name="fuFlags">Specifies how the image is copies.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the newly created image.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     When you are finished using the resource, you can release its associated memory by calling one of
        ///     the functions in the following table.
        ///     Resource	Release function
        ///     Bitmap	    DeleteObject
        ///     Cursor	    DestroyCursor
        ///     Icon	    DestroyIcon
        ///     The system automatically deletes the resource when its process terminates, however, calling the appropriate
        ///     function saves memory and decreases the size of the process's working set.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms648031%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CopyImage(IntPtr hImage, ImageTypes uType, int cxDesired, int cyDesired,
            ImageCopyTypes fuFlags);
        #endregion
    }
}