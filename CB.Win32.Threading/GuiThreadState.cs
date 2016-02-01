using System;


namespace CB.Win32.Threading
{
    /// <summary>
    ///     The thread state.
    /// </summary>
    [Flags]
    public enum GuiThreadState
    {
        /// <summary>
        ///     The caret's blink state. This bit is set if the caret is visible.
        /// </summary>
        CaretBlinking = 0x00000001,

        /// <summary>
        ///     The thread's menu state. This bit is set if the thread is in menu mode.
        /// </summary>
        InMenuMode = 0x00000004,

        /// <summary>
        ///     The thread's move state. This bit is set if the thread is in a move or size loop.
        /// </summary>
        InMoveSize = 0x00000002,

        /// <summary>
        ///     The thread's pop-up menu state. This bit is set if the thread has an active pop-up menu.
        /// </summary>
        PopupMenuMode = 0x00000010,

        /// <summary>
        ///     The thread's system menu state. This bit is set if the thread is in a system menu mode.
        /// </summary>
        SystemMenuMode = 0x00000008
    }
}