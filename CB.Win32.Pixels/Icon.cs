using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Pixels
{
    public static class Icon
    {
        #region Import
        /// <summary>
        ///     Copies the specified icon from another module to the current module.
        /// </summary>
        /// <param name="hIcon">A handle to the icon to be copied.</param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the duplicate icon.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The CopyIcon function enables an application or DLL to get its own handle to an icon owned by
        ///     another module. If the other module is freed, the application icon will still be able to use the icon.
        ///     Before closing, an application must call the DestroyIcon function to free any system resources associated
        ///     with the icon.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms648058%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);
        #endregion
    }
}