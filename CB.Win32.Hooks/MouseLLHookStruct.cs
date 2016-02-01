using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644970%28v=vs.85%29.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseLLHookStruct
    {
        /// <summary>
        ///     Specifies a Point structure that contains the x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public Point Location;

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

        /// <summary>
        ///     The event-injected flags. An application can use the following values to test the flags. Testing
        ///     LLMHF_INJECTED (bit 0) will tell you whether the event was injected. If it was, then testing
        ///     LLMHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process
        ///     running at lower integrity level.
        ///     <para>LLMHF_INJECTED = 0x00000001. Test the event-injected (from any process) flag.</para>
        ///     <para>
        ///         LLMHF_LOWER_IL_INJECTED = 0x00000002. Test the event-injected (from a process running at lower
        ///         integrity level) flag.
        ///     </para>
        /// </summary>
        public int EventInjectedFlags;

        /// <summary>
        ///     Specifies the _time stamp for this _message.
        /// </summary>
        public int Time;

        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        public IntPtr ExtraInfo;
    }
}