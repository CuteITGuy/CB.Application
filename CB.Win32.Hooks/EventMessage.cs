using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains information about a hardware _message sent to the system _message queue. This structure is used to store
    ///     _message information for the JournalPlaybackProc callback function.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EventMessage
    {
        /// <summary>
        ///     The _message.
        /// </summary>
        public int Message;

        /// <summary>
        ///     Additional information about the _message. The exact meaning depends on the _message value.
        /// </summary>
        public int ParamL;

        /// <summary>
        ///     Additional information about the _message. The exact meaning depends on the _message value.
        /// </summary>
        public int ParamH;

        /// <summary>
        ///     The _time at which the _message was posted.
        /// </summary>
        public int Time;

        /// <summary>
        ///     A _handle to the window to which the _message was posted.
        /// </summary>
        public IntPtr Handle;
    }
}