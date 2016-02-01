using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains debugging information passed to a WH_DEBUG hook _procedure, DebugProc.
    /// </summary>
    /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644965%28v=vs.85%29.aspx</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct DebugHookStruct
    {
        /// <summary>
        ///     A _handle to the thread containing the filter function.
        /// </summary>
        public int IdThread;

        /// <summary>
        ///     A _handle to the thread that installed the debugging filter function.
        /// </summary>
        public int IdThreadInstaller;

        /// <summary>
        ///     The value to be passed to the hook in the lParam parameter of the DebugProc callback function.
        /// </summary>
        public int LParam;

        /// <summary>
        ///     The value to be passed to the hook in the wParam parameter of the DebugProc callback function.
        /// </summary>
        public int WParam;

        /// <summary>
        ///     The value to be passed to the hook in the nCode parameter of the DebugProc callback function.
        /// </summary>
        public int Code;
    }
}