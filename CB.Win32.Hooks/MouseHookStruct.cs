using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     The MOUSEHOOKSTRUCT structure contains information about a mouse event passed to a WH_MOUSE hook _procedure,
    ///     MouseProc.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644968%28v=vs.85%29.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseHookStruct
    {
        /// <summary>
        ///     Specifies a Point structure that contains the x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public Point Location;

        /// <summary>
        ///     Handle to the window that will receive the mouse _message corresponding to the mouse event.
        /// </summary>
        public IntPtr Handle;

        /// <summary>
        ///     Specifies the hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST _message.
        /// </summary>
        public HitTestValues HitTestCode;

        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        public IntPtr ExtraInfo;
    }
}