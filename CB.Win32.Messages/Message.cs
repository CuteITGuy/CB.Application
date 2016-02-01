using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Messages
{
    public static class Message
    {
        #region Import
        /// <summary>
        ///     Retrieves the extra message information for the current thread. Extra message information is an
        ///     application- or driver-defined value associated with the current thread's message queue.
        /// </summary>
        /// <returns>
        ///     The return value specifies the extra information. The meaning of the extra information is
        ///     device specific.
        /// </returns>
        /// <remarks>To set a thread's extra message information, use the SetMessageExtraInfo function.</remarks>
        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetMessageExtraInfo();
        #endregion
    }
}