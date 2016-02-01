using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Inputs
{
    /// <summary>
    ///     The keyboard types
    /// </summary>
    public enum KeyboardTypes
    {
        /// <summary>
        ///     IBM PC/XT or compatible (83-key) keyboard
        /// </summary>
        Pcxt = 1,

        /// <summary>
        ///     Olivetti "ICO" (102-key) keyboard
        /// </summary>
        Olivetti = 2,

        /// <summary>
        ///     IBM PC/AT (84-key) or similar keyboard
        /// </summary>
        Pcat = 3,

        /// <summary>
        ///     IBM enhanced (101- or 102-key) keyboard
        /// </summary>
        Enhanced = 4,

        /// <summary>
        ///     Nokia 1050 and similar keyboards
        /// </summary>
        Nokia1050 = 5,

        /// <summary>
        ///     Nokia 9140 and similar keyboards
        /// </summary>
        Nokia9140 = 6,

        /// <summary>
        ///     Japanese keyboard
        /// </summary>
        Japanese = 7
    }

    /// <summary>
    ///     Used by SendInput to store information for synthesizing input events such as keystrokes, mouse movement,
    ///     and mouse clicks.
    /// </summary>
    /// <remarks>
    ///     Keyboard supports nonkeyboard input methods, such as handwriting recognition or voice recognition, as if
    ///     it were text input by using the KEYEVENTF_UNICODE flag. For more information, see the remarks section of
    ///     KEYBDINPUT.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        /// <summary>
        ///     The type of the input event.
        /// </summary>
        public InputTypes type;

        public InputUnion U;

        public static int Size
        {
            get { return Marshal.SizeOf(typeof(Input)); }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        /// <summary>
        ///     The information about a simulated mouse event.
        /// </summary>
        [FieldOffset(0)]
        public MouseInput mi;

        /// <summary>
        ///     The information about a simulated keyboard event.
        /// </summary>
        [FieldOffset(0)]
        public KeyboardInput ki;

        /// <summary>
        ///     The information about a simulated hardware event.
        /// </summary>
        [FieldOffset(0)]
        public HardwareInput hi;
    }

    /// <summary>
    ///     Contains information about a simulated keyboard event.
    /// </summary>
    /// <remarks>
    ///     Keyboard supports nonkeyboard-input methods—such as handwriting recognition or voice recognition—as
    ///     if it were text input by using the KEYEVENTF_UNICODE flag. If KEYEVENTF_UNICODE is specified, SendInput sends
    ///     a WM_KEYDOWN or WM_KEYUP message to the foreground thread's message queue with wParam equal to VK_PACKET.
    ///     Once GetMessage or PeekMessage obtains this message, passing the message to TranslateMessage posts a WM_CHAR
    ///     message with the Unicode character originally specified by wScan. This Unicode character will automatically
    ///     be converted to the appropriate ANSI value if it is posted to an ANSI window.
    ///     Set the KEYEVENTF_SCANCODE flag to define keyboard input in terms of the scan code. This is useful to simulate
    ///     a physical keystroke regardless of which keyboard is currently being used. The virtual key value of a key may
    ///     alter depending on the current keyboard layout or what other keys were pressed, but the scan code will always
    ///     be the same.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        /// <summary>
        ///     A virtual-key code. The code must be a value in the range 1 to 254. If the dwFlags member specifies
        ///     KEYEVENTF_UNICODE, wVk must be 0.
        /// </summary>
        public VirtualKeys wVk;

        /// <summary>
        ///     A hardware scan code for the key. If dwFlags specifies KEYEVENTF_UNICODE, wScan specifies a Unicode
        ///     character which is to be sent to the foreground application.
        /// </summary>
        public ScanCodes wScan;

        /// <summary>
        ///     Specifies various aspects of a keystroke.
        /// </summary>
        public KeyEventFlags dwFlags;

        /// <summary>
        ///     The time stamp for the event, in milliseconds. If this parameter is zero, the system will provide its
        ///     own time stamp.
        /// </summary>
        public int time;

        /// <summary>
        ///     An additional value associated with the keystroke. Use the GetMessageExtraInfo function to obtain this information.
        /// </summary>
        public UIntPtr dwExtraInfo;
    }

    public struct KeyState
    {
        public bool Pressed { get; set; }

        public bool Toggled { get; set; }

        public KeyState(short state)
        {
            Pressed = (state & 0x80) == 0x80;
            Toggled = (state & 0x01) == 0x01;
        }

        public KeyState(byte state)
        {
            Pressed = (state & 0x80) == 0x80;
            Toggled = (state & 0x01) == 0x01;
        }

        public byte ToByte()
        {
            byte result = 0;
            if (Pressed)
                result |= 0x80;
            if (Toggled)
                result |= 0x01;
            return result;
        }
    }

    /// <summary>
    ///     Contains information about a simulated mouse event.
    /// </summary>
    /// <remarks>
    ///     If the mouse has moved, indicated by MOUSEEVENTF_MOVE, dxand dy specify information about that movement. The
    ///     information is specified as absolute or relative integer values.
    ///     If MOUSEEVENTF_ABSOLUTE value is specified, dx and dy contain normalized absolute coordinates between 0 and
    ///     65,535. The event procedure maps these coordinates onto the display surface. Coordinate (0,0) maps onto the
    ///     upper-left corner of the display surface; coordinate (65535,65535) maps onto the lower-right corner. In a
    ///     multimonitor system, the coordinates map to the primary monitor.
    ///     If MOUSEEVENTF_VIRTUALDESK is specified, the coordinates map to the entire virtual desktop.
    ///     If the MOUSEEVENTF_ABSOLUTE value is not specified, dxand dy specify movement relative to the previous mouse
    ///     event (the last reported position). Positive values mean the mouse moved right (or down); negative values mean
    ///     the mouse moved left (or up).
    ///     Relative mouse motion is subject to the effects of the mouse speed and the two-mouse threshold values. A user
    ///     sets these three values with the Pointer Speed slider of the Control Panel's Mouse Properties sheet. You can
    ///     obtain and set these values using the SystemParametersInfo function.
    ///     The system applies two tests to the specified relative mouse movement. If the specified distance along either
    ///     the x or y axis is greater than the first mouse threshold value, and the mouse speed is not zero, the system
    ///     doubles the distance. If the specified distance along either the x or y axis is greater than the second mouse
    ///     threshold value, and the mouse speed is equal to two, the system doubles the distance that resulted from applying
    ///     the first threshold test. It is thus possible for the system to multiply specified relative mouse movement along
    ///     the x or y axis by up to four times.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        /// <summary>
        ///     The absolute position of the mouse, or the amount of motion since the last mouse event was generated,
        ///     depending on the value of the dwFlags member. Absolute data is specified as the x coordinate of the mouse;
        ///     relative data is specified as the number of pixels moved.
        /// </summary>
        public int dx;

        /// <summary>
        ///     The absolute position of the mouse, or the amount of motion since the last mouse event was generated,
        ///     depending on the value of the dwFlags member. Absolute data is specified as the y coordinate of the mouse;
        ///     relative data is specified as the number of pixels moved.
        /// </summary>
        public int dy;

        /// <summary>
        ///     <para>
        ///         If dwFlags contains MOUSEEVENTF_WHEEL, then mouseData specifies the amount of wheel movement.
        ///         A positive value indicates that the wheel was rotated forward, away from the user; a negative value
        ///         indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA,
        ///         which is 120.
        ///     </para>
        ///     <para>
        ///         Windows Vista: If dwFlags contains MOUSEEVENTF_HWHEEL, then dwData specifies the amount of wheel
        ///         movement. A positive value indicates that the wheel was rotated to the right; a negative value indicates
        ///         that the wheel was rotated to the left. One wheel click is defined as WHEEL_DELTA, which is 120.
        ///     </para>
        ///     <para>
        ///         If dwFlags does not contain MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then mouseData
        ///         should be zero.
        ///     </para>
        ///     <para>
        ///         If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then mouseData specifies which X buttons
        ///         were pressed or released. This value may be any combination of the following flags.
        ///     </para>
        ///     <para>XBUTTON1 = 0x0001. Set if the first X button is pressed or released.</para>
        ///     <para>XBUTTON2 = 0x0002. Set if the second X button is pressed or released.</para>
        /// </summary>
        public int mouseData;

        /// <summary>
        ///     <para>
        ///         A set of bit flags that specify various aspects of mouse motion and button clicks. The bits in
        ///         this member can be any reasonable combination of the following values.
        ///     </para>
        ///     <para>
        ///         The bit flags that specify mouse button status are set to indicate changes in status, not ongoing
        ///         conditions. For example, if the left mouse button is pressed and held down, MOUSEEVENTF_LEFTDOWN is set
        ///         when the left button is first pressed, but not for subsequent motions. Similarly, MOUSEEVENTF_LEFTUP is
        ///         set only when the button is first released.
        ///     </para>
        ///     <para>
        ///         You cannot specify both the MOUSEEVENTF_WHEEL flag and either MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP
        ///         flags simultaneously in the dwFlags parameter, because they both require use of the mouseData field.
        ///     </para>
        /// </summary>
        public MouseEventFlags dwFlags;

        /// <summary>
        ///     The time stamp for the event, in milliseconds. If this parameter is 0, the system will provide its own
        ///     time stamp.
        /// </summary>
        public uint time;

        /// <summary>
        ///     An additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain
        ///     this extra information.
        /// </summary>
        public UIntPtr dwExtraInfo;
    }
}