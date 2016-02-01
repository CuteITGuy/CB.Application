namespace CB.Win32.Hooks
{
    /// <summary>
    ///     HookProc is a placeholder for an application-defined name.
    /// </summary>
    /// <param name="nCode">
    ///     The nCode parameter is a hook code that the hook _procedure uses to determine the action to perform. The value
    ///     of the hook code depends on the _type of the hook; each _type has its own characteristic set of hook codes.
    /// </param>
    /// <param name="wParam">
    ///     The values of the wParam parameter depend on the hook code, but they typically contain information about a
    ///     _message that was sent or posted.
    /// </param>
    /// <param name="lParam">
    ///     The values of the lParam parameter depend on the hook code, but they typically contain information about a
    ///     _message that was sent or posted.
    /// </param>
    /// <returns>
    ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
    ///     If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx and return
    ///     the value it returns; otherwise, other applications that have installed HOOKPROC _hooks will not receive hook
    ///     notifications and may behave incorrectly as a result. If the hook _procedure does not call CallNextHookEx, the
    ///     return value should be zero.
    /// </returns>
    public delegate int HookProc(int nCode, int wParam, int lParam);
}