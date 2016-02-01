using System;


namespace CB.Win32.Hooks
{
    // UNDONE: JournalRecordHook
    public class JournalRecordHook: HookBase
    {
        #region  Constructors & Destructor
        public JournalRecordHook()
            : base(HookTypes.JournalRecord)
        {
            Procedure = new HookProc(JournalRecordProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     function records messages the system removes from the system message queue. Later, an application can use a
        ///     JournalPlaybackProc hook _procedure to play back the messages.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer
        ///         to this callback function. JournalRecordProc is a placeholder for the application-defined or library-defined
        ///         function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     Specifies how to process the message. If code is less than zero, the hook _procedure must
        ///     pass the message to the CallNextHookEx function without further processing and should return the value returned
        ///     by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>
        ///         HC_ACTION = 0.  The lParam parameter is a pointer to an EVENTMSG structure containing information about
        ///         a message removed from the system queue. The hook _procedure must record the contents of the structure by
        ///         copying them to a buffer or file.
        ///     </para>
        ///     <para>
        ///         HC_SYSMODALOFF = 5.   A system-modal dialog box has been destroyed. The hook _procedure must resume
        ///         recording.
        ///     </para>
        ///     <para>
        ///         HC_SYSMODALON = 4.    A system-modal dialog box is being displayed. Until the dialog box is destroyed,
        ///         the hook _procedure must stop recording.
        ///     </para>
        /// </param>
        /// <param name="wParam">This parameter is not used.</param>
        /// <param name="lParam">A pointer to an EVENTMSG structure that contains the message to be recorded.</param>
        /// <returns>The return value is ignored.</returns>
        /// <remarks>
        ///     A JournalRecordProc hook _procedure must copy but not modify the messages. After the hook _procedure
        ///     returns control to the system, the message continues to be processed.
        ///     Install the JournalRecordProc hook _procedure by specifying the WH_JOURNALRECORD _type and a pointer to the hook
        ///     _procedure in a call to the SetWindowsHookEx function.
        ///     A JournalRecordProc hook _procedure does not need to live in a dynamic-link library. A JournalRecordProc hook
        ///     _procedure can live in the application itself.
        ///     Unlike most other global hook procedures, the JournalRecordProc and JournalPlaybackProc hook procedures are
        ///     always called in the context of the thread that set the hook.
        ///     An application that has installed a JournalRecordProc hook _procedure should watch for the VK_CANCEL virtual
        ///     key code (which is implemented as the CTRL+BREAK key combination on most keyboards). This virtual key code
        ///     should be interpreted by the application as a signal that the user wishes to stop journal recording. The
        ///     application should respond by ending the recording sequence and removing the JournalRecordProc hook _procedure.
        ///     Removal is important. It prevents a journaling application from locking up the system by hanging inside a hook
        ///     _procedure.
        ///     This role as a signal to stop journal recording means that a CTRL+BREAK key combination cannot itself be
        ///     recorded. Since the CTRL+C key combination has no such role as a journaling signal, it can be recorded. There
        ///     are two other key combinations that cannot be recorded: CTRL+ESC and CTRL+ALT+DEL. Those two key combinations
        ///     cause the system to stop all journaling activities (record or playback), remove all journaling _hooks, and post
        ///     a WM_CANCELJOURNAL message to the journaling application.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644983%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int JournalRecordProc(int nCode, int wParam, int lParam)
        {
            if (nCode == HC_ACTION && Event != null)
                Event(this, new JournalRecordHookInfo(nCode, wParam, lParam));
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        #endregion
    }
}