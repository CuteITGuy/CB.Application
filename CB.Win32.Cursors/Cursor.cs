using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using CB.Win32.Common;
using CB.Win32.Resources;


namespace CB.Win32.Cursors
{
    public static class Cursor
    {
        #region Import
        /// <summary>
        ///     Confines the cursor to a rectangular area on the screen. If a subsequent cursor position (set by the
        ///     SetCursorPos function or the mouse) lies outside the rectangle, the system automatically adjusts the
        ///     position to keep the cursor inside the rectangular area.
        /// </summary>
        /// <param name="lpRect">
        ///     A pointer to the structure that contains the screen coordinates of the upper-left
        ///     and lower-right corners of the confining rectangle. If this parameter is NULL, the cursor is free to move
        ///     anywhere on the screen.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The cursor is a shared resource. If an application confines the cursor, it must release the cursor by
        ///     using ClipCursor before relinquishing control to another application.
        ///     The calling process must have WINSTA_WRITEATTRIBUTES access to the window station.
        /// </remarks>
        [DllImport("user32.dll")]
        private static extern bool ClipCursor(ref Rect lpRect);

        /// <summary>
        ///     Creates a cursor having the specified size, bit patterns, and hot spot.
        /// </summary>
        /// <param name="hInst">A handle to the current instance of the application creating the cursor.</param>
        /// <param name="xHotSpot">The horizontal position of the cursor's hot spot.</param>
        /// <param name="yHotSpot">The vertical position of the cursor's hot spot.</param>
        /// <param name="nWidth">The width of the cursor, in pixels.</param>
        /// <param name="nHeight">The height of the cursor, in pixels.</param>
        /// <param name="pvANDPlane">
        ///     An array of bytes that contains the bit values for the AND mask of the cursor,
        ///     as in a device-dependent monochrome bitmap.
        /// </param>
        /// <param name="pvXORPlane">
        ///     An array of bytes that contains the bit values for the XOR mask of the cursor,
        ///     as in a device-dependent monochrome bitmap.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the cursor.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The nWidth and nHeight parameters must specify a width and height that are supported by the current display
        ///     driver, because the system cannot create cursors of other sizes. To determine the width and height supported
        ///     by the display driver, use the GetSystemMetrics function, specifying the SM_CXCURSOR or SM_CYCURSOR value.
        ///     Before closing, an application must call the DestroyCursor function to free any system resources associated
        ///     with the cursor.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr CreateCursor(IntPtr hInst, int xHotSpot, int yHotSpot,
            int nWidth, int nHeight, byte[] pvANDPlane, byte[] pvXORPlane);

        /// <summary>
        ///     Destroys a cursor and frees any memory the cursor occupied. Do not use this function to destroy a shared cursor.
        /// </summary>
        /// <param name="hCursor">A handle to the cursor to be destroyed. The cursor must not be in use.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The DestroyCursor function destroys a nonshared cursor. Do not use this function to destroy a shared cursor.
        ///     A shared cursor is valid as long as the module from which it was loaded remains in memory. The following
        ///     functions obtain a shared cursor:
        ///     LoadCursor
        ///     LoadCursorFromFile
        ///     LoadImage (if you use the LR_SHARED flag)
        ///     CopyImage (if you use the LR_COPYRETURNORG flag and the hImage parameter is a shared cursor)
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern bool DestroyCursor(IntPtr hCursor);

        /// <summary>
        ///     Retrieves the screen coordinates of the rectangular area to which the cursor is confined.
        /// </summary>
        /// <param name="lpRect">
        ///     A pointer to a Rect structure that receives the screen coordinates of the confining
        ///     rectangle. The structure receives the dimensions of the screen if the cursor is not confined to a rectangle.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The cursor is a shared resource. If an application confines the cursor with the ClipCursor function,
        ///     it must later release the cursor by using ClipCursor before relinquishing control to another application.
        ///     The calling process must have WINSTA_READATTRIBUTES access to the window station.
        /// </remarks>
        [DllImport("user32.dll")]
        static extern bool GetClipCursor(out Rect lpRect);

        /// <summary>
        ///     Retrieves a handle to the current cursor.
        ///     <para>
        ///         To get information on the global cursor, even if it is not
        ///         owned by the current thread, use GetCursorInfo.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     The return value is the handle to the current cursor. If there is no cursor, the return value is
        ///     NULL.
        /// </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms648388%28v=vs.85%29.aspx</remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr GetCursor();

        /// <summary>
        ///     Retrieves the position of the mouse cursor, in screen coordinates.
        /// </summary>
        /// <param name="lpPoint">A pointer to a Point structure that receives the screen coordinates of the cursor.</param>
        /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
        /// <remarks>
        ///     The cursor position is always specified in screen coordinates and is not affected by the mapping mode of the
        ///     window that contains the cursor.
        ///     The calling process must have WINSTA_READATTRIBUTES access to the window station.
        ///     The input desktop must be the current desktop when you call GetCursorPos. Call OpenInputDesktop to determine
        ///     whether the current desktop is the input desktop. If it is not, call SetThreadDesktop with the HDESK returned
        ///     by OpenInputDesktop to switch to that desktop.
        /// </remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out Point lpPoint);

        /// <summary>
        ///     Loads the specified cursor resource from the executable (.EXE) file associated with an application instance.
        ///     <para>Note  This function has been superseded by the LoadImage function.</para>
        /// </summary>
        /// <param name="hInstance">
        ///     A handle to an instance of the module whose executable file contains the cursor
        ///     to be loaded.
        /// </param>
        /// <param name="lpCursorName">
        ///     The name of the cursor resource to be loaded. Alternatively, this parameter can
        ///     consist of the resource identifier in the low-order word and zero in the high-order word. The MAKEINTRESOURCE
        ///     macro can also be used to create this value. To use one of the predefined cursors, the application must set
        ///     the hInstance parameter to NULL and the lpCursorName parameter to one the following values.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the newly loaded cursor.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The LoadCursor function loads the cursor resource only if it has not been loaded; otherwise, it retrieves
        ///     the handle to the existing resource. This function returns a valid cursor handle only if the lpCursorName
        ///     parameter is a pointer to a cursor resource. If lpCursorName is a pointer to any type of resource other than
        ///     a cursor (such as an icon), the return value is not NULL, even though it is not a valid cursor handle.
        ///     The LoadCursor function searches the cursor resource most appropriate for the cursor for the current display
        ///     device. The cursor resource can be a color or monochrome bitmap.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, CursorNames lpCursorName);

        /// <summary>
        ///     Loads the specified cursor resource from the executable (.EXE) file associated with an application instance.
        ///     <para>Note  This function has been superseded by the LoadImage function.</para>
        /// </summary>
        /// <param name="hInstance">
        ///     A handle to an instance of the module whose executable file contains the cursor
        ///     to be loaded.
        /// </param>
        /// <param name="lpCursorName">
        ///     The name of the cursor resource to be loaded. Alternatively, this parameter can
        ///     consist of the resource identifier in the low-order word and zero in the high-order word. The MAKEINTRESOURCE
        ///     macro can also be used to create this value. To use one of the predefined cursors, the application must set
        ///     the hInstance parameter to NULL and the lpCursorName parameter to one the following values.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the newly loaded cursor.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The LoadCursor function loads the cursor resource only if it has not been loaded; otherwise, it retrieves
        ///     the handle to the existing resource. This function returns a valid cursor handle only if the lpCursorName
        ///     parameter is a pointer to a cursor resource. If lpCursorName is a pointer to any type of resource other than
        ///     a cursor (such as an icon), the return value is not NULL, even though it is not a valid cursor handle.
        ///     The LoadCursor function searches the cursor resource most appropriate for the cursor for the current display
        ///     device. The cursor resource can be a color or monochrome bitmap.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);

        /// <summary>
        ///     Creates a cursor based on data contained in a file.
        /// </summary>
        /// <param name="lpFileName">
        ///     The source of the file data to be used to create the cursor. The data in the file
        ///     must be in either .CUR or .ANI format.
        ///     <para>
        ///         If the high-order word of lpFileName is nonzero, it is a pointer to a string that is a fully qualified
        ///         name of a file containing cursor data.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function is successful, the return value is a handle to the new cursor.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        ///     GetLastError may return the following value:
        ///     ERROR_FILE_NOT_FOUND. The specified file cannot be found.
        /// </returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        ///     Sets the cursor shape.
        /// </summary>
        /// <param name="hCursor">
        ///     A handle to the cursor. The cursor must have been created by the CreateCursor
        ///     function or loaded by the LoadCursor or LoadImage function. If this parameter is NULL, the cursor is
        ///     removed from the screen.
        /// </param>
        /// <returns>
        ///     The return value is the handle to the previous cursor, if there was one.
        ///     If there was no previous cursor, the return value is NULL.
        /// </returns>
        /// <remarks>
        ///     The cursor is set only if the new cursor is different from the previous cursor; otherwise, the function
        ///     returns immediately.
        ///     The cursor is a shared resource. A window should set the cursor shape only when the cursor is in its
        ///     client area or when the window is capturing mouse input. In systems without a mouse, the window should
        ///     restore the previous cursor before the cursor leaves the client area or before it relinquishes control
        ///     to another window.
        ///     If your application must set the cursor while it is in a window, make sure the class cursor for the
        ///     specified window's class is set to NULL. If the class cursor is not NULL, the system restores the class
        ///     cursor each time the mouse is moved.
        ///     The cursor is not shown on the screen if the internal cursor display count is less than zero. This occurs
        ///     if the application uses the ShowCursor function to hide the cursor more times than to show the cursor.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        /// <summary>
        ///     Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen
        ///     rectangle set by the most recent ClipCursor function call, the system automatically adjusts the coordinates
        ///     so that the cursor stays within the rectangle.
        /// </summary>
        /// <param name="x">The new x-coordinate of the cursor, in screen coordinates.</param>
        /// <param name="y">The new y-coordinate of the cursor, in screen coordinates.</param>
        /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
        /// <remarks>
        ///     The cursor is a shared resource. A window should move the cursor only when the cursor is in the window's client
        ///     area.
        ///     The calling process must have WINSTA_WRITEATTRIBUTES access to the window station.
        ///     The input desktop must be the current desktop when you call SetCursorPos. Call OpenInputDesktop to determine
        ///     whether the current desktop is the input desktop. If it is not, call SetThreadDesktop with the HDESK returned
        ///     by OpenInputDesktop to switch to that desktop.
        /// </remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        /// <summary>
        ///     Enables an application to customize the system cursors. It replaces the contents of the system cursor
        ///     specified by the id parameter with the contents of the cursor specified by the hcur parameter and then
        ///     destroys hcur.
        /// </summary>
        /// <param name="hcur">
        ///     A handle to the cursor. The function replaces the contents of the system cursor
        ///     specified by id with the contents of the cursor handled by hcur.
        ///     <para>
        ///         The system destroys hcur by calling the DestroyCursor function. Therefore, hcur cannot be a cursor
        ///         loaded using the LoadCursor function. To specify a cursor loaded from a resource, copy the cursor using
        ///         the CopyCursor function, then pass the copy to SetSystemCursor.
        ///     </para>
        /// </param>
        /// <param name="id">The system cursor to replace with the contents of hcur.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetSystemCursor(IntPtr hcur, CursorNames id);

        /// <summary>
        ///     Displays or hides the cursor.
        /// </summary>
        /// <param name="bShow">
        ///     If bShow is TRUE, the display count is incremented by one. If bShow is FALSE, the
        ///     display count is decremented by one.
        /// </param>
        /// <returns>The return value specifies the new display counter.</returns>
        /// <remarks>
        ///     Windows 8: Call GetCursorInfo to determine the cursor visibility.
        ///     This function sets an internal display counter that determines whether the cursor should be displayed.
        ///     The cursor is displayed only if the display count is greater than or equal to 0. If a mouse is installed,
        ///     the initial display count is 0. If no mouse is installed, the display count is –1.
        ///     Similar to System.Windows.Forms.Cursor.Hide() and System.Windows.Forms.Cursor.Show()
        /// </remarks>
        [DllImport("user32.dll")]
        internal static extern int ShowCursor(bool bShow);
        #endregion


        #region Methods
        /// <summary>
        ///     Confines the cursor to a rectangular area on the screen. If a subsequent cursor position (set by the
        ///     SetCursorPos function or the mouse) lies outside the rectangle, the system automatically adjusts the
        ///     position to keep the cursor inside the rectangular area.
        /// </summary>
        /// <param name="clip">
        ///     A Rect structure that contains the screen coordinates of the upper-left and
        ///     lower-right corners of the confining rectangle. If this parameter is NULL, the cursor is free to move
        ///     anywhere on the screen.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.
        /// </returns>
        /// <remarks>
        ///     The cursor is a shared resource. If an application confines the cursor, it must release the cursor by
        ///     using ClipCursor before relinquishing control to another application.
        ///     The calling process must have WINSTA_WRITEATTRIBUTES access to the window station.
        /// </remarks>
        public static bool ClipCursor(Rect clip)
        {
            return ClipCursor(ref clip);
        }

        /// <summary>
        ///     Confines the cursor to a rectangular area on the screen. If a subsequent cursor position (set by the
        ///     SetCursorPos function or the mouse) lies outside the rectangle, the system automatically adjusts the
        ///     position to keep the cursor inside the rectangular area.
        /// </summary>
        /// <param name="clip">
        ///     A Rectangle structure that contains the screen coordinates of the upper-left and
        ///     lower-right corners of the confining rectangle. If this parameter is NULL, the cursor is free to move
        ///     anywhere on the screen.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.
        /// </returns>
        /// <remarks>
        ///     The cursor is a shared resource. If an application confines the cursor, it must release the cursor by
        ///     using ClipCursor before relinquishing control to another application.
        ///     The calling process must have WINSTA_WRITEATTRIBUTES access to the window station.
        /// </remarks>
        public static bool ClipCursor(Rectangle clip)
        {
            return ClipCursor((Rect)clip);
        }

        /// <summary>
        ///     Copies the specified cursor.
        /// </summary>
        /// <param name="pCur">A handle to the cursor to be copied.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the duplicate cursor.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     CopyCursor enables an application or DLL to obtain the handle to a cursor shape owned by another
        ///     module. Then if the other module is freed, the application is still able to use the cursor shape.
        ///     Before closing, an application must call the DestroyCursor function to free any system resources associated
        ///     with the cursor.
        /// </remarks>
        public static IntPtr CopyCursor(IntPtr pCur)
        {
            return Resource.CopyImage(pCur, ImageTypes.Cursor, 0, 0, ImageCopyTypes.LoadFromFile);
        }

        /// <summary>
        ///     Retrieves the screen coordinates of the rectangular area to which the cursor is confined.
        /// </summary>
        /// <returns>
        ///     A Rect structure that receives the screen coordinates of the confining rectangle.
        ///     The structure receives the dimensions of the screen if the cursor is not confined to a rectangle.
        /// </returns>
        /// <remarks>
        ///     The cursor is a shared resource. If an application confines the cursor with the ClipCursor function,
        ///     it must later release the cursor by using ClipCursor before relinquishing control to another application.
        ///     The calling process must have WINSTA_READATTRIBUTES access to the window station.
        /// </remarks>
        public static Rect GetClipCursor()
        {
            Rect clip;
            if (GetClipCursor(out clip))
                return clip;
            throw new Exception();
        }

        /// <summary>
        ///     Retrieves the position of the mouse cursor, in screen coordinates.
        /// </summary>
        /// <returns>A Point structure that contains the screen coordinates of the cursor.</returns>
        public static Point GetCursorPos()
        {
            Point pos;
            if (GetCursorPos(out pos))
                return pos;
            throw new Exception();
        }

        /// <summary>
        ///     Load one of the predefined cursors
        /// </summary>
        /// <param name="name">Name of the predefined cursors</param>
        public static IntPtr GetSystemCursor(CursorNames name)
        {
            return LoadCursor(IntPtr.Zero, name);
        }

        /// <summary>
        ///     Hides the cursor. To show the cursor, call ShowCursor
        /// </summary>
        public static void HideCursor()
        {
            while (ShowCursor(false) > -1) { }
        }

        /// <summary>
        ///     Creates a cursor based on data contained in a file.
        /// </summary>
        /// <param name="fileName">
        ///     The source of the file data to be used to create the cursor. The data in the file
        ///     must be in either .CUR or .ANI format.
        /// </param>
        /// <returns>
        ///     If the function is successful, the return value is a handle to the new cursor.
        ///     If the function fails, <c>Win32Exception</c> is thrown.
        /// </returns>
        public static IntPtr LoadCursor(string fileName)
        {
            var cursor = LoadCursorFromFile(fileName);
            /*if (cursor == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());*/
            return cursor;
        }

        public static IntPtr LoadCursor(Stream cursorStream)
        {
            var tempFileName = Path.GetTempFileName();
            try
            {
                using (var reader = new BinaryReader(cursorStream))
                {
                    using (var stream = new FileStream(tempFileName, FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        var buffer = reader.ReadBytes(0x1000);
                        var length = buffer.Length;
                        while (length >= 0x1000)
                        {
                            stream.Write(buffer, 0, 0x1000);
                            length = reader.Read(buffer, 0, 0x1000);
                        }
                        stream.Write(buffer, 0, length);
                    }
                }
                var cursorHndl = LoadCursor(tempFileName);
                return cursorHndl;
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }

        /// <summary>
        ///     Enables an application to customize the system cursors. It replaces the contents of the system cursor
        ///     specified by the id parameter with the contents of the cursor specified by the curHwnd parameter and then
        ///     destroys curHwnd.
        /// </summary>
        /// <param name="curHwnd">
        ///     A handle to the cursor. The function replaces the contents of the system cursor
        ///     specified by id with the contents of the cursor handled by curHwnd.
        /// </param>
        /// <param name="id">The system cursor to replace with the contents of curHwnd.</param>
        /// <param name="destroyCur">Specify whether the cursor handle is destroyed.</param>
        public static void SetSystemCursor(IntPtr curHwnd, CursorNames id, bool destroyCur)
        {
            /*if ((destroyCur && !SetSystemCursor(curHwnd, id) || (!destroyCur && !SetSystemCursor(CopyCursor(curHwnd),id))))
                throw new Win32Exception(Marshal.GetLastWin32Error());*/
        }

        /// <summary>
        ///     Enables an application to customize the system cursors. It replaces the contents of the system cursor
        ///     specified by the id parameter with the contents of the cursor specified by the curHwnd parameter and then
        ///     destroys curHwnd.
        /// </summary>
        /// <param name="curHwnd">
        ///     A handle to the cursor. The function replaces the contents of the system cursor
        ///     specified by id with the contents of the cursor handled by curHwnd.
        /// </param>
        /// <param name="id">The system cursor to replace with the contents of curHwnd.</param>
        /// <param name="preCur">
        ///     If the function succeeds, the return value is the handle to the previous cursor, if there
        ///     was one.
        ///     <para>If there was no previous cursor, the return value is NULL.</para>
        /// </param>
        /// <param name="destroyCur">Specify whether the cursor handle is destroyed.</param>
        public static void SetSystemCursor(IntPtr curHwnd, CursorNames id, out IntPtr preCur, bool destroyCur)
        {
            /*preCur = CopyCursor(GetSystemCursor(id));
            IntPtr currentCursor = SetCursor(curHwnd);
            if ((destroyCur && !SetSystemCursor(curHwnd, id) || (!destroyCur && !SetSystemCursor(CopyCursor(curHwnd), id))))
            {
                DestroyCursor(preCur);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }*/
            if (!TrySetSystemCursor(curHwnd, id, out preCur, destroyCur))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        ///     Displays the cursor.
        /// </summary>
        public static void ShowCursor()
        {
            while (ShowCursor(true) < 0) { }
        }

        /// <summary>
        ///     Enables an application to customize the system cursors. It replaces the contents of the system cursor
        ///     specified by the id parameter with the contents of the cursor specified by the curHwnd parameter and then
        ///     destroys curHwnd.
        /// </summary>
        /// <param name="curHwnd">
        ///     A handle to the cursor. The function replaces the contents of the system cursor
        ///     specified by id with the contents of the cursor handled by curHwnd.
        /// </param>
        /// <param name="id">The system cursor to replace with the contents of curHwnd.</param>
        /// <param name="destroyCur">Specify whether the cursor handle is destroyed.</param>
        /// <returns>True if succeed; false otherwise.</returns>
        public static bool TrySetSystemCursor(IntPtr curHwnd, CursorNames id, bool destroyCur)
        {
            if ((destroyCur && !SetSystemCursor(curHwnd, id) ||
                 (!destroyCur && !SetSystemCursor(CopyCursor(curHwnd), id))))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Enables an application to customize the system cursors. It replaces the contents of the system cursor specified by
        ///     the id parameter with the contents of the cursor specified by the curHwnd parameter and then destroys curHwnd.
        /// </summary>
        /// <param name="curHwnd">
        ///     A handle to the cursor. The function replaces the contents of the system cursor specified by id
        ///     with the contents of the cursor handled by curHwnd.
        /// </param>
        /// <param name="id">The system cursor to replace with the contents of curHwnd.</param>
        /// <param name="preCur">
        ///     If the function succeeds, the return value is the handle to the previous cursor, if there was one.
        ///     <para>If there was no previous cursor, the return value is NULL.</para>
        /// </param>
        /// <param name="destroyCur">Specify whether the cursor handle is destroyed.</param>
        /// <returns>True if succeed; false otherwise.</returns>
        public static bool TrySetSystemCursor(IntPtr curHwnd, CursorNames id, out IntPtr preCur, bool destroyCur)
        {
            preCur = CopyCursor(GetSystemCursor(id));
            var currentCursor = SetCursor(curHwnd);
            if ((destroyCur && !SetSystemCursor(curHwnd, id) ||
                 (!destroyCur && !SetSystemCursor(CopyCursor(curHwnd), id))))
            {
                DestroyCursor(preCur);
                return false;
            }
            return true;
        }
        #endregion
    }
}