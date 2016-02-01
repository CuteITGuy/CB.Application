namespace CB.Win32.Hooks
{
    /// <summary>
    ///     The _type of hook _procedure to be installed.
    /// </summary>
    public enum HookTypes
    {
        /// <summary>
        ///     WH_MSGFILTER constant
        ///     <para>
        ///         Installs a hook _procedure that monitors messages generated as a result of an input event in a
        ///         dialog box, _message box, menu, or scroll bar. For more information, see the MessageProc hook _procedure.
        ///     </para>
        /// </summary>
        MessageFilter = -1,

        /// <summary>
        ///     WH_JOURNALRECORD constant
        ///     <para>
        ///         Installs a hook _procedure that records input messages posted to the system _message queue. This hook
        ///         is useful for recording macros. For more information, see the JournalRecordProc hook _procedure.
        ///     </para>
        /// </summary>
        JournalRecord = 0,

        /// <summary>
        ///     WH_JOURNALPLAYBACK constant
        ///     <para>
        ///         Installs a hook _procedure that posts messages previously recorded by a WH_JOURNALRECORD hook
        ///         _procedure. For more information, see the JournalPlaybackProc hook _procedure.
        ///     </para>
        /// </summary>
        JournalPlayback = 1,

        /// <summary>
        ///     WH_KEYBOARD constant
        ///     <para>
        ///         Installs a hook _procedure that monitors keystroke messages. For more information, see the
        ///         KeyboardProc hook _procedure.
        ///     </para>
        /// </summary>
        Keyboard = 2,

        /// <summary>
        ///     WH_GETMESSAGE constant
        ///     <para>
        ///         Installs a hook _procedure that monitors messages posted to a _message queue. For more information,
        ///         see the GetMsgProc hook _procedure.
        ///     </para>
        /// </summary>
        GetMessage = 3,

        /// <summary>
        ///     WH_CALLWNDPROC constant
        ///     <para>
        ///         Installs a hook _procedure that monitors messages before the system sends them to the destination
        ///         window _procedure. For more information, see the CallWndProc hook _procedure.
        ///     </para>
        /// </summary>
        CallWndProc = 4,

        /// <summary>
        ///     WH_CBT constant
        ///     <para>
        ///         Installs a hook _procedure that receives notifications useful to a CBT application. For more
        ///         information, see the CBTProc hook _procedure.
        ///     </para>
        /// </summary>
        CBT = 5,

        /// <summary>
        ///     WH_SYSMSGFILTER constant
        ///     <para>
        ///         Installs a hook _procedure that monitors messages generated as a result of an input event in a dialog
        ///         box, _message box, menu, or scroll bar. The hook _procedure monitors these messages for all applications
        ///         in the same desktop as the calling thread. For more information, see the SysMsgProc hook _procedure.
        ///     </para>
        /// </summary>
        SysMessageFilter = 6,

        /// <summary>
        ///     WH_MOUSE constant
        ///     <para>
        ///         Installs a hook _procedure that monitors mouse messages. For more information, see the MouseProc
        ///         hook _procedure.
        ///     </para>
        /// </summary>
        Mouse = 7,

        /// <summary>
        ///     WH_HARDWARE constant
        /// </summary>
        Hardware = 8,

        /// <summary>
        ///     WH_DEBUG constant
        ///     <para>
        ///         Installs a hook _procedure useful for debugging other hook procedures. For more information, see
        ///         the DebugProc hook _procedure.
        ///     </para>
        /// </summary>
        Debug = 9,

        /// <summary>
        ///     WH_SHELL constant
        ///     <para>
        ///         Installs a hook _procedure that receives notifications useful to shell applications. For more
        ///         information, see the ShellProc hook _procedure.
        ///     </para>
        /// </summary>
        Shell = 10,

        /// <summary>
        ///     WH_FOREGROUNDIDLE constant
        ///     <para>
        ///         Installs a hook _procedure that will be called when the application's foreground thread is about to
        ///         become idle. This hook is useful for performing low priority tasks during idle _time. For more information,
        ///         see the ForegroundIdleProc hook _procedure.
        ///     </para>
        /// </summary>
        ForegroundIdle = 11,

        /// <summary>
        ///     WH_CALLWNDPROCRET constant
        ///     <para>
        ///         Installs a hook _procedure that monitors messages after they have been processed by the destination
        ///         window _procedure. For more information, see the CallWndRetProc hook _procedure.
        ///     </para>
        /// </summary>
        CallWndRetProc = 12,

        /// <summary>
        ///     WH_KEYBOARD_LL constant
        ///     <para>
        ///         Installs a hook _procedure that monitors low-level keyboard input events. For more information, see
        ///         the LowLevelKeyboardProc hook _procedure.
        ///     </para>
        /// </summary>
        KeyboardLowLevel = 13,

        /// <summary>
        ///     WH_MOUSE_LL constant
        ///     <para>
        ///         Installs a hook _procedure that monitors low-level mouse input events. For more information, see
        ///         the LowLevelMouseProc hook _procedure.
        ///     </para>
        /// </summary>
        MouseLowLevel = 14
    }
}