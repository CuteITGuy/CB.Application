using System.Runtime.InteropServices;


namespace CB.Win32.Threading
{
    public static class ProcessThread
    {
        #region Import
        /// <summary>
        ///     Attaches or detaches the input processing mechanism of one thread to that of another thread.
        /// </summary>
        /// <param name="idAttach">
        ///     The identifier of the thread to be attached to another thread. The thread to be attached cannot
        ///     be a system thread.
        /// </param>
        /// <param name="idAttachTo">
        ///     <para>The identifier of the thread to which idAttach will be attached. This thread cannot be a system thread.</para>
        ///     <para>A thread cannot attach to itself. Therefore, idAttachTo cannot equal idAttach.</para>
        /// </param>
        /// <param name="fAttach">
        ///     If this parameter is TRUE, the two threads are attached. If the parameter is FALSE, the threads
        ///     are detached.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get
        ///     extended error information, call GetLastError.
        /// </returns>
        /// <remarks> https://msdn.microsoft.com/en-us/library/windows/desktop/ms681956(v=vs.85).aspx </remarks>
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        /// <summary>
        ///     Retrieves the thread identifier of the calling thread.
        /// </summary>
        /// <returns>The return value is the thread identifier of the calling thread.</returns>
        /// <remarks>
        ///     Until the thread terminates, the thread identifier uniquely identifies the thread throughout the system.
        ///     <para>https://msdn.microsoft.com/en-us/library/windows/desktop/ms683183(v=vs.85).aspx</para>
        ///     <para>Similar to AppDomain.GetCurrentThreadId() in .NET</para>
        /// </remarks>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        #endregion
    }
}