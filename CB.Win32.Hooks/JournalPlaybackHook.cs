using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    // UNDONE: JournalPlaybackHook
    public class JournalPlaybackHook: HookBase
    {
        #region Fields
        bool _nextMsg = true;
        #endregion


        #region  Constructors & Destructor
        public JournalPlaybackHook()
            : base(HookTypes.JournalPlayback)
        {
            Procedure = new HookProc(JournalPlaybackProc);
        }
        #endregion


        #region Events
        public override event HookEventHandler Event;
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. Typically,
        ///     an application uses this function to play back a series of mouse and keyboard messages recorded previously by
        ///     the JournalRecordProc hook _procedure. As long as a JournalPlaybackProc hook _procedure is installed, regular
        ///     mouse and keyboard input is disabled.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer to this callback function.
        ///         JournalPlaybackProc is a placeholder for the application-defined or library-defined function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the message. If code is less
        ///     than zero, the hook _procedure must pass the message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>
        ///         HC_GETNEXT = 1. The hook _procedure must copy the current mouse or keyboard message to the EVENTMSG
        ///         structure pointed to by the lParam parameter.
        ///     </para>
        ///     <para>
        ///         HC_NOREMOVE = 3. An application has called the PeekMessage function with wRemoveMsg set to PM_NOREMOVE,
        ///         indicating that the message is not removed from the message queue after PeekMessage processing.
        ///     </para>
        ///     <para>
        ///         HC_SKIP = 2. The hook _procedure must prepare to copy the next mouse or keyboard message to the EVENTMSG
        ///         structure pointed to by lParam. Upon receiving the HC_GETNEXT code, the hook _procedure must copy the message
        ///         to the structure.
        ///     </para>
        ///     <para>
        ///         HC_SYSMODALOFF = 5. A system-modal dialog box has been destroyed. The hook _procedure must resume playing
        ///         back the messages.
        ///     </para>
        ///     <para>
        ///         HC_SYSMODALON = 4. A system-modal dialog box is being displayed. Until the dialog box is destroyed, the
        ///         hook _procedure must stop playing back messages.
        ///     </para>
        /// </param>
        /// <param name="wParam">This parameter is not used.</param>
        /// <param name="lParam">
        ///     A pointer to an EVENTMSG structure that represents a message being processed by the hook
        ///     _procedure. This parameter is valid only when the code parameter is HC_GETNEXT.
        /// </param>
        /// <returns>
        ///     To have the system wait before processing the message, the return value must be the amount of time,
        ///     in clock ticks, that the system should wait. (This value can be computed by calculating the difference between
        ///     the time members in the current and previous input messages.) To process the message immediately, the return
        ///     value should be zero. The return value is used only if the hook code is HC_GETNEXT; otherwise, it is ignored.
        /// </returns>
        /// <remarks>
        ///     A JournalPlaybackProc hook _procedure should copy an input message to the lParam parameter. The message
        ///     must have been previously recorded by using a JournalRecordProc hook _procedure, which should not modify the
        ///     message.
        ///     To retrieve the same message over and over, the hook _procedure can be called several times with the code
        ///     parameter set to HC_GETNEXT without an intervening call with code set to HC_SKIP.
        ///     If code is HC_GETNEXT and the return value is greater than zero, the system sleeps for the number of
        ///     milliseconds specified by the return value. When the system continues, it calls the hook _procedure again with
        ///     code set to HC_GETNEXT to retrieve the same message. The return value from this new call to JournalPlaybackProc
        ///     should be zero; otherwise, the system will go back to sleep for the number of milliseconds specified by the
        ///     return value, call JournalPlaybackProc again, and so on. The system will appear to be not responding.
        ///     Unlike most other global hook procedures, the JournalRecordProc and JournalPlaybackProc hook procedures are
        ///     always called in the context of the thread that set the hook.
        ///     After the hook _procedure returns control to the system, the message continues to be processed. If code is
        ///     HC_SKIP, the hook _procedure must prepare to return the next recorded event message on its next call.
        ///     Install the JournalPlaybackProc hook _procedure by specifying the WH_JOURNALPLAYBACK _type and a pointer to the
        ///     hook _procedure in a call to the SetWindowsHookEx function.
        ///     If the user presses CTRL+ESC OR CTRL+ALT+DEL during journal playback, the system stops the playback, unhooks
        ///     the journal playback _procedure, and posts a WM_CANCELJOURNAL message to the journaling application.
        ///     If the hook _procedure returns a message in the range WM_KEYFIRST to WM_KEYLAST, the following conditions
        ///     apply:
        ///     The paramL member of the EVENTMSG structure specifies the virtual key code of the key that was pressed.
        ///     The paramH member of the EVENTMSG structure specifies the scan code.
        ///     There's no way to specify a repeat count. The event is always taken to represent one key event.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644982%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int JournalPlaybackProc(int nCode, int wParam, int lParam)
        {
            if (Event != null)
            {
                switch (nCode)
                {
                    case HC_GETNEXT:
                        if (_nextMsg)
                        {
                            Event(this, new HookInfo(nCode, wParam, lParam));
                            var msg = (EventMessage)Marshal.PtrToStructure((IntPtr)lParam, typeof(EventMessage));
                            _nextMsg = false;
                            return msg.Time;
                        }
                        break;
                    case HC_SKIP:
                        _nextMsg = true;
                        break;
                }
                return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
            }
            return 0;
        }
        #endregion
    }
}