using System;


namespace CB.Win32.Hooks
{
    public class KeyboardHook: HookBase
    {
        #region  Constructors & Destructor
        public KeyboardHook()
            : base(HookTypes.Keyboard)
        {
            Procedure = new HookProc(KeyboardProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The system
        ///     calls this function whenever an application calls the GetMessage or PeekMessage function and there is a
        ///     keyboard message (WM_KEYUP or WM_KEYDOWN) to be processed.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer to this
        ///         callback function. KeyboardProc is a placeholder for the application-defined or library-defined function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the message. If code is less
        ///     than zero, the hook _procedure must pass the message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.    The wParam and lParam parameters contain information about a keystroke message.</para>
        ///     <para>
        ///         HC_NOREMOVE = 3.  The wParam and lParam parameters contain information about a keystroke message, and
        ///         the keystroke message has not been removed from the message queue. (An application called the PeekMessage
        ///         function, specifying the PM_NOREMOVE flag.)
        ///     </para>
        /// </param>
        /// <param name="wParam">The virtual-key code of the key that generated the keystroke message.</param>
        /// <param name="lParam">
        ///     The repeat count, scan code, extended-key flag, context code, previous key-state flag,
        ///     and transition-state flag. For more information about the lParam parameter, see Keystroke Message Flags. The
        ///     following table describes the bits of this value.
        ///     <para>Bits  Description</para>
        ///     <para>
        ///         0-15	The repeat count. The value is the number of times the keystroke is repeated as a result of the
        ///         user's holding down the key.
        ///     </para>
        ///     <para>16-23	The scan code. The value depends on the OEM.</para>
        ///     <para>
        ///         24	Indicates whether the key is an extended key, such as a function key or a key on the numeric keypad.
        ///         The value is 1 if the key is an extended key; otherwise, it is 0.
        ///     </para>
        ///     <para>25-28	Reserved.</para>
        ///     <para>29	The context code. The value is 1 if the ALT key is down; otherwise, it is 0.</para>
        ///     <para>
        ///         30	The previous key state. The value is 1 if the key is down before the message is sent; it is 0 if
        ///         the key is up.
        ///     </para>
        ///     <para>31	The transition state. The value is 0 if the key is being pressed and 1 if it is being released.</para>
        /// </param>
        /// <returns>
        ///     If code is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If code is greater than or equal to zero, and the hook _procedure did not process the message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_KEYBOARD _hooks will not receive hook notifications and may behave incorrectly as a result.
        ///     If the hook _procedure processed the message, it may return a nonzero value to prevent the system from passing
        ///     the message to the rest of the hook chain or the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_KEYBOARD hook _type and a pointer to
        ///     the hook _procedure in a call to the SetWindowsHookEx function.
        ///     This hook may be called in the context of the thread that installed it. The call is made by sending a message
        ///     to the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644984%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int KeyboardProc(int nCode, int wParam, int lParam)
        {
            if ((nCode == HC_ACTION || nCode == HC_NOREMOVE) && Event != null)
            {
                var ki = new KeyboardHookInfo(nCode, wParam, lParam);
                Event(this, ki);
                if (ki.Handled)
                    return 1;
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        #endregion
    }
}