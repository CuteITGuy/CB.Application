#define LOCAL
using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    public static class Hook
    {
        #region Import
        /// <summary>
        ///     The CallNextHookEx function passes the hook information to the next hook _procedure in the current hook chain.
        ///     A hook _procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="hhk">Ignored.</param>
        /// <param name="nCode">
        ///     [in] Specifies the hook code passed to the current hook _procedure.
        ///     The next hook _procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        ///     [in] Specifies the wParam value passed to the current hook _procedure.
        ///     The meaning of this parameter depends on the _type of hook associated with the current hook chain.
        /// </param>
        /// <param name="lParam">
        ///     [in] Specifies the lParam value passed to the current hook _procedure.
        ///     The meaning of this parameter depends on the _type of hook associated with the current hook chain.
        /// </param>
        /// <returns>
        ///     This value is returned by the next hook _procedure in the chain.
        ///     The current hook _procedure must also return this value. The meaning of the return value depends on the hook _type.
        ///     For more information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, int wParam, int lParam);

        /*/// <summary>
        /// The CallNextHookEx function passes the hook information to the next hook _procedure in the current hook chain. 
        /// A hook _procedure can call this function either before or after processing the hook information.
        /// <para>Overloaded for use with LowLevelKeyboardProc</para>
        /// </summary>
        /// <param name="hhk">Ignored.</param>
        /// <param name="nCode">
        /// [in] Specifies the hook code passed to the current hook _procedure. 
        /// The next hook _procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies the wParam value passed to the current hook _procedure. 
        /// The meaning of this parameter depends on the _type of hook associated with the current hook chain. 
        /// </param>
        /// <param name="lParam">
        /// [in] Specifies the lParam value passed to the current hook _procedure. 
        /// The meaning of this parameter depends on the _type of hook associated with the current hook chain. 
        /// </param>
        /// <returns>
        /// This value is returned by the next hook _procedure in the chain. 
        /// The current hook _procedure must also return this value. The meaning of the return value depends on the hook _type. 
        /// For more information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WindowsMessages wParam, [In]KeyboardHookStruct lParam);

        /// <summary>
        /// The CallNextHookEx function passes the hook information to the next hook _procedure in the current hook chain. 
        /// A hook _procedure can call this function either before or after processing the hook information.
        /// <para>Overloaded for use with LowLevelMouseProc</para>
        /// </summary>
        /// <param name="hhk">Ignored.</param>
        /// <param name="nCode">
        /// [in] Specifies the hook code passed to the current hook _procedure. 
        /// The next hook _procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies the wParam value passed to the current hook _procedure. 
        /// The meaning of this parameter depends on the _type of hook associated with the current hook chain. 
        /// </param>
        /// <param name="lParam">
        /// [in] Specifies the lParam value passed to the current hook _procedure. 
        /// The meaning of this parameter depends on the _type of hook associated with the current hook chain. 
        /// </param>
        /// <returns>
        /// This value is returned by the next hook _procedure in the chain. 
        /// The current hook _procedure must also return this value. The meaning of the return value depends on the hook _type. 
        /// For more information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WindowsMessages wParam, [In]MouseLLHookStruct lParam);*/

        /// <summary>
        ///     Installs an application-defined hook _procedure into a hook chain. You would install a hook _procedure
        ///     to monitor the system for certain types of events. These events are associated either with a specific
        ///     thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="hookType">The _type of hook _procedure to be installed.</param>
        /// <param name="lpfn">
        ///     A pointer to the hook _procedure. If the dwThreadId parameter is zero or specifies the
        ///     identifier of a thread created by a different process, the lpfn parameter must point to a hook _procedure
        ///     in a DLL. Otherwise, lpfn can point to a hook _procedure in the code associated with the current process.
        /// </param>
        /// <param name="hMod">
        ///     A _handle to the DLL containing the hook _procedure pointed to by the lpfn parameter.
        ///     The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the
        ///     current process and if the hook _procedure is within the code associated with the current process.
        /// </param>
        /// <param name="dwThreadId">
        ///     The identifier of the thread with which the hook _procedure is to be associated.
        ///     For desktop apps, if this parameter is zero, the hook _procedure is associated with all existing threads
        ///     running in the same desktop as the calling thread. For Windows Store apps, see the Remarks section.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the _handle to the hook _procedure.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644990%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookTypes hookType, HookProc lpfn, IntPtr hMod, int dwThreadId);

        /// <summary>
        ///     Removes a hook _procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">
        ///     A _handle to the hook to be removed. This parameter is a hook _handle obtained by a
        ///     previous call to SetWindowsHookEx.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The hook _procedure can be in the state of being called by another thread even after UnhookWindowsHookEx
        ///     returns. If the hook _procedure is not being called concurrently, the hook _procedure is removed immediately
        ///     before UnhookWindowsHookEx returns.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644993%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        #endregion
    }
}