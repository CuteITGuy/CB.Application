using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains information about a mouse event passed to a WH_MOUSE hook _procedure, MouseProc.
    ///     <para>
    ///         This is an
    ///         extension of the MouseHookStruct structure that includes information about wheel movement or the use of
    ///         the X button.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644969%28v=vs.85%29.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseHookStructEx
    {
        /// <summary>
        ///     The members of a MouseHookStruct structure make up the first part of this structure.
        /// </summary>
        public MouseHookStruct MouseHookStruct;

        /// <summary>
        ///     If the _message is WM_MOUSEWHEEL, the HIWORD of this member is the wheel delta. The LOWORD is undefined and
        ///     reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative
        ///     value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as
        ///     WHEEL_DELTA, which is 120.
        ///     <para>
        ///         If the _message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK,
        ///         WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the HIWORD of _mouseData specifies which X button
        ///         was _pressed or released, and the LOWORD is undefined and reserved. This member can be one or more of the
        ///         following values. Otherwise, _mouseData is not used.
        ///     </para>
        ///     <para>XBUTTON1 = 0x0001 The first X button was _pressed or released.</para>
        ///     <para>XBUTTON2 = 0x0002 The second X button was _pressed or released.</para>
        /// </summary>
        public int MouseData;
    }
}