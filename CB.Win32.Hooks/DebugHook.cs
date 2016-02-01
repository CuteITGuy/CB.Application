using System;


namespace CB.Win32.Hooks
{
    public class DebugHook: HookBase
    {
        #region  Constructors & Destructor
        public DebugHook()
            : base(HookTypes.Debug)
        {
            Procedure = new HookProc(DebugProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     system calls this function before calling the hook procedures associated with any _type of hook. The system
        ///     passes information about the hook to be called to the DebugProc hook _procedure, which examines the information
        ///     and determines whether to allow the hook to be called.
        ///     The HOOKPROC _type defines a pointer to this callback function. DebugProc is a placeholder for the
        ///     application-defined or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     Specifies whether the hook _procedure must process the message. If nCode is HC_ACTION, the
        ///     hook _procedure must process the message. If nCode is less than zero, the hook _procedure must pass the message
        ///     to the CallNextHookEx function without further processing and should return the value returned by
        ///     CallNextHookEx.
        /// </param>
        /// <param name="wParam">The _type of hook about to be called, see <see cref="HookTypes" /></param>
        /// <param name="lParam">
        ///     A pointer to a DEBUGHOOKINFO structure that contains the parameters to be passed to the
        ///     destination hook _procedure. See <see cref="DebugHookStruct" />
        /// </param>
        /// <returns>
        ///     To prevent the system from calling the hook, the hook _procedure must return a nonzero value.
        ///     Otherwise, the hook _procedure must call CallNextHookEx.
        /// </returns>
        /// <remarks>
        ///     An application installs this hook _procedure by specifying the WH_DEBUG hook _type and the pointer to
        ///     the hook _procedure in a call to the SetWindowsHookEx function.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644978%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int DebugProc(int nCode, int wParam, int lParam)
        {
            if (nCode == HC_ACTION && Event != null)
            {
                var di = new DebugHookInfo(nCode, wParam, lParam);
                Event(this, di);
                if (di.Handled)
                    return 1;
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        #endregion
    }
}