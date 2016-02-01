using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CB.Win32.Common;
using CB.Win32.Cursors;
using CB.Win32.Threading;
using SHDocVw;


namespace CB.Win32.Windows
{
    public static unsafe class Window
    {
        #region Import
        /// <summary>
        ///     Calculates the required size of the window rectangle, based on the desired client-rectangle size.
        ///     The window rectangle can then be passed to the CreateWindow function to create a window whose
        ///     client area is the desired size.
        ///     <para>To specify an extended window style, use the AdjustWindowRectEx function.</para>
        /// </summary>
        /// <param name="lpRect">
        ///     A pointer to a Rect structure that contains the coordinates of the top-left and
        ///     bottom-right corners of the desired client area. When the function returns, the structure contains the
        ///     coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.
        /// </param>
        /// <param name="dwStyle">
        ///     The window style of the window whose required size is to be calculated. Note that
        ///     you cannot specify the Overlapped style.
        /// </param>
        /// <param name="bMenu">Indicates whether the window has a menu.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     A client rectangle is the smallest rectangle that completely encloses a client area. A window rectangle is the
        ///     smallest rectangle that completely encloses the window, which includes the client area and the nonclient area.
        ///     The AdjustWindowRect function does not add extra space when a menu bar wraps to two or more rows.
        ///     The AdjustWindowRect function does not take the WS_VSCROLL or WS_HSCROLL styles into account. To account for
        ///     the scroll bars, call the GetSystemMetrics function with SM_CXVSCROLL or SM_CYHSCROLL.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AdjustWindowRect(ref Rect lpRect, WindowStyles dwStyle, bool bMenu);

        /// <summary>
        ///     Calculates the required size of the window rectangle, based on the desired size of the client rectangle.
        ///     The window rectangle can then be passed to the CreateWindowEx function to create a window whose client area
        ///     is the desired size.
        /// </summary>
        /// <param name="lpRect">
        ///     A pointer to a Rect structure that contains the coordinates of the top-left and
        ///     bottom-right corners of the desired client area. When the function returns, the structure contains the
        ///     coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.
        /// </param>
        /// <param name="dwStyle">
        ///     The window style of the window whose required size is to be calculated. Note that
        ///     you cannot specify the Overlapped style.
        /// </param>
        /// <param name="bMenu">Indicates whether the window has a menu.</param>
        /// <param name="dwExStyle">The extended window style of the window whose required size is to be calculated. </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     A client rectangle is the smallest rectangle that completely encloses a client area. A window rectangle is the
        ///     smallest rectangle that completely encloses the window, which includes the client area and the nonclient area.
        ///     The AdjustWindowRectEx function does not add extra space when a menu bar wraps to two or more rows.
        ///     The AdjustWindowRectEx function does not take the WS_VSCROLL or WS_HSCROLL styles into account. To account
        ///     for the scroll bars, call the GetSystemMetrics function with SM_CXVSCROLL or SM_CYHSCROLL
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref Rect lpRect, WindowStyles dwStyle, bool bMenu,
            WindowStylesEx dwExStyle);

        /// <summary>
        ///     Enables you to produce special effects when showing or hiding windows.
        ///     There are four types of animation: roll, slide, collapse or expand, and alpha-blended fade.
        /// </summary>
        /// <remarks>
        ///     To show or hide a window without special effects, use ShowWindow.
        ///     When using slide or roll animation, you must specify the direction. It can be either
        ///     AW_HOR_POSITIVE, AW_HOR_NEGATIVE, AW_VER_POSITIVE, or AW_VER_NEGATIVE.
        ///     You can combine AW_HOR_POSITIVE or AW_HOR_NEGATIVE with AW_VER_POSITIVE
        ///     or AW_VER_NEGATIVE to animate a window diagonally.
        ///     The window procedures for the window and its child windows should handle any WM_PRINT or WM_PRINTCLIENT
        ///     messages. Dialog boxes, controls, and common controls already handle WM_PRINTCLIENT.
        ///     The default window procedure already handles WM_PRINT.
        ///     If a child window is displayed partially clipped, when it is animated it will have holes where it is clipped.
        /// </remarks>
        /// <param name="hwnd">
        ///     A handle to the window to animate. The calling thread must own this window.
        /// </param>
        /// <param name="time">
        ///     The time it takes to play the animation, in milliseconds.
        ///     Typically, an animation takes 200 milliseconds to play.
        /// </param>
        /// <param name="flags">
        ///     The type of animation. This parameter can be one or more of the following values.
        ///     Note that, by default, these flags take effect when showing a window.
        ///     To take effect when hiding a window, use AW_HIDE and a logical OR operator with the appropriate flags.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. The function will fail in the following situations:
        ///     - If the window is already visible and you are trying to show the window.
        ///     - If the window is already hidden and you are trying to hide the window.
        ///     - If there is no direction specified for the slide or roll animation.
        ///     - When trying to animate a child window with AW_BLEND.
        ///     - If the thread does not own the window. Note that, in this case,
        ///     AnimateWindow fails but GetLastError returns ERROR_SUCCESS.
        ///     To get extended error information, call the GetLastError function.
        /// </returns>
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        /// <summary>
        ///     Arranges all the minimized (iconic) child windows of the specified parent window.
        /// </summary>
        /// <param name="hWnd">A handle to the parent window.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the height of one row of icons.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern uint ArrangeIconicWindows(IntPtr hWnd);

        /// <summary>
        ///     Brings the specified window to the top of the Z order. If the window is a top-level window,
        ///     it is activated. If the window is a child window, the top-level parent window associated with
        ///     the child window is activated.
        /// </summary>
        /// <param name="hWnd">A handle to the window to bring to the top of the Z order.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        /// <summary>
        ///     Determines which, if any, of the child windows belonging to a parent window contains the specified point. The
        ///     search is restricted to immediate child windows. Grandchildren, and deeper descendant windows are not searched.
        ///     <para>To skip certain child windows, use the ChildWindowFromPointEx function.</para>
        /// </summary>
        /// <param name="hWndParent">A handle to the parent window.</param>
        /// <param name="point">
        ///     A structure that defines the client coordinates, relative to hWndParent, of the point to
        ///     be checked.
        /// </param>
        /// <returns>
        ///     The return value is a handle to the child window that contains the point, even if the child window
        ///     is hidden or disabled. If the point lies outside the parent window, the return value is NULL. If the point is
        ///     within the parent window but not within any child window, the return value is a handle to the parent window.
        /// </returns>
        /// <remarks>
        ///     The system maintains an internal list, containing the handles of the child windows associated with a parent
        ///     window. The order of the handles in the list depends on the Z order of the child windows. If more than one
        ///     child window contains the specified point, the system returns a handle to the first window in the list that
        ///     contains the point.
        ///     ChildWindowFromPoint treats an HTTRANSPARENT area of a standard control the same as other parts of the
        ///     control. In contrast, RealChildWindowFromPoint treats an HTTRANSPARENT area differently; it returns the child
        ///     window behind a transparent area of a control. For example, if the point is in a transparent area of a
        ///     groupbox, ChildWindowFromPoint returns the groupbox while RealChildWindowFromPoint returns the child window
        ///     behind the groupbox. However, both APIs return a static field, even though it, too, returns HTTRANSPARENT.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point point);

        /// <summary>
        ///     Determines which, if any, of the child windows belonging to the specified parent window contains the
        ///     specified point. The function can ignore invisible, disabled, and transparent child windows. The search
        ///     is restricted to immediate child windows. Grandchildren and deeper descendants are not searched.
        /// </summary>
        /// <param name="hWndParent">A handle to the parent window.</param>
        /// <param name="pt">
        ///     A structure that defines the client coordinates (relative to hwndParent) of the point
        ///     to be checked.
        /// </param>
        /// <param name="uFlags">The child windows to be skipped.</param>
        /// <returns>
        ///     The return value is a handle to the first child window that contains the point and meets the
        ///     criteria specified by uFlags. If the point is within the parent window but not within any child window
        ///     that meets the criteria, the return value is a handle to the parent window. If the point lies outside
        ///     the parent window or if the function fails, the return value is NULL.
        /// </returns>
        /// <remarks>
        ///     The system maintains an internal list that contains the handles of the child windows associated with a
        ///     parent window. The order of the handles in the list depends on the Z order of the child windows. If more
        ///     than one child window contains the specified point, the system returns a handle to the first window in
        ///     the list that contains the point and meets the criteria specified by uFlags.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, Point pt, WindowFromPointFlags uFlags);

        /// <summary>
        ///     The ClientToScreen function converts the client-area coordinates of a specified point to screen coordinates.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client area is used for the conversion.</param>
        /// <param name="lpPoint">
        ///     A pointer to a Point structure that contains the client coordinates to be converted.
        ///     The new screen coordinates are copied into this structure if the function succeeds.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.
        /// </returns>
        /// <remarks>
        ///     The ClientToScreen function replaces the client-area coordinates in the Point structure with the
        ///     screen coordinates. The screen coordinates are relative to the upper-left corner of the screen. Note, a
        ///     screen-coordinate point that is above the window's client area has a negative y-coordinate. Similarly, a
        ///     screen coordinate to the left of a client area has a negative x-coordinate.
        ///     All coordinates are device coordinates.
        /// </remarks>
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        /// <summary>
        ///     Minimizes (but does not destroy) the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be minimized. </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        /// <summary>
        ///     <para>
        ///         The DestroyWindow function destroys the specified window. The function sends WM_DESTROY
        ///         and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it.
        ///         The function also destroys the window's menu, flushes the thread message queue, destroys timers,
        ///         removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of
        ///         the viewer chain).
        ///     </para>
        ///     <para>
        ///         If the specified window is a parent or owner window, DestroyWindow automatically destroys the
        ///         associated child or owned windows when it destroys the parent or owner window. The function first
        ///         destroys child or owned windows, and then it destroys the parent or owner window.
        ///     </para>
        ///     <para>DestroyWindow also destroys modeless dialog boxes created by the CreateDialog function.</para>
        /// </summary>
        /// <param name="hwnd">Handle to the window to be destroyed.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails,
        ///     the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        /// <summary>
        ///     Enumerates the child windows that belong to the specified parent window by passing the handle to each
        ///     child window, in turn, to an application-defined callback function. EnumChildWindows continues until
        ///     the last child window is enumerated or the callback function returns FALSE.
        /// </summary>
        /// <param name="hwndParent">
        ///     A handle to the parent window whose child windows are to be enumerated.
        ///     If this parameter is NULL, this function is equivalent to EnumWindows.
        /// </param>
        /// <param name="lpEnumFunc">A pointer to an application-defined callback function.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        /// <returns>The return value is not used.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, void* lParam);

        /// <summary>
        ///     Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an
        ///     application-defined callback function. EnumWindows continues until the last top-level window is enumerated
        ///     or the callback function returns FALSE.
        /// </summary>
        /// <param name="lpEnumFunc">A pointer to an application-defined callback function.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
        ///     <para>
        ///         If EnumWindowsProc returns zero, the return value is also zero. In this case, the callback function
        ///         should call SetLastError to obtain a meaningful error code to be returned to the caller of EnumWindows.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        ///     <para>
        ///         Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        ///         This function does not search child windows. This function does not perform a case-sensitive search.
        ///     </para>
        ///     <para>
        ///         To search child windows, beginning with a specified child window, use the FindWindowEx function.
        ///     </para>
        /// </summary>
        /// <param name="lpClassName">
        ///     <para>
        ///         The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx
        ///         function. The atom must be in the low-order word of lpClassName; the high-order word must be zero.
        ///     </para>
        ///     <para>
        ///         If lpClassName points to a string, it specifies the window class name. The class name can be any name
        ///         registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        ///     </para>
        ///     <para>
        ///         If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.
        ///     </para>
        /// </param>
        /// <param name="lpWindowName">
        ///     The window name (the window's title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     <para>
        ///         If the function succeeds, the return value is a handle to the window that has the specified class name
        ///         and window name.
        ///     </para>
        ///     <para>
        ///         If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        ///     Retrieves a handle to a window whose class name and window name match the specified strings.
        ///     The function searches child windows, beginning with the one following the specified child window.
        ///     This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="hwndParent">
        ///     A handle to the parent window whose child windows are to be searched.
        ///     If hwndParent is NULL, the function uses the desktop window as the parent window.
        ///     The function searches among windows that are child windows of the desktop.
        ///     If hwndParent is HWND_MESSAGE, the function searches all message-only windows.
        /// </param>
        /// <param name="hwndChildAfter">
        ///     A handle to a child window. The search begins with the next child window in the Z order.
        ///     The child window must be a direct child window of hwndParent, not just a descendant window.
        ///     If hwndChildAfter is NULL, the search begins with the first child window of hwndParent.
        ///     Note that if both hwndParent and hwndChildAfter are NULL, the function searches all
        ///     top-level and message-only windows.
        /// </param>
        /// <param name="lpszClass">
        ///     The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function.
        ///     The atom must be placed in the low-order word of lpszClass; the high-order word must be zero.
        ///     If lpszClass is a string, it specifies the window class name. The class name can be any name registered
        ///     with RegisterClass or RegisterClassEx, or any of the predefined control-class names, or it can be
        ///     MAKEINTATOM(0x8000). In this latter case, 0x8000 is the atom for a menu class.
        ///     If the lpszWindow parameter is not NULL, FindWindowEx calls the GetWindowText function to retrieve
        ///     the window name for comparison. For a description of a potential problem that can arise, see the
        ///     Remarks section of GetWindowText.
        ///     An application can call this function in the following way.
        ///     FindWindowEx( NULL, NULL, MAKEINTATOM(0x8000), NULL );
        ///     Note that 0x8000 is the atom for a menu class. When an application calls this function, the function
        ///     checks whether a context menu is being displayed that the application created.
        /// </param>
        /// <param name="lpszWindow">
        ///     The window name (the window's title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the window that has the specified class
        ///     and window names. If the function fails, the return value is NULL. To get extended error
        ///     information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        /// <summary>
        ///     Retrieves the coordinates of a window's client area. The client coordinates specify the upper-left
        ///     and lower-right corners of the client area. Because client coordinates are relative to the upper-left
        ///     corner of a window's client area, the coordinates of the upper-left corner are (0,0).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client coordinates are to be retrieved. </param>
        /// <param name="lpRect">
        ///     A pointer to a Rectange structure that receives the client coordinates. The left and
        ///     top members are zero. The right and bottom members contain the width and height of the window.
        /// </param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently working).
        ///     The system assigns a slightly higher priority to the thread that creates the foreground window than
        ///     it does to other threads.
        /// </summary>
        /// <returns>
        ///     The return value is a handle to the foreground window. The foreground window can be NULL
        ///     in certain circumstances, such as when a window is losing activation.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     Retrieves information about the active window or a specified GUI thread.
        /// </summary>
        /// <param name="idThread">
        ///     The identifier for the thread for which information is to be retrieved. To retrieve
        ///     this value, use the GetWindowThreadProcessId function. If this parameter is NULL, the function returns
        ///     information for the foreground thread.
        /// </param>
        /// <param name="lpgui">
        ///     A pointer to a GuiThreadInfo structure that receives information describing the thread.
        ///     Note that you must set the cbSize member to sizeof(GuiThreadInfo) before calling this function.
        /// </param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool GetGUIThreadInfo(int idThread, ref GuiThreadInfo lpgui);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        /// <summary>
        ///     Examines the Z order of the child windows associated with the specified parent window and retrieves a handle
        ///     to the child window at the top of the Z order.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the parent window whose child windows are to be examined. If this parameter is
        ///     NULL, the function returns a handle to the window at the top of the Z order.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the child window at the top of the Z order.
        ///     If the specified window has no child windows, the return value is NULL. To get extended error information,
        ///     use the GetLastError function.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        /// <summary>
        ///     Retrieves information about the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose information is to be retrieved. </param>
        /// <param name="pwi">
        ///     A pointer to a WINDOWINFO structure to receive the information.
        ///     Note that you must set the cbSize member to sizeof(WINDOWINFO) before calling this function.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.
        ///     To get extended error information, call GetLastError.
        /// </returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WindowInfo pwi);

        /// <summary>
        ///     Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        ///     A pointer to the WINDOWPLACEMENT structure that receives the show state and position information.
        ///     <para>
        ///         Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT).
        ///         GetWindowPlacement fails if lpwndpl-> length is not set correctly.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        /// <summary>
        ///     Retrieves the dimensions of the bounding rectangle of the specified window.
        ///     The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">
        ///     A pointer to a Rectangle structure that receives the screen coordinates of the
        ///     upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        /// <summary>
        ///     Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a
        ///     control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in
        ///     another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpString">
        ///     The buffer that will receive the text. If the string is as long or longer than the
        ///     buffer, the string is truncated and terminated with a null character.
        /// </param>
        /// <param name="nMaxCount">
        ///     The maximum number of characters to copy to the buffer, including the null character.
        ///     If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the length, in characters, of the copied string, not
        ///     including the terminating null character. If the window has no title bar or text, if the title bar is empty,
        ///     or if the window or control handle is invalid, the return value is zero. To get extended error information,
        ///     call GetLastError.
        ///     This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        /// <remarks>
        ///     If the target window is owned by the current process, GetWindowText causes a WM_GETTEXT message to be
        ///     sent to the specified window or control. If the target window is owned by another process and has a caption,
        ///     GetWindowText retrieves the window caption text. If the window does not have a caption, the return value is a
        ///     null string. This behavior is by design. It allows applications to call GetWindowText without becoming
        ///     unresponsive if the process that owns the target window is not responding. However, if the target window is not
        ///     responding and it belongs to the calling application, GetWindowText will cause the calling application to
        ///     become unresponsive.
        ///     To retrieve the text of a control in another process, send a WM_GETTEXT message directly instead of calling
        ///     GetWindowText.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms633520%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        /// <summary>
        ///     Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a
        ///     control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in
        ///     another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpString">
        ///     The buffer that will receive the text. If the string is as long or longer than the
        ///     buffer, the string is truncated and terminated with a null character.
        /// </param>
        /// <param name="nMaxCount">
        ///     The maximum number of characters to copy to the buffer, including the null character.
        ///     If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the length, in characters, of the copied string, not
        ///     including the terminating null character. If the window has no title bar or text, if the title bar is empty,
        ///     or if the window or control handle is invalid, the return value is zero. To get extended error information,
        ///     call GetLastError.
        ///     This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        /// <remarks>
        ///     If the target window is owned by the current process, GetWindowText causes a WM_GETTEXT message to be
        ///     sent to the specified window or control. If the target window is owned by another process and has a caption,
        ///     GetWindowText retrieves the window caption text. If the window does not have a caption, the return value is a
        ///     null string. This behavior is by design. It allows applications to call GetWindowText without becoming
        ///     unresponsive if the process that owns the target window is not responding. However, if the target window is not
        ///     responding and it belongs to the calling application, GetWindowText will cause the calling application to
        ///     become unresponsive.
        ///     To retrieve the text of a control in another process, send a WM_GETTEXT message directly instead of calling
        ///     GetWindowText.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms633520%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        /// <summary>
        ///     Retrieves the length, in characters, of the specified window's title bar text (if the window has a
        ///     title bar). If the specified window is a control, the function retrieves the length of the text
        ///     within the control. However, GetWindowTextLength cannot retrieve the length of the text of an
        ///     edit control in another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the length, in characters, of the text.
        ///     Under certain conditions, this value may actually be greater than the length of the text.
        ///     If the window has no text, the return value is zero.
        ///     To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier
        ///     of the process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">
        ///     A pointer to a variable that receives the process identifier. If this parameter
        ///     is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it
        ///     does not.
        /// </param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms633522%28v=vs.85%29.aspx</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier
        ///     of the process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="processId">
        ///     A pointer to a variable that receives the process identifier. If this parameter
        ///     is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; When you don't want
        ///     the ProcessId, pass IntPtr.Zero for this parameter
        /// </param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms633522%28v=vs.85%29.aspx</remarks>
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        /// <summary>
        ///     Determines whether the specified window is minimized (iconic).
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>
        ///     If the window is iconic, the return value is nonzero.
        ///     <para>If the window is not iconic, the return value is zero.</para>
        /// </returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms633527%28v=vs.85%29.aspx</remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        ///     The MoveWindow function changes the position and dimensions of the specified window.
        ///     For a top-level window, the position and dimensions are relative to the upper-left corner of the screen.
        ///     For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="x">Specifies the new position of the left side of the window.</param>
        /// <param name="y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">
        ///     Specifies whether the window is to be repainted. If this parameter is TRUE,
        ///     the window receives a message. If the parameter is FALSE, no repainting of any kind occurs.
        ///     This applies to the client area, the nonclient area (including the title bar and scroll bars),
        ///     and any part of the parent window uncovered as a result of moving a child window.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information,
        ///         call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        ///     Restores a minimized (iconic) window to its previous size and position; it then activates the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be restored and activated. </param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool OpenIcon(IntPtr hWnd);

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

        /// <summary>
        ///     Retrieves a handle to the child window at the specified point. The search is restricted to immediate
        ///     child windows; grandchildren and deeper descendant windows are not searched.
        /// </summary>
        /// <param name="hWndParent">A handle to the window whose child is to be retrieved.</param>
        /// <param name="ptParentClientCoords">
        ///     A Point structure that defines the client coordinates of the point
        ///     to be checked.
        /// </param>
        /// <returns>The return value is a handle to the child window that contains the specified point. </returns>
        /// <remarks>
        ///     RealChildWindowFromPoint treats HTTRANSPARENT areas of a standard control differently from other areas
        ///     of the control; it returns the child window behind a transparent part of a control. In contrast,
        ///     ChildWindowFromPoint treats HTTRANSPARENT areas of a control the same as other areas. For example,
        ///     if the point is in a transparent area of a groupbox, RealChildWindowFromPoint returns the child window
        ///     behind a groupbox, whereas ChildWindowFromPoint returns the groupbox. However, both APIs return a static
        ///     field, even though it, too, returns HTTRANSPARENT.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr RealChildWindowFromPoint(IntPtr hWndParent, Point ptParentClientCoords);

        /// <summary>
        ///     Retrieves a string that specifies the window type.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose type will be retrieved.</param>
        /// <param name="pszType">A pointer to a string that receives the window type.</param>
        /// <param name="cchType">The length, in characters, of the buffer pointed to by the pszType parameter.</param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is the number of characters copied to the specified buffer.</para>
        ///     <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
        /// </returns>
        [DllImport("user32.dll")]
        static extern int RealGetWindowClass(IntPtr hWnd, [Out] char[] pszType, int cchType);

        [DllImport("user32.dll")]
        private static extern int RealGetWindowClass(IntPtr hWnd, [Out] StringBuilder pszType, int cchType);

        /// <summary>
        ///     The ScreenToClient function converts the screen coordinates of a specified point on the screen to
        ///     client-area coordinates.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client area will be used for the conversion.</param>
        /// <param name="lpPoint">A pointer to a Point structure that specifies the screen coordinates to be converted.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.
        /// </returns>
        /// <remarks>
        ///     The function uses the window identified by the hWnd parameter and the screen coordinates given in the Point
        ///     structure to compute client coordinates. It then replaces the screen coordinates with the client coordinates.
        ///     The new coordinates are relative to the upper-left corner of the specified window's client area.
        ///     The ScreenToClient function assumes the specified point is in screen coordinates.
        ///     All coordinates are in device units.
        ///     Do not use ScreenToClient when in a mirroring situation, that is, when changing from left-to-right layout to
        ///     right-to-left layout. Instead, use MapWindowPoints. For more information, see "Window Layout and Mirroring"
        ///     in Window Features.
        /// </remarks>
        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        // For Windows Mobile, replace user32.dll with coredll.dll
        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed
        ///     to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to
        ///     the thread
        ///     that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>
        ///     If the window was brought to the foreground, the return value is nonzero.
        ///     <para>If the window was not brought to the foreground, the return value is zero.</para>
        /// </returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx</remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are
        ///     ordered according to their appearance on the screen. The topmost window receives the highest rank and
        ///     is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order.</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels.</param>
        /// <param name="uFlags">The window sizing and positioning flags.</param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, SpecialWindowHandles hWndInsertAfter,
            int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

        /// <summary>
        ///     Changes the text of the specified window's title bar (if it has one). If the specified window is a
        ///     control, the text of the control is changed. However, SetWindowText cannot change the text of a
        ///     control in another application.
        /// </summary>
        /// <param name="hwnd">A handle to the window or control whose text is to be changed.</param>
        /// <param name="lpString">The new title or control text.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);

        /// <summary>Shows a Window</summary>
        /// <remarks>
        ///     <para>To perform certain special effects when showing or hiding a window, use AnimateWindow.</para>
        ///     <para>
        ///         The first time an application calls ShowWindow, it should use the WinMain function's nCmdShow parameter
        ///         as its nCmdShow parameter. Subsequent calls to ShowWindow must use one of the values in the given list,
        ///         instead of the one specified by the WinMain function's nCmdShow parameter.
        ///     </para>
        ///     <para>
        ///         As noted in the discussion of the nCmdShow parameter, the nCmdShow value is ignored in the first call
        ///         to ShowWindow if the program that launched the application specifies startup information in the structure.
        ///         In this case, ShowWindow uses the information specified in the STARTUPINFO structure to show the window. On
        ///         subsequent calls, the application must call ShowWindow with nCmdShow set to SW_SHOWDEFAULT to use the startup
        ///         information provided by the program that launched the application. This behavior is designed for
        ///         the following situations:
        ///     </para>
        ///     <list type="">
        ///         <item>Applications create their main window by calling CreateWindow with the WS_VISIBLE flag set. </item>
        ///         <item>
        ///             Applications create their main window by calling CreateWindow with the WS_VISIBLE flag cleared, and later
        ///             call ShowWindow with the SW_SHOW flag set to make it visible.
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="nCmdShow">
        ///     Specifies how the window is to be shown. This parameter is ignored the first time an
        ///     application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure.
        ///     Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function
        ///     in its nCmdShow parameter. In subsequent calls, this parameter can be one of the WindowShowStyle members.
        /// </param>
        /// <returns>
        ///     If the window was previously visible, the return value is nonzero.
        ///     If the window was previously hidden, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);

        /// <summary>
        ///     Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// <param name="point">The point to be checked.</param>
        /// <returns>
        ///     The return value is a handle to the window that contains the point. If no window exists at the
        ///     given point, the return value is NULL. If the point is over a static text control, the return value is
        ///     a handle to the window under the static text control.
        /// </returns>
        /// <remarks>
        ///     The WindowFromPoint function does not retrieve a handle to a hidden or disabled window, even if the point
        ///     is within the window. An application should use the ChildWindowFromPoint function for a nonrestrictive search.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);
        #endregion


        #region Methods
        public static Rect AdjustWindowRect(IntPtr hWnd, Rect clientRect) // get whether has menu?
        {
            var info = WindowInfo.Default;
            GetWindowInfo(hWnd, ref info);
            AdjustWindowRectEx(ref clientRect, info.WindowStyle, true, info.ExtendedStyle);
            return clientRect;
        }

        public static Rectangle AdjustWindowRect(IntPtr hWnd, Rectangle clientRect) // get whether has menu?
        {
            return AdjustWindowRect(hWnd, (Rect)clientRect);
        }

        public static IntPtr[] GetAllExplorerHandles()
        {
            return GetAllExplorers().Select(e => (IntPtr)e.HWND).ToArray();
        }

        public static InternetExplorer[] GetAllExplorers()
        {
            return new ShellWindows().OfType<InternetExplorer>().ToArray();
        }

        public static IntPtr[] GetAllInternetExplorerHandles()
        {
            return GetAllInternetExplorers().Select(e => (IntPtr)e.HWND).ToArray();
        }

        public static InternetExplorer[] GetAllInternetExplorers()
        {
            return new ShellWindows().OfType<InternetExplorer>().Where(e => e.Name == "Internet Explorer").ToArray();
        }

        public static IntPtr[] GetAllWindowsExplorerHandles()
        {
            return GetAllWindowsExplorers().Select(e => (IntPtr)e.HWND).ToArray();
        }

        public static InternetExplorer[] GetAllWindowsExplorers()
        {
            return new ShellWindows().OfType<InternetExplorer>().Where(e => e.Name == "Windows Explorer").ToArray();
        }

        /// <summary>
        ///     Retrieve handle of the next Windows Explorer in the Z-order
        /// </summary>
        /// <param name="lastHwnd">
        ///     Handle of the previous Windows Explorer
        ///     If lastHwnd is IntPtr.Zero the function returns the first Windows Explorer's handle found.
        /// </param>
        /// <returns></returns>
        public static IntPtr GetNextWindowsExplorer(IntPtr lastHwnd)
        {
            return FindWindowEx(IntPtr.Zero, lastHwnd, "CabinetWClass", null);
        }

        public static Point GetRelativePos(IntPtr hWnd)
        {
            Rect rect;
            GetWindowRect(hWnd, out rect);
            var parentHwnd = GetParent(hWnd);
            var parentInfo = WindowInfo.Default;
            GetWindowInfo(parentHwnd, ref parentInfo);
            var parentRect = parentInfo.ClientRect;
            return new Point(rect.X - parentRect.X, rect.Y - parentRect.Y);
        }

        /// <summary>
        ///     Get the top parent window at the specified position.
        /// </summary>
        /// <param name="position">Coordinates of the position.</param>
        /// <returns>
        ///     Handle of the top parent window. If there is no window at the specified position the return
        ///     value is NULL.
        /// </returns>
        public static IntPtr GetTopLevelWindowAt(Point position)
        {
            var hWnd = WindowFromPoint(position);
            /*IntPtr parent;
            while ((parent = GetParent(hndl)) != IntPtr.Zero)
                hndl = parent;
            return hndl;*/
            return GetTopLevelWindowOf(hWnd);
        }

        /// <summary>
        ///     Get the top parent window at the cursor position.
        /// </summary>
        /// <returns>
        ///     Handle of the top parent window. If there is no window at the cursor position the return
        ///     value is NULL.
        /// </returns>
        public static IntPtr GetTopLevelWindowAtMouse()
        {
            return GetTopLevelWindowAt(Cursor.GetCursorPos());
        }

        /// <summary>
        ///     Get the top parent window that host the specified window (the child window).
        /// </summary>
        /// <param name="hWnd">Handle of the child window.</param>
        /// <returns>
        ///     Handle of the top parent window. If <c>hWnd</c> is already a top parent window the return value
        ///     is equal to <c>hWnd</c>. If <c>hWnd</c> is NULL the return value is NULL.
        /// </returns>
        public static IntPtr GetTopLevelWindowOf(IntPtr hWnd)
        {
            IntPtr parent;
            while ((parent = GetParent(hWnd)) != IntPtr.Zero)
                hWnd = parent;
            return hWnd;
        }

        public static IntPtr GetWindowAtMouse()
        {
            return WindowFromPoint(Cursor.GetCursorPos());
        }

        /*public static string GetWindowClass(IntPtr hWnd)
        {
            int capacity = 255;
            StringBuilder sb = new StringBuilder(capacity);
            RealGetWindowClass(hWnd, sb, capacity);
            return sb.ToString();
        }*/

        public static string GetWindowClass(IntPtr hWnd)
        {
            var capacity = 255;
            var buffer = new char[capacity];
            var length = RealGetWindowClass(hWnd, buffer, capacity);
            return new string(buffer, 0, length);
        }

        /// <summary>
        ///     Retrieves the process of the thread that created the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <returns>The identifier of the thread that created the window.</returns>
        public static int GetWindowProcessId(IntPtr hWnd)
        {
            int result;
            GetWindowThreadProcessId(hWnd, out result);
            return result;
        }

        /// <summary>
        ///     Retrieves content of the address bar of a Windows Explorer
        /// </summary>
        /// <param name="hWnd">
        ///     Handle of the Windows Explorer.
        /// </param>
        /// <returns></returns>
        /// <summary>
        ///     Get the content text and the handle of the address bar of given Windows Explorer.
        /// </summary>
        /// <param name="addrBarHndl">Handle of the address bar.</param>
        /// <returns>The content text of the address bar.</returns>
        public static string GetWindowsExplorerAddress(IntPtr hWnd, out IntPtr addrBarHndl)
        {
            addrBarHndl = hWnd;
            if (addrBarHndl != IntPtr.Zero && GetWindowClass(addrBarHndl) == "CabinetWClass")
            {
                string[] classNames =
                {
                    "WorkerW", "ReBarWindow32", "Address Band Root", "msctls_progress32",
                    "Breadcrumb Parent", "ToolbarWindow32"
                };
                for (var i = -1; i < classNames.Length - 1 && addrBarHndl != IntPtr.Zero;
                    i++, addrBarHndl = FindWindowEx(addrBarHndl, IntPtr.Zero, classNames[i], null)) { }
                if (addrBarHndl != IntPtr.Zero)
                {
                    var caption = GetWindowText(addrBarHndl);
                    if (!string.IsNullOrEmpty(caption) && caption.StartsWith("Address:"))
                        return caption.Substring(9);
                }
            }
            return null;
        }

        /// <summary>
        ///     Retrieves an array of handles of all Windows Explorers currently on Desktop
        /// </summary>
        /// <returns></returns>
        public static IntPtr[] GetWindowsExplorers()
        {
            return FindAllWindowsExplorers().ToArray();
        }

        public static string GetWindowsExplorerSearchText(IntPtr hWnd, out IntPtr searchBarHndl)
        {
            searchBarHndl = hWnd;
            if (searchBarHndl != IntPtr.Zero && GetWindowClass(searchBarHndl) == "CabinetWClass")
            {
                string[] classNames =
                {
                    "WorkerW", "ReBarWindow32", "UniversalSearchBand", "msctls_progress32",
                    "Breadcrumb Parent", "ToolbarWindow32"
                };
                for (var i = -1; i < classNames.Length - 1 && searchBarHndl != IntPtr.Zero;
                    i++, searchBarHndl = FindWindowEx(searchBarHndl, IntPtr.Zero, classNames[i], null)) { }
                if (searchBarHndl != IntPtr.Zero)
                    return GetWindowText(searchBarHndl);
            }
            return null;
        }

        /// <summary>
        ///     Retrieves the text of the specified window's title bar (if it has one). If the specified window is a control,
        ///     the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another
        ///     application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <returns>
        ///     The text of the window's title bar. If the window does not have a caption, the return value is an
        ///     empty string.
        /// </returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            var capacity = 255;
            var sb = new StringBuilder(capacity);
            GetWindowText(hWnd, sb, capacity);
            return sb.ToString();
        }

        /// <summary>
        ///     Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <returns>The identifier of the thread that created the window.</returns>
        public static int GetWindowThreadId(IntPtr hWnd)
        {
            return GetWindowThreadProcessId(hWnd, IntPtr.Zero);
        }

        public static bool IsDesktop(IntPtr hWnd)
        {
            return GetWindowText(hWnd) == "FolderView";
        }

        public static bool IsInternetExplorer(IntPtr hWnd)
        {
            return GetAllInternetExplorerHandles().Contains(hWnd);
        }

        public static bool IsMouseOverDesktop()
        {
            return IsDesktop(GetWindowAtMouse());
        }

        public static bool IsMouseOverWindowsExplorer()
        {
            return IsWindowsExplorer(GetTopLevelWindowAtMouse());
        }

        public static bool IsWindowsExplorer(IntPtr hWnd)
        {
            return GetAllWindowsExplorerHandles().Contains(hWnd);
        }

        /// <summary>
        ///     Converts the screen coordinates of a specified point on the screen to client-area coordinates.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client area is used for the conversion.</param>
        /// <param name="ptToScreen">
        ///     A Point structure that contains the screen coordinates to be converted.
        ///     The new screen coordinates are copied into this structure if the function succeeds.
        /// </param>
        /// <returns>The converted client-area coordinates.</returns>
        public static Point PointToClient(IntPtr hWnd, Point ptToScreen)
        {
            if (ScreenToClient(hWnd, ref ptToScreen))
                return ptToScreen;
            throw new Exception();
        }

        /// <summary>
        ///     Converts the client-area coordinates of a specified point to screen coordinates.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client area is used for the conversion.</param>
        /// <param name="ptToClient">A Point structure that contains the client coordinates to be converted.</param>
        /// <returns>The converted screen coordinates.</returns>
        public static Point PointToScreen(IntPtr hWnd, Point ptToClient)
        {
            if (ClientToScreen(hWnd, ref ptToClient))
                return ptToClient;
            throw new Exception();
        }

        /// <summary>
        ///     Changes the size of a child, pop-up, or top-level window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="width">The new width of the window, in pixels.</param>
        /// <param name="height">The new height of the window, in pixels.</param>
        public static void ResizeWindow(IntPtr hWnd, int width, int height)
        {
            SetWindowPosition(hWnd, 0, 0, 0, width, height, SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder);
        }

        /// <summary>
        ///     Changes the position of a child, pop-up, or top-level window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        public static void SetWindowPosition(IntPtr hWnd, int x, int y)
        {
            SetWindowPosition(hWnd, 0, x, y, 0, 0, SetWindowPosFlags.NoZOrder | SetWindowPosFlags.NoSize);
        }

        /// <summary>
        ///     Changes the Z order of a child, pop-up, or top-level window. These windows are ordered according
        ///     to their appearance on the screen. The topmost window receives the highest rank and is the first
        ///     window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="specialHndl">A handle to the window to precede the positioned window in the Z order.</param>
        public static void SetWindowPosition(IntPtr hWnd, SpecialWindowHandles specialHndl)
        {
            SetWindowPosition(hWnd, specialHndl, 0, 0, 0, 0, SetWindowPosFlags.NoMove | SetWindowPosFlags.NoSize);
        }

        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are
        ///     ordered according to their appearance on the screen. The topmost window receives the highest rank and
        ///     is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="specialHndl">A handle to the window to precede the positioned window in the Z order.</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="width">The new width of the window, in pixels.</param>
        /// <param name="height">The new height of the window, in pixels.</param>
        /// <param name="posFlags">The window sizing and positioning flags.</param>
        public static void SetWindowPosition(IntPtr hWnd, SpecialWindowHandles specialHndl, int x, int y, int width,
            int height, SetWindowPosFlags posFlags)
        {
            if (!SetWindowPos(hWnd, specialHndl, x, y, width, height, posFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        #endregion


        #region Implementation
        static IEnumerable<IntPtr> FindAllWindowsExplorers()
        {
            var hndl = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "CabinetWClass", null);
            while (hndl != IntPtr.Zero)
            {
                yield return hndl;
                hndl = FindWindowEx(IntPtr.Zero, hndl, "CabinetWClass", null);
            }
        }
        #endregion
    }
}