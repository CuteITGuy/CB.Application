using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using CB.Win32.Threading;


namespace CB.Win32.Hooks
{
    public class GlobalHook: IDisposable
    {
        /// <summary>
        ///     The _type of hook _procedure to be installed.
        /// </summary>
        [Flags]
        public enum InstallHookFlags
        {
            /// <summary>
            ///     Uninstall all _hooks
            /// </summary>
            None = 0,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors messages generated as a result of an input event in a
            ///         dialog box, _message box, menu, or scroll bar. For more information, see the MessageProc hook _procedure.
            ///     </para>
            /// </summary>
            MessageFilter = 1,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that records input messages posted to the system _message queue. This hook
            ///         is useful for recording macros. For more information, see the JournalRecordProc hook _procedure.
            ///     </para>
            /// </summary>
            JournalRecord = 2,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that posts messages previously recorded by a WH_JOURNALRECORD hook
            ///         _procedure. For more information, see the JournalPlaybackProc hook _procedure.
            ///     </para>
            /// </summary>
            JournalPlayback = 4,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors keystroke messages. For more information, see the
            ///         KeyboardProc hook _procedure.
            ///     </para>
            /// </summary>
            Keyboard = 8,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors messages posted to a _message queue. For more information,
            ///         see the GetMsgProc hook _procedure.
            ///     </para>
            /// </summary>
            GetMessage = 16,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors messages before the system sends them to the destination
            ///         window _procedure. For more information, see the CallWndProc hook _procedure.
            ///     </para>
            /// </summary>
            CallWndProc = 32,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that receives notifications useful to a CBT application. For more
            ///         information, see the CBTProc hook _procedure.
            ///     </para>
            /// </summary>
            CBT = 64,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors messages generated as a result of an input event in a dialog
            ///         box, _message box, menu, or scroll bar. The hook _procedure monitors these messages for all applications
            ///         in the same desktop as the calling thread. For more information, see the SysMsgProc hook _procedure.
            ///     </para>
            /// </summary>
            SysMessageFilter = 128,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors mouse messages. For more information, see the MouseProc
            ///         hook _procedure.
            ///     </para>
            /// </summary>
            Mouse = 256,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure useful for debugging other hook procedures. For more information, see
            ///         the DebugProc hook _procedure.
            ///     </para>
            /// </summary>
            Debug = 512,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that receives notifications useful to shell applications. For more
            ///         information, see the ShellProc hook _procedure.
            ///     </para>
            /// </summary>
            Shell = 1024,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that will be called when the application's foreground thread is about to
            ///         become idle. This hook is useful for performing low priority tasks during idle _time. For more information,
            ///         see the ForegroundIdleProc hook _procedure.
            ///     </para>
            /// </summary>
            ForegroundIdle = 2048,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors messages after they have been processed by the destination
            ///         window _procedure. For more information, see the CallWndRetProc hook _procedure.
            ///     </para>
            /// </summary>
            CallWndRetProc = 4096,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors low-level keyboard input events. For more information, see
            ///         the LowLevelKeyboardProc hook _procedure.
            ///     </para>
            /// </summary>
            KeyboardLowLevel = 8192,

            /// <summary>
            ///     <para>
            ///         Installs a hook _procedure that monitors low-level mouse input events. For more information, see
            ///         the LowLevelMouseProc hook _procedure.
            ///     </para>
            /// </summary>
            MouseLowLevel = 16384
        }


        #region Fields
        protected const int HC_ACTION = 0, HC_NOREMOVE = 3;

        protected static HookProc _mouseProc, _llmouseProc, _keyboardProc, _llkeyboardProc;
        protected bool _enabled;

        protected IntPtr _mouseHookHandle = IntPtr.Zero, _llmouseHookHandle = IntPtr.Zero,
                         _keyboardHookHandle = IntPtr.Zero, _llkeyboardHookHandle = IntPtr.Zero;
        #endregion


        #region  Constructors & Destructor
        ~GlobalHook()
        {
            Dispose();
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Gets or sets the value indicating whether the hook is able to catch events.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }
        #endregion


        #region Events
        /// <summary>
        ///     Event for keyboard hook
        /// </summary>
        public event KeyboardHookEventHandler KeyboardEvent;

        /// <summary>
        ///     Event for low level keyboard hook
        /// </summary>
        public event KeyboardHookEventHandler KeyboardLLEvent;

        /*/// <summary>
        /// Occurs when the user moves the mouse, presses any mouse button or scrolls the wheel
        /// </summary>
        public event EventHandler<MouseLLHookEventArgs> MouseActive;
        public event EventHandler<KeyboardHookEventArgs> KeyboardActive;*/

        /// <summary>
        ///     Event for mouse hook
        /// </summary>
        public event MouseHookEventHandler MouseEvent;

        /// <summary>
        ///     Event for low level mouse hook
        /// </summary>
        public event MouseHookEventHandler MouseLLEvent;
        #endregion


        #region Methods
        /// <summary>
        ///     Cleans up any resources being used
        /// </summary>
        public void Dispose()
        {
            Stop(false);
        }

        /// <summary>
        ///     Starts the hook
        /// </summary>
        public void Start()
        {
            _mouseProc = new HookProc(MouseProc);
            SetHook(ref _mouseHookHandle, HookTypes.Mouse, _mouseProc, true);
            _keyboardProc = new HookProc(KeyboardProc);
            SetHook(ref _keyboardHookHandle, HookTypes.Keyboard, _keyboardProc, true);
            _llmouseProc = new HookProc(LowLevelMouseProc);
            SetHook(ref _llmouseHookHandle, HookTypes.MouseLowLevel, _llmouseProc, false);
            _llkeyboardProc = new HookProc(LowLevelKeyboardProc);
            SetHook(ref _llkeyboardHookHandle, HookTypes.KeyboardLowLevel, _llkeyboardProc, false);
            _enabled = true;
            System.GC.ReRegisterForFinalize(this);
        }

        /// <summary>
        ///     Stops the hook and cleans up any resources being used
        /// </summary>
        public void Stop()
        {
            Stop(true);
        }
        #endregion


        #region Implementation
        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The system
        ///     calls this function whenever an application calls the GetMessage or PeekMessage function and there is a
        ///     keyboard _message (WM_KEYUP or WM_KEYDOWN) to be processed.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer to this
        ///         callback function. KeyboardProc is a placeholder for the application-defined or library-defined function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the _message. If code is less
        ///     than zero, the hook _procedure must pass the _message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.    The wParam and lParam parameters contain information about a keystroke _message.</para>
        ///     <para>
        ///         HC_NOREMOVE = 3.  The wParam and lParam parameters contain information about a keystroke _message, and
        ///         the keystroke _message has not been removed from the _message queue. (An application called the PeekMessage
        ///         function, specifying the PM_NOREMOVE flag.)
        ///     </para>
        /// </param>
        /// <param name="wParam">The virtual-key code of the key that generated the keystroke _message.</param>
        /// <param name="lParam">
        ///     The repeat count, scan code, extended-key flag, context code, previous key-state flag,
        ///     and transition-state flag. For more information about the lParam parameter, see Keystroke Message Flags. The
        ///     following table describes the bits of this value.
        ///     <para>Bits  Description</para>
        ///     <para>
        ///         0-15	The repeat count. The value is the number of times the keystroke is repeated as a result of the
        ///         user's holding down the key.
        ///     </para>
        ///     <para>16-23	The scan code. The value depends on the OEM.</para>
        ///     <para>
        ///         24	Indicates whether the key is an extended key, such as a function key or a key on the numeric keypad.
        ///         The value is 1 if the key is an extended key; otherwise, it is 0.
        ///     </para>
        ///     <para>25-28	Reserved.</para>
        ///     <para>29	The context code. The value is 1 if the ALT key is down; otherwise, it is 0.</para>
        ///     <para>
        ///         30	The previous key state. The value is 1 if the key is down before the _message is sent; it is 0 if
        ///         the key is up.
        ///     </para>
        ///     <para>31	The transition state. The value is 0 if the key is being _pressed and 1 if it is being released.</para>
        /// </param>
        /// <returns>
        ///     If code is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If code is greater than or equal to zero, and the hook _procedure did not process the _message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_KEYBOARD _hooks will not receive hook notifications and may behave incorrectly as a result.
        ///     If the hook _procedure processed the _message, it may return a nonzero value to prevent the system from passing
        ///     the _message to the rest of the hook chain or the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_KEYBOARD hook _type and a pointer to
        ///     the hook _procedure in a call to the SetWindowsHookEx function.
        ///     This hook may be called in the context of the thread that installed it. The call is made by sending a _message
        ///     to the thread that installed the hook. Therefore, the thread that installed the hook must have a _message loop.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644984%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int KeyboardProc(int nCode, int wParam, int lParam)
        {
            if ((nCode == HC_ACTION || nCode == HC_NOREMOVE))
            {
                KeyboardEvent?.Invoke(this, new KeyboardHookInfo(nCode, wParam, lParam));
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The system
        ///     calls this function every _time a new keyboard input event is about to be posted into a thread input queue.
        ///     The HOOKPROC _type defines a pointer to this callback function. LowLevelKeyboardProc is a placeholder for the
        ///     application-defined or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the _message. If nCode is less
        ///     than zero, the hook _procedure must pass the _message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0. The wParam and lParam parameters contain information about a keyboard _message.</para>
        /// </param>
        /// <param name="wParam">
        ///     The identifier of the keyboard _message. This parameter can be one of the following messages:
        ///     WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP.
        /// </param>
        /// <param name="lParam">A pointer to a KBDLLHOOKSTRUCT structure.</param>
        /// <returns>
        ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, and the hook _procedure did not process the _message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_KEYBOARD_LL _hooks will not receive hook notifications and may behave incorrectly as a result.
        ///     If the hook _procedure processed the _message, it may return a nonzero value to prevent the system from passing
        ///     the _message to the rest of the hook chain or the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_KEYBOARD_LL hook _type and a pointer
        ///     to the hook _procedure in a call to the SetWindowsHookEx function.
        ///     This hook is called in the context of the thread that installed it. The call is made by sending a _message to
        ///     the thread that installed the hook. Therefore, the thread that installed the hook must have a _message loop.
        ///     The keyboard input can come from the local keyboard driver or from calls to the keybd_event function. If the
        ///     input comes from a call to keybd_event, the input was "injected". However, the WH_KEYBOARD_LL hook is not injected
        ///     into another process. Instead, the context switches back to the process that installed the hook and it is
        ///     called in its original context. Then the context switches back to the application that generated the event.
        ///     The hook _procedure should process a _message in less _time than the data entry specified in the
        ///     LowLevelHooksTimeout value in the following registry key:
        ///     HKEY_CURRENT_USER\Control Panel\Desktop
        ///     The value is in milliseconds. If the hook _procedure times out, the system passes the _message to the next hook.
        ///     However, on Windows 7 and later, the hook is silently removed without being called. There is no way for the
        ///     application to know whether the hook is removed.
        ///     Note  Debug _hooks cannot track this _type of low level keyboard _hooks. If the application must use low level
        ///     _hooks, it should run the _hooks on a dedicated thread that passes the work off to a worker thread and then
        ///     immediately returns. In most cases where the application needs to use low level _hooks, it should monitor raw
        ///     input instead. This is because raw input can asynchronously monitor mouse and keyboard messages that are
        ///     targeted for other threads more effectively than low level _hooks can. For more information on raw input,
        ///     see Raw Input.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644985%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int LowLevelKeyboardProc(int nCode, int wParam, int lParam)
        {
            if (nCode == HC_ACTION)
            {
                //KeyboardLLHookStruct info = (KeyboardLLHookStruct)Marshal.PtrToStructure((IntPtr)lParam, typeof(KeyboardLLHookStruct));
                KeyboardLLEvent?.Invoke(this, new KeyboardLLHookInfo(nCode, wParam, lParam));
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     system calls this function every _time a new mouse input event is about to be posted into a thread input queue.
        ///     The HOOKPROC _type defines a pointer to this callback function. LowLevelMouseProc is a placeholder for
        ///     the application-defined or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     A code the hook _procedure uses to determine how to process the _message. If nCode is less
        ///     than zero, the hook _procedure must pass the _message to the CallNextHookEx function without further processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.The wParam and lParam parameters contain information about a mouse _message.</para>
        /// </param>
        /// <param name="wParam">
        ///     The identifier of the mouse _message. This parameter can be one of the following messages:
        ///     WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE, WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.
        /// </param>
        /// <param name="lParam">A pointer to an MSLLHOOKSTRUCT structure.</param>
        /// <returns>
        ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, and the hook _procedure did not process the _message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_MOUSE_LL _hooks will not receive hook notifications and may behave incorrectly as a result.
        ///     If the hook _procedure processed the _message, it may return a nonzero value to prevent the system from passing
        ///     the _message to the rest of the hook chain or the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_MOUSE_LL hook _type and a pointer to
        ///     the hook _procedure in a call to the SetWindowsHookEx function.
        ///     This hook is called in the context of the thread that installed it. The call is made by sending a _message to
        ///     the thread that installed the hook. Therefore, the thread that installed the hook must have a _message loop.
        ///     The mouse input can come from the local mouse driver or from calls to the mouse_event function. If the input
        ///     comes from a call to mouse_event, the input was "injected". However, the WH_MOUSE_LL hook is not injected
        ///     into another process. Instead, the context switches back to the process that installed the hook and it is
        ///     called in its original context. Then the context switches back to the application that generated the event.
        ///     The hook _procedure should process a _message in less _time than the data entry specified in the
        ///     LowLevelHooksTimeout value in the following registry key:
        ///     HKEY_CURRENT_USER\Control Panel\Desktop
        ///     The value is in milliseconds. If the hook _procedure times out, the system passes the _message to the next hook.
        ///     However, on Windows 7 and later, the hook is silently removed without being called. There is no way for the
        ///     application to know whether the hook is removed.
        ///     Note  Debug _hooks cannot track this _type of low level mouse _hooks. If the application must use low level
        ///     _hooks, it should run the _hooks on a dedicated thread that passes the work off to a worker thread and then
        ///     immediately returns. In most cases where the application needs to use low level _hooks, it should monitor
        ///     raw input instead. This is because raw input can asynchronously monitor mouse and keyboard messages that
        ///     are targeted for other threads more effectively than low level _hooks can. For more information on raw input,
        ///     see Raw Input.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644986%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int LowLevelMouseProc(int nCode, int wParam, int lParam)
        {
            if (nCode == HC_ACTION)
            {
                //MouseLLHookStruct info = (MouseLLHookStruct)Marshal.PtrToStructure((IntPtr)lParam, typeof(MouseLLHookStruct));
                MouseLLEvent?.Invoke(this, new MouseLLHookInfo(nCode, wParam, lParam));
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        ///     An application-defined or library-defined callback function used with the SetWindowsHookEx function. The
        ///     system calls this function whenever an application calls the GetMessage or PeekMessage function and there
        ///     is a mouse _message to be processed.
        ///     <para>
        ///         The HOOKPROC _type defines a pointer to this callback function.
        ///         MouseProc is a placeholder for the application-defined or library-defined function name.
        ///     </para>
        /// </summary>
        /// <param name="nCode">
        ///     A code that the hook _procedure uses to determine how to process the _message. If nCode is
        ///     less than zero, the hook _procedure must pass the _message to the CallNextHookEx function without further
        ///     processing
        ///     and should return the value returned by CallNextHookEx. This parameter can be one of the following values.
        ///     <para>HC_ACTION = 0.    The wParam and lParam parameters contain information about a mouse _message.</para>
        ///     <para>
        ///         HC_NOREMOVE = 3.  The wParam and lParam parameters contain information about a mouse _message, and the
        ///         mouse _message has not been removed from the _message queue. (An application called the PeekMessage function,
        ///         specifying the PM_NOREMOVE flag.)
        ///     </para>
        /// </param>
        /// <param name="wParam">The identifier of the mouse _message.</param>
        /// <param name="lParam">A pointer to a MOUSEHOOKSTRUCT structure.</param>
        /// <returns>
        ///     If nCode is less than zero, the hook _procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, and the hook _procedure did not process the _message, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that
        ///     have installed WH_MOUSE _hooks will not receive hook notifications and may behave incorrectly as a result. If
        ///     the hook _procedure processed the _message, it may return a nonzero value to prevent the system from passing the
        ///     _message to the target window _procedure.
        /// </returns>
        /// <remarks>
        ///     An application installs the hook _procedure by specifying the WH_MOUSE hook _type and a pointer to the
        ///     hook _procedure in a call to the SetWindowsHookEx function.
        ///     The hook _procedure must not install a WH_JOURNALPLAYBACK callback function.
        ///     This hook may be called in the context of the thread that installed it. The call is made by sending a _message
        ///     to the thread that installed the hook. Therefore, the thread that installed the hook must have a _message loop.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644988%28v=vs.85%29.aspx
        /// </remarks>
        protected virtual int MouseProc(int nCode, int wParam, int lParam)
        {
            if ((nCode == HC_ACTION || nCode == HC_NOREMOVE))
            {
                //MouseHookStructEx info = (MouseHookStructEx)Marshal.PtrToStructure((IntPtr)lParam, typeof(MouseHookStructEx));
                MouseEvent?.Invoke(this, new MouseHookInfo(nCode, wParam, lParam));
            }
            return Hook.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private static void SetHook(ref IntPtr hookHandle, HookTypes type, HookProc proc, bool local)
        {
            if (hookHandle != IntPtr.Zero) return;

            hookHandle = Hook.SetWindowsHookEx(type, proc, IntPtr.Zero,
                local ? ProcessThread.GetCurrentThreadId() : 0);
            if (hookHandle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void Stop(bool throwException)
        {
            Unhook(_mouseHookHandle, throwException);
            Unhook(_keyboardHookHandle, throwException);
            Unhook(_llmouseHookHandle, throwException);
            Unhook(_llkeyboardHookHandle, throwException);
            _enabled = false;
            System.GC.SuppressFinalize(this);
        }

        private static void Unhook(IntPtr hookHandle, bool throwException)
        {
            if (hookHandle != IntPtr.Zero && !Hook.UnhookWindowsHookEx(hookHandle) && throwException)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        #endregion
    }
}