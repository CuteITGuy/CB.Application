using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Inputs
{
    [ComVisible(true), Flags]
    public enum MouseButtons
    {
        /// <summary>
        ///     No mouse button was pressed.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The left mouse button was pressed.
        /// </summary>
        Left = 0x100000,

        /// <summary>
        ///     The right mouse button was pressed.
        /// </summary>
        Right = 0x200000,

        /// <summary>
        ///     The middle mouse button was pressed.
        /// </summary>
        Middle = 0x400000,

        /// <summary>
        ///     The first XButton was pressed.
        ///     <para>
        ///         With Windows 2000, Microsoft is introducing support for the Microsoft
        ///         IntelliMouse Explorer, which is a mouse with five buttons. The two new mouse buttons (XBUTTON1 and XBUTTON2)
        ///         provide backward/forward navigation.
        ///     </para>
        /// </summary>
        XButton1 = 0x800000,

        /// <summary>
        ///     The second XButton was pressed.
        ///     <para>
        ///         With Windows 2000, Microsoft is introducing support for the Microsoft
        ///         IntelliMouse Explorer, which is a mouse with five buttons. The two new mouse buttons (XBUTTON1 and XBUTTON2)
        ///         provide backward/forward navigation.
        ///     </para>
        /// </summary>
        XButton2 = 0x1000000
    }
}