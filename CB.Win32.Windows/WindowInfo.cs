using System.Runtime.InteropServices;
using CB.Win32.Common;


namespace CB.Win32.Windows
{
    /// <summary>
    ///     Contains window information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowInfo
    {
        /// <summary>
        ///     The size of the structure, in bytes. The caller must set this member to sizeof(WINDOWINFO).
        /// </summary>
        public uint Size;

        /// <summary>
        ///     The coordinates of the window.
        /// </summary>
        public Rect WindowRect;

        /// <summary>
        ///     The coordinates of the client area.
        /// </summary>
        public Rect ClientRect;

        /// <summary>
        ///     The window styles.
        /// </summary>
        public WindowStyles WindowStyle;

        /// <summary>
        ///     The extended window styles.
        /// </summary>
        public WindowStylesEx ExtendedStyle;

        /// <summary>
        ///     The window status. If this member is WS_ACTIVECAPTION (0x0001), the window is active.
        ///     Otherwise, this member is zero.
        /// </summary>
        public uint WindowStatus;

        /// <summary>
        ///     The width of the window border, in pixels.
        /// </summary>
        public uint BorderWidth;

        /// <summary>
        ///     The height of the window border, in pixels.
        /// </summary>
        public uint BorderHeight;

        /// <summary>
        ///     The window class atom
        /// </summary>
        public ushort AtomWindowType;

        /// <summary>
        ///     The Windows version of the application that created the window.
        /// </summary>
        public ushort CreatorVersion;

        /*public WindowInfo(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            Size = (UInt32)(Marshal.SizeOf(typeof(WindowInfo)));
        }*/

        public static WindowInfo Default
        {
            get
            {
                var result = new WindowInfo { Size = (uint)Marshal.SizeOf(typeof(WindowInfo)) };
                return result;
            }
        }
    }
}