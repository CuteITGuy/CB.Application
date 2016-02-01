using System;


namespace CB.Win32.Hooks
{
    public class MouseHook: HookBase
    {
        #region  Constructors & Destructor
        public MouseHook()
            : base(HookTypes.Mouse)
        {
            Procedure = new HookProc(MouseProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     system calls this function whenever an application calls the GetMessage or PeekMessage function and there
        ///     is a mouse message to be processed.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer to this callback function.
        ///         MouseProc is a placeholder for the application-defined or library-defined function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     A code that the hook _procedure uses to determine how to process the message. If nCode is
        ///     less than zero, the hook _procedure must pass the message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.    The wParam and lParam parameters contain information about a mouse message.</para>
        ///     <para>
        ///         HC_NOREMOVE = 3.  The wParam and lParam parameters contain information about a mouse message, and the
        ///         mouse message has not been removed from the message queue. (An application called the PeekMessage function,
        ///         specifying the PM_NOREMOVE flag.)
        ///     </para>
        /// </param>
        /// <param name="wParam">The identifier of the mouse message.</param>
        /// <param name="lParam">A pointer to a MOUSEHOOKSTRUCT structure.</param>
        /// <returns>
        ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, and the hook _procedure did not process the message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_MOUSE _hooks will not receive hook notifications and may behave incorrectly as a result. If
        ///     the hook _procedure processed the message, it may return a nonzero value to prevent the system from passing the
        ///     message to the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_MOUSE hook _type and a pointer to the
        ///     hook _procedure in a call to the SetWindowsHookEx function.
        ///     The hook _procedure must not install a WH_JOURNALPLAYBACK callback function.
        ///     This hook may be called in the context of the thread that installed it. The call is made by sending a message
        ///     to the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644988%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int MouseProc(int nCode, int wParam, int lParam)
        {
            if ((nCode == HC_ACTION || nCode == HC_NOREMOVE) && Event != null)
            {
                var mi = new MouseHookInfo(nCode, wParam, lParam);
                Event(this, mi);
                if (mi.Handled)
                    return 1;
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        #endregion
    }
}