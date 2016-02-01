using System;
using System.Runtime.InteropServices;
using CB.Win32.Common;


namespace CB.Win32.Threading
{
    /// <summary>
    ///     Contains information about a GUI thread.
    /// </summary>
    public struct GuiThreadInfo
    {
        /// <summary>
        ///     The size of this structure, in bytes. The caller must set this member to sizeof(GUITHREADINFO).
        /// </summary>
        public int Size;

        /// <summary>
        ///     The thread state. This member can be one or more of the GuiThreadState values.
        /// </summary>
        public GuiThreadState Flags;

        /// <summary>
        ///     A handle to the active window within the thread.
        /// </summary>
        public IntPtr ActiveHwnd;

        /// <summary>
        ///     A handle to the window that has the keyboard focus.
        /// </summary>
        public IntPtr FocusHwnd;

        /// <summary>
        ///     A handle to the window that has captured the mouse.
        /// </summary>
        public IntPtr CaptureHwnd;

        /// <summary>
        ///     A handle to the window that owns any active menus.
        /// </summary>
        public IntPtr MenuOwnerHwnd;

        /// <summary>
        ///     A handle to the window in a move or size loop.
        /// </summary>
        public IntPtr MoveSizeHwnd;

        /// <summary>
        ///     A handle to the window that is displaying the caret.
        /// </summary>
        public IntPtr CaretHwnd;

        /// <summary>
        ///     The caret's bounding rectangle, in client coordinates, relative to the window specified by the CaretHwnd member.
        /// </summary>
        public Rect Caret;

        public static GuiThreadInfo Default
        {
            get
            {
                var result = new GuiThreadInfo();
                result.Size = Marshal.SizeOf(result);
                return result;
            }
        }
    }
}