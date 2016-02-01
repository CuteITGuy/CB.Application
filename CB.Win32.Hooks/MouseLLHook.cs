using System;


namespace CB.Win32.Hooks
{
    public class MouseLLHook: HookBase
    {
        #region  Constructors & Destructor
        public MouseLLHook()
            : base(HookTypes.MouseLowLevel)
        {
            Procedure = new HookProc(MouseLLProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     system calls this function every time a new mouse input event is about to be posted into a thread input queue.
        ///     The HOOKPROC _type defines a pointer to this callback function. LowLevelMouseProc is a placeholder for
        ///     the application-defined or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the message. If nCode is less
        ///     than zero, the hook _procedure must pass the message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.The wParam and lParam parameters contain information about a mouse message.</para>
        /// </param>
        /// <param name="wParam">
        ///     The identifier of the mouse message. This parameter can be one of the following messages:
        ///     WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE, WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.
        /// </param>
        /// <param name="lParam">A pointer to an MSLLHOOKSTRUCT structure.</param>
        /// <returns>
        ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, and the hook _procedure did not process the message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_MOUSE_LL _hooks will not receive hook notifications and may behave incorrectly as a result.
        ///     If the hook _procedure processed the message, it may return a nonzero value to prevent the system from passing
        ///     the message to the rest of the hook chain or the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_MOUSE_LL hook _type and a pointer to
        ///     the hook _procedure in a call to the SetWindowsHookEx function.
        ///     This hook is called in the context of the thread that installed it. The call is made by sending a message to
        ///     the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.
        ///     The mouse input can come from the local mouse driver or from calls to the mouse_event function. If the input
        ///     comes from a call to mouse_event, the input was "injected". However, the WH_MOUSE_LL hook is not injected
        ///     into another process. Instead, the context switches back to the process that installed the hook and it is
        ///     called in its original context. Then the context switches back to the application that generated the event.
        ///     The hook _procedure should process a message in less time than the data entry specified in the
        ///     LowLevelHooksTimeout value in the following registry key:
        ///     HKEY_CURRENT_USER\Control Panel\Desktop
        ///     The value is in milliseconds. If the hook _procedure times out, the system passes the message to the next hook.
        ///     However, on Windows 7 and later, the hook is silently removed without being called. There is no way for the
        ///     application to know whether the hook is removed.
        ///     Note  Debug _hooks cannot track this _type of low level mouse _hooks. If the application must use low level
        ///     _hooks, it should run the _hooks on a dedicated thread that passes the work off to a worker thread and then
        ///     immediately returns. In most cases where the application needs to use low level _hooks, it should monitor
        ///     raw input instead. This is because raw input can asynchronously monitor mouse and keyboard messages that
        ///     are targeted for other threads more effectively than low level _hooks can. For more information on raw input,
        ///     see Raw Input.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644986%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int MouseLLProc(int nCode, int wParam, int lParam)
        {
            if (nCode == HC_ACTION && Event != null)
            {
                var mi = new MouseLLHookInfo(nCode, wParam, lParam);
                Event(this, mi);
                if (mi.Handled)
                    return 1;
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        #endregion
    }
}