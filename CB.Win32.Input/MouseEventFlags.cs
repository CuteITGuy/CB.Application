using System;


namespace CB.Win32.Inputs
{
    /// <summary>
    ///     A set of bit flags that specify various aspects of mouse motion and button clicks.
    /// </summary>
    [Flags]
    public enum MouseEventFlags: uint
    {
        /// <summary>
        ///     The dx and dy members contain normalized absolute coordinates. If the flag is not set, dx and dy contain
        ///     relative data (the change in position since the last reported position). This flag can be set, or not set,
        ///     regardless of what kind of mouse or other pointing device, if any, is connected to the system. For further
        ///     information about relative mouse motion, see the following Remarks section.
        /// </summary>
        Absolute = 0x8000,

        /// <summary>
        ///     <para>
        ///         The wheel was moved horizontally, if the mouse has a wheel. The amount of movement is specified in
        ///         mouseData.
        ///     </para>
        ///     <para>Windows XP/2000:  This value is not supported.</para>
        /// </summary>
        HorizontalWheel = 0x01000,

        /// <summary>
        ///     Movement occurred.
        /// </summary>
        Move = 0x0001,

        /// <summary>
        ///     <para>
        ///         The WM_MOUSEMOVE messages will not be coalesced. The default behavior is to coalesce WM_MOUSEMOVE messages.
        ///     </para>
        ///     <para>Windows XP/2000:  This value is not supported.</para>
        /// </summary>
        MoveNoCoalesce = 0x2000,

        /// <summary>
        ///     The left button was pressed.
        /// </summary>
        LeftDown = 0x0002,

        /// <summary>
        ///     The left button was released.
        /// </summary>
        LeftUp = 0x0004,

        /// <summary>
        ///     The right button was pressed.
        /// </summary>
        RightDown = 0x0008,

        /// <summary>
        ///     The right button was released.
        /// </summary>
        RightUp = 0x0010,

        /// <summary>
        ///     The middle button was pressed.
        /// </summary>
        MiddleDown = 0x0020,

        /// <summary>
        ///     The middle button was released.
        /// </summary>
        MiddleUp = 0x0040,

        /// <summary>
        ///     Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.
        /// </summary>
        VirtualDesk = 0x4000,

        /// <summary>
        ///     The wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseData.
        /// </summary>
        Wheel = 0x0800,

        /// <summary>
        ///     An X button was pressed.
        /// </summary>
        XDown = 0x0080,

        /// <summary>
        ///     An X button was released.
        /// </summary>
        XUp = 0x0100
    }
}