using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CB.Win32.Inputs
{
    public static class Keyboard
    {
        #region Import
        /// <summary>
        ///     Blocks keyboard and mouse input events from reaching applications.
        /// </summary>
        /// <param name="fBlockIt">
        ///     The function's purpose. If this parameter is TRUE, keyboard and mouse input events
        ///     are blocked. If this parameter is FALSE, keyboard and mouse events are unblocked. Note that only the thread
        ///     that blocked input can successfully unblock input.
        /// </param>
        /// <returns>
        ///     <para>If the function succeeds, the return value is nonzero.</para>
        ///     <para>
        ///         If input is already blocked, the return value is zero. To get extended error information,
        ///         call GetLastError.
        ///     </para>
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool BlockInput(bool fBlockIt);

        /// <summary>
        ///     Enables or disables mouse and keyboard input to the specified window or control. When input is disabled,
        ///     the window does not receive input such as mouse clicks and key presses. When input is enabled, the window
        ///     receives all input.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be enabled or disabled.</param>
        /// <param name="bEnable">
        ///     Indicates whether to enable or disable the window. If this parameter is TRUE, the
        ///     window is enabled. If the parameter is FALSE, the window is disabled.
        /// </param>
        /// <returns>
        ///     <para>If the window was previously disabled, the return value is nonzero.</para>
        ///     <para>If the window was not previously disabled, the return value is zero.</para>
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        /// <summary>
        ///     Retrieves the window handle to the active window attached to the calling thread's message queue.
        /// </summary>
        /// <returns>
        ///     The return value is the handle to the active window attached to the calling thread's message queue.
        ///     Otherwise, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        ///     Retrieves the handle to the window that has the keyboard focus, if the window is attached to the calling
        ///     thread's message queue.
        /// </summary>
        /// <returns>
        ///     The return value is the handle to the window with the keyboard focus. If the calling thread's
        ///     message queue does not have an associated window with the keyboard focus, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        /// <summary>
        ///     Copies the status of the 256 virtual keys to the specified buffer.
        /// </summary>
        /// <param name="lpKeyState">The 256-byte array that receives the status data for each virtual key.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails, the return value
        ///     is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     An application can call this function to retrieve the current status of all the virtual keys. The status
        ///     changes as a thread removes keyboard messages from its message queue. The status does not change as keyboard
        ///     messages are posted to the thread's message queue, nor does it change as keyboard messages are posted to
        ///     or retrieved from message queues of other threads. (Exception: Threads that are connected through
        ///     AttachThreadInput share the same keyboard state.)
        ///     When the function returns, each member of the array pointed to by the lpKeyState parameter contains status
        ///     data for a virtual key. If the high-order bit is 1, the key is down; otherwise, it is up. If the key is a
        ///     toggle key, for example CAPS LOCK, then the low-order bit is 1 when the key is toggled and is 0 if the key
        ///     is untoggled. The low-order bit is meaningless for non-toggle keys. A toggle key is said to be toggled when
        ///     it is turned on. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled,
        ///     and off when the key is untoggled.
        ///     To retrieve status information for an individual key, use the GetKeyState function. To retrieve the current
        ///     state for an individual key regardless of whether the corresponding keyboard message has been retrieved from
        ///     the message queue, use the GetAsyncKeyState function.
        ///     An application can use the virtual-key code constants Keys.ShiftKey, Keys.ControlKey and Keys.Menu as indices
        ///     into the array pointed to by lpKeyState. This gives the status of the SHIFT, CTRL, or ALT keys without
        ///     distinguishing between left and right. An application can also use the following virtual-key code constants
        ///     as indices to distinguish between the left and right instances of those keys: Keys.LShiftKey, Keys.RShiftKey,
        ///     Keys.LControlKey, Keys.RControlKey, Keys.LMenu, Keys.RMenu
        ///     These left- and right-distinguishing constants are available to an application only through the
        ///     GetKeyboardState, SetKeyboardState, GetAsyncKeyState, GetKeyState, and MapVirtualKey functions.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        ///     Retrieves information about the current keyboard.
        /// </summary>
        /// <param name="nTypeFlag">The type of keyboard information to be retrieved.</param>
        /// <returns>
        ///     If the function succeeds, the return value specifies the requested information. If the function
        ///     fails and nTypeFlag is not one, the return value is zero; zero is a valid return value when nTypeFlag is
        ///     one (keyboard subtype). To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int GetKeyboardType(KeyboardTypeFlags nTypeFlag);

        /// <summary>
        ///     Retrieves the status of the specified virtual key. The status specifies whether the key is up, down,
        ///     or toggled (on, off—alternating each time the key is pressed).
        /// </summary>
        /// <param name="nVirtKey">
        ///     <para>
        ///         A virtual key. If the desired virtual key is a letter or digit (A through Z,
        ///         a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys,
        ///         it must be a virtual-key code.
        ///     </para>
        ///     <para>
        ///         If a non-English keyboard layout is used, virtual keys with values in the range ASCII A through Z and
        ///         0 through 9 are used to specify most of the character keys. For example, for the German keyboard layout, the
        ///         virtual key of value ASCII O (0x4F) refers to the "o" key, whereas VK_OEM_1 refers to the "o with umlaut" key.
        ///     </para>
        /// </param>
        /// <returns>
        ///     The return value specifies the status of the specified virtual key, as follows:
        ///     If the high-order bit is 1, the key is down; otherwise, it is up.
        ///     If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on.
        ///     The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard
        ///     will be on when the key is toggled, and off when the key is untoggled.
        /// </returns>
        /// <remarks>
        ///     The key status returned from this function changes as a thread reads key messages from its message queue. The
        ///     status does not reflect the interrupt-level state associated with the hardware. Use the GetAsyncKeyState
        ///     function to retrieve that information.
        ///     An application calls GetKeyState in response to a keyboard-input message. This function retrieves the state
        ///     of the key when the input message was generated.
        ///     To retrieve state information for all the virtual keys, use the GetKeyboardState function.
        ///     An application can use the virtual-key code constants Keys.ShiftKey, Keys.ControlKey and Keys.Menu as indices
        ///     into the array pointed to by lpKeyState. This gives the status of the SHIFT, CTRL, or ALT keys without
        ///     distinguishing between left and right. An application can also use the following virtual-key code constants
        ///     as indices to distinguish between the left and right instances of those keys: Keys.LShiftKey, Keys.RShiftKey,
        ///     Keys.LControlKey, Keys.RControlKey, Keys.LMenu, Keys.RMenu
        ///     These left- and right-distinguishing constants are available to an application only through the GetKeyboardState,
        ///     SetKeyboardState, GetAsyncKeyState, GetKeyState, and MapVirtualKey functions.
        /// </remarks>
        [DllImport("USER32.dll")]
        public static extern short GetKeyState(Keys nVirtKey);

        /// <summary>
        ///     Determines whether the specified window is enabled for mouse and keyboard input.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>
        ///     <para>If the window is enabled, the return value is nonzero.</para>
        ///     <para>If the window is not enabled, the return value is zero.</para>
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        /// <summary>
        ///     Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs">The number of structures in the pInputs array.</param>
        /// <param name="pInputs">
        ///     An array of INPUT structures. Each structure represents an event to be inserted
        ///     into the keyboard or mouse input stream.
        /// </param>
        /// <param name="cbSize">
        ///     The size, in bytes, of an INPUT structure. If cbSize is not the size of an INPUT
        ///     structure, the function fails.
        /// </param>
        /// <returns>
        ///     The function returns the number of events that it successfully inserted into the keyboard or
        ///     mouse input stream. If the function returns zero, the input was already blocked by another thread. To
        ///     get extended error information, call GetLastError.
        ///     This function fails when it is blocked by UIPI. Note that neither GetLastError nor the return value
        ///     will indicate the failure was caused by UIPI blocking.
        /// </returns>
        /// <remarks>
        ///     This function is subject to UIPI. Applications are permitted to inject input only into applications
        ///     that are at an equal or lesser integrity level.
        ///     The SendInput function inserts the events in the INPUT structures serially into the keyboard or mouse
        ///     input stream. These events are not interspersed with other keyboard or mouse input events inserted
        ///     either by the user (with the keyboard or mouse) or by calls to keybd_event, mouse_event, or other
        ///     calls to SendInput.
        ///     This function does not reset the keyboard's current state. Any keys that are already pressed when the
        ///     function is called might interfere with the events that this function generates. To avoid this problem,
        ///     check the keyboard's state with the GetAsyncKeyState function and correct as necessary.
        ///     Because the touch keyboard uses the surrogate macros defined in winnls.h to send input to the system,
        ///     a listener on the keyboard event hook must decode input originating from the touch keyboard. For more
        ///     information, see Surrogates and Supplementary Characters.
        ///     An accessibility application can use SendInput to inject keystrokes corresponding to application launch
        ///     shortcut keys that are handled by the shell. This functionality is not guaranteed to work for other types
        ///     of applications.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern int SendInput(int nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs,
            int cbSize);

        /// <summary>
        ///     Activates a window. The window must be attached to the calling thread's message queue.
        /// </summary>
        /// <param name="hWnd">A handle to the top-level window to be activated.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the window that was previously active.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The SetActiveWindow function activates a window, but not if the application is in the background. The window
        ///     will be brought into the foreground (top of Z-Order) if its application is in the foreground when the system
        ///     activates the window.
        ///     If the window identified by the hWnd parameter was created by the calling thread, the active window status of
        ///     the calling thread is set to hWnd. Otherwise, the active window status of the calling thread is set to NULL.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        ///     Sets the keyboard focus to the specified window. The window must be attached to the calling thread's
        ///     message queue.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window that will receive the keyboard input. If this parameter is NULL,
        ///     keystrokes are ignored.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the window that previously had the
        ///     keyboard focus. If the hWnd parameter is invalid or the window is not attached to the calling thread's
        ///     message queue, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The SetFocus function sends a WM_KILLFOCUS message to the window that loses the keyboard focus and
        ///         a WM_SETFOCUS message to the window that receives the keyboard focus. It also activates either the window
        ///         that receives the focus or the parent of the window that receives the focus.
        ///     </para>
        ///     <para>
        ///         If a window is active but does not have the focus, any key pressed will produce the WM_SYSCHAR,
        ///         WM_SYSKEYDOWN, or WM_SYSKEYUP message. If the VK_MENU key is also pressed, the lParam parameter of the
        ///         message will have bit 30 set. Otherwise, the messages produced do not have this bit set.
        ///     </para>
        ///     <para>
        ///         By using the AttachThreadInput function, a thread can attach its input processing to another thread.
        ///         This allows a thread to call SetFocus to set the keyboard focus to a window attached to another thread's
        ///         message queue.
        ///     </para>
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        ///     The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding
        ///     character or characters. The function translates the code using the input language and physical keyboard
        ///     layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey"> [in] Specifies the virtual-key code to be translated.</param>
        /// <param name="uScanCode">
        ///     [in] Specifies the hardware scan code of the key to be translated. The high-order
        ///     bit of this value is set if the key is up (not pressed).
        /// </param>
        /// <param name="lpbKeyState">
        ///     [in] Pointer to a 256-byte array that contains the current keyboard state.
        ///     Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set,
        ///     the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function,
        ///     only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys
        ///     is ignored.
        /// </param>
        /// <param name="lpwTransKey">[out] Pointer to the buffer that receives the translated character or characters.</param>
        /// <param name="fuState">
        ///     [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active,
        ///     or 0 otherwise.
        /// </param>
        /// <returns>
        ///     If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values.
        ///     Value Meaning
        ///     0 The specified virtual key has no translation for the current state of the keyboard.
        ///     1 One character was copied to the buffer.
        ///     2 Two characters were copied to the buffer. This usually happens when a dead-key character
        ///     (accent or diacritic) stored in the keyboard layout cannot be composed with the specified
        ///     virtual key to form a single character.
        /// </returns>
        /// <remarks>
        ///     The parameters supplied to the ToAscii function might not be sufficient to translate the virtual-key
        ///     code, because a previous dead key is stored in the keyboard layout.
        ///     Typically, ToAscii performs the translation based on the virtual-key code. In some cases, however,
        ///     bit 15 of the uScanCode parameter may be used to distinguish between a key press and a key release.
        ///     The scan code is used for translating ALT+ number key combinations.
        ///     Although NUM LOCK is a toggle key that affects keyboard behavior, ToAscii ignores the toggle setting
        ///     (the low bit) of lpKeyState (VK_NUMLOCK) because the uVirtKey parameter alone is sufficient to distinguish
        ///     the cursor movement keys (VK_HOME, VK_INSERT, and so on) from the numeric keys
        ///     (VK_DECIMAL, VK_NUMPAD0 - VK_NUMPAD9).
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms646316%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey,
            int fuState);

        /// <summary>
        ///     The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding
        ///     character or characters. The function translates the code using the input language and physical keyboard
        ///     layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey"> [in] Specifies the virtual-key code to be translated.</param>
        /// <param name="uScanCode">
        ///     [in] Specifies the hardware scan code of the key to be translated. The high-order
        ///     bit of this value is set if the key is up (not pressed).
        /// </param>
        /// <param name="lpbKeyState">
        ///     [in] Pointer to a 256-byte array that contains the current keyboard state.
        ///     Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set,
        ///     the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function,
        ///     only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys
        ///     is ignored.
        /// </param>
        /// <param name="lpwTransKey">[out] Pointer to the buffer that receives the translated character or characters.</param>
        /// <param name="fuState">
        ///     [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active,
        ///     or 0 otherwise.
        /// </param>
        /// <returns>
        ///     If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values.
        ///     Value Meaning
        ///     0 The specified virtual key has no translation for the current state of the keyboard.
        ///     1 One character was copied to the buffer.
        ///     2 Two characters were copied to the buffer. This usually happens when a dead-key character
        ///     (accent or diacritic) stored in the keyboard layout cannot be composed with the specified
        ///     virtual key to form a single character.
        /// </returns>
        /// <remarks>
        ///     The parameters supplied to the ToAscii function might not be sufficient to translate the virtual-key
        ///     code, because a previous dead key is stored in the keyboard layout.
        ///     Typically, ToAscii performs the translation based on the virtual-key code. In some cases, however,
        ///     bit 15 of the uScanCode parameter may be used to distinguish between a key press and a key release.
        ///     The scan code is used for translating ALT+ number key combinations.
        ///     Although NUM LOCK is a toggle key that affects keyboard behavior, ToAscii ignores the toggle setting
        ///     (the low bit) of lpKeyState (VK_NUMLOCK) because the uVirtKey parameter alone is sufficient to distinguish
        ///     the cursor movement keys (VK_HOME, VK_INSERT, and so on) from the numeric keys
        ///     (VK_DECIMAL, VK_NUMPAD0 - VK_NUMPAD9).
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms646316%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder lpChar, int uFlags);

        /// <summary>
        ///     Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or
        ///     characters.
        ///     <para>
        ///         To specify a handle to the keyboard layout to use to translate the specified code, use
        ///         the ToUnicodeEx function.
        ///     </para>
        /// </summary>
        /// <param name="wVirtKey">The virtual-key code to be translated.</param>
        /// <param name="wScanCode">
        ///     The hardware scan code of the key to be translated. The high-order bit of this
        ///     value is set if the key is up.
        /// </param>
        /// <param name="lpKeyState">
        ///     A pointer to a 256-byte array that contains the current keyboard state. Each
        ///     element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the
        ///     key is down.
        /// </param>
        /// <param name="pwszBuff">
        ///     The buffer that receives the translated Unicode character or characters. However,
        ///     this buffer may be returned without being null-terminated even though the variable name suggests that it
        ///     is null-terminated.
        /// </param>
        /// <param name="cchBuff">The size, in characters, of the buffer pointed to by the pwszBuff parameter.</param>
        /// <param name="wFlags">
        ///     The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are
        ///     reserved.
        /// </param>
        /// <returns>
        ///     The function returns one of the following values.
        ///     -1      The specified virtual key is a dead-key character (accent or diacritic). This value is returned
        ///     regardless of the keyboard layout, even if several characters have been typed and are stored in the keyboard
        ///     state. If possible, even with Unicode keyboard layouts, the function has written a spacing version of the
        ///     dead-key character to the buffer specified by pwszBuff. For example, the function writes the character
        ///     SPACING ACUTE (0x00B4), rather than the character NON_SPACING ACUTE (0x0301).
        ///     0           The specified virtual key has no translation for the current state of the keyboard. Nothing was
        ///     written to the buffer specified by pwszBuff.
        ///     1           One character was written to the buffer specified by pwszBuff.
        ///     2 ≤ value   Two or more characters were written to the buffer specified by pwszBuff. The most common cause
        ///     for this is that a dead-key character (accent or diacritic) stored in the keyboard layout could not be
        ///     combined with the specified virtual key to form a single character. However, the buffer may contain more
        ///     characters than the return value specifies. When this happens, any extra characters are invalid and should
        ///     be ignored.
        /// </returns>
        /// <remarks>
        ///     The parameters supplied to the ToUnicode function might not be sufficient to translate the virtual-key code
        ///     because a previous dead key is stored in the keyboard layout.
        ///     Typically, ToUnicode performs the translation based on the virtual-key code. In some cases, however, bit 15
        ///     of the wScanCode parameter can be used to distinguish between a key press and a key release.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern int ToUnicode(Keys wVirtKey, uint wScanCode, byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff, int cchBuff, uint wFlags);

        /// <summary>
        ///     Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or
        ///     characters.
        ///     <para>
        ///         To specify a handle to the keyboard layout to use to translate the specified code, use
        ///         the ToUnicodeEx function.
        ///     </para>
        /// </summary>
        /// <param name="wVirtKey">The virtual-key code to be translated.</param>
        /// <param name="wScanCode">
        ///     The hardware scan code of the key to be translated. The high-order bit of this
        ///     value is set if the key is up.
        /// </param>
        /// <param name="lpKeyState">
        ///     A pointer to a 256-byte array that contains the current keyboard state. Each
        ///     element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the
        ///     key is down.
        /// </param>
        /// <param name="pwszBuff">
        ///     The buffer that receives the translated Unicode character or characters. However,
        ///     this buffer may be returned without being null-terminated even though the variable name suggests that it
        ///     is null-terminated.
        /// </param>
        /// <param name="cchBuff">The size, in characters, of the buffer pointed to by the pwszBuff parameter.</param>
        /// <param name="wFlags">
        ///     The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are
        ///     reserved.
        /// </param>
        /// <returns>
        ///     The function returns one of the following values.
        ///     -1      The specified virtual key is a dead-key character (accent or diacritic). This value is returned
        ///     regardless of the keyboard layout, even if several characters have been typed and are stored in the keyboard
        ///     state. If possible, even with Unicode keyboard layouts, the function has written a spacing version of the
        ///     dead-key character to the buffer specified by pwszBuff. For example, the function writes the character
        ///     SPACING ACUTE (0x00B4), rather than the character NON_SPACING ACUTE (0x0301).
        ///     0           The specified virtual key has no translation for the current state of the keyboard. Nothing was
        ///     written to the buffer specified by pwszBuff.
        ///     1           One character was written to the buffer specified by pwszBuff.
        ///     2 ≤ value   Two or more characters were written to the buffer specified by pwszBuff. The most common cause
        ///     for this is that a dead-key character (accent or diacritic) stored in the keyboard layout could not be
        ///     combined with the specified virtual key to form a single character. However, the buffer may contain more
        ///     characters than the return value specifies. When this happens, any extra characters are invalid and should
        ///     be ignored.
        /// </returns>
        /// <remarks>
        ///     The parameters supplied to the ToUnicode function might not be sufficient to translate the virtual-key code
        ///     because a previous dead key is stored in the keyboard layout.
        ///     Typically, ToUnicode performs the translation based on the virtual-key code. In some cases, however, bit 15
        ///     of the wScanCode parameter can be used to distinguish between a key press and a key release.
        /// </remarks>
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern int ToUnicode(Keys virtualKey, uint scanCode, byte[] keyStates,
            [MarshalAs(UnmanagedType.LPArray)] [Out] char[] chars, int charMaxCount, uint flags);
        #endregion


        #region Fields
        static readonly Dictionary<char, VirtualKeys> _symbolTable1;
        static readonly Dictionary<char, VirtualKeys> _symbolTable2;

        static readonly VirtualKeys[] _telexInput =
        {
            0, VirtualKeys.S, VirtualKeys.F, VirtualKeys.R, VirtualKeys.X,
            VirtualKeys.J, 0, VirtualKeys.W, VirtualKeys.W
        };

        static readonly string[] letterChars =
        {
            "aáàảãạâ\0ă", "ăắằẳẵặ", "âấầẩẫậ", "eéèẻẽẹê", "êếềểễệ", "iíìỉĩị", "oóòỏõọôơ",
            "ôốồổỗộ", "ơớờởỡợ", "uúùủũụ\0ư", "ưứừửữự", "yýỳỷỹỵ"
        };
        #endregion


        #region  Constructors & Destructor
        static Keyboard()
        {
            _symbolTable1 = GetSymbolTable1();
            _symbolTable2 = GetSymbolTable2();
        }
        #endregion


        #region Methods
        /// <summary>
        ///     Get the status of the 256 virtual keys.
        /// </summary>
        /// <returns>The array of KeyState objects that contains the status data for each virtual key.</returns>
        public static KeyState[] GetVirtualKeyboardState()
        {
            var keyStates = new byte[256];
            GetKeyboardState(keyStates);
            return keyStates.Select(b => new KeyState(b)).ToArray();
        }

        /// <summary>
        ///     Retrieves the status of the specified virtual key. The status specifies whether the key is up, down,
        ///     or toggled (on, off—alternating each time the key is pressed).
        /// </summary>
        /// <param name="virtKey">
        ///     <para>
        ///         A virtual key. If the desired virtual key is a letter or digit (A through Z,
        ///         a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys,
        ///         it must be a virtual-key code.
        ///     </para>
        ///     <para>
        ///         If a non-English keyboard layout is used, virtual keys with values in the range ASCII A through Z and
        ///         0 through 9 are used to specify most of the character keys. For example, for the German keyboard layout, the
        ///         virtual key of value ASCII O (0x4F) refers to the "o" key, whereas VK_OEM_1 refers to the "o with umlaut" key.
        ///     </para>
        /// </param>
        /// <returns>
        ///     A KeyState struct that represents whether the key is pressed and whether the key is toggled (used only
        ///     for CapsLock, NumLock and ScrollLock keys)
        /// </returns>
        public static KeyState GetVirtualKeyState(Keys virtKey)
        {
            return new KeyState(GetKeyState(virtKey));
        }

        /// <summary>
        ///     Stimulate a key combination press
        /// </summary>
        /// <param name="keys">The key combination</param>
        /// <returns>If the function succeeds it returns true; otherwise, returns false</returns>
        public static bool SendKeyCombination(VirtualKeys[] keys)
        {
            var length = keys.Length;
            if (length > 0)
            {
                var doubleLength = 2 * length /*, partTime = time / doubleLength*/;
                var inputs = new Input[doubleLength];
                for (var i = 0; i < length; i++)
                {
                    var kbInput = new KeyboardInput { wVk = keys[i], dwFlags = 0 /*, time = partTime */ };
                    inputs[i] = new Input { type = InputTypes.Keyboard, U = new InputUnion { ki = kbInput } };
                }
                for (var i = length - 1; i >= 0; i--)
                {
                    var kbInput = new KeyboardInput
                    {
                        wVk = keys[i],
                        dwFlags = KeyEventFlags.KeyUp /*, time = partTime */
                    };
                    inputs[doubleLength - i - 1] = new Input
                    {
                        type = InputTypes.Keyboard,
                        U = new InputUnion { ki = kbInput }
                    };
                }
                return SendInput(doubleLength, inputs, Marshal.SizeOf(inputs[0])) == doubleLength;
            }
            return true;
        }

        /// <summary>
        ///     Stimulate a key combination press
        /// </summary>
        /// <param name="keys">The key combination</param>
        /// <returns>If the function succeeds it returns true; otherwise, returns false</returns>
        public static bool SendKeyCombination(Keys[] keys)
        {
            return SendKeyCombination(keys.Cast<VirtualKeys>().ToArray());
        }

        public static async Task<bool> SendKeyCombination(VirtualKeys[] keys, int time)
        {
            var result = SendKeyCombination(keys);
            await Task.Delay(time);
            return result;
        }

        public static async Task<bool> SendKeyCombination(Keys[] keys, int time)
        {
            return await SendKeyCombination(keys.Cast<VirtualKeys>().ToArray(), time);
        }

        /*public static VirtualKeys[] GetKeysPressed()
        {
            return GetVirtualKeyboardState().Select((s, i) => new { State = s, Code = i }).Where(sc => sc.State.Pressed).Select(sc => (VirtualKeys)sc.Code).ToArray();
        }*/

        /// <summary>
        ///     Stimulate a key press (one couple of key down and and key up)
        /// </summary>
        /// <param name="key">The key to stimulate a key press</param>
        /// <returns>If the function succeeds it returns true; otherwise, returns false</returns>
        public static bool SendKeyPress(VirtualKeys key)
        {
            var kInput1 = new KeyboardInput { wVk = key, dwFlags = 0 /*, time = time/2 */ };
            var input1 = new Input { type = InputTypes.Keyboard, U = new InputUnion { ki = kInput1 } };
            var kInput2 = new KeyboardInput { wVk = key, dwFlags = KeyEventFlags.KeyUp /*, time = time/2 */ };
            var input2 = new Input { type = InputTypes.Keyboard, U = new InputUnion { ki = kInput2 } };
            return SendInput(2, new[] { input1, input2 }, Marshal.SizeOf(input1)) == 2;
        }

        /// <summary>
        ///     Stimulate a key press (one couple of key down and and key up)
        /// </summary>
        /// <param name="key">The key to stimulate a key press</param>
        /// <returns>If the function succeeds it returns true; otherwise, returns false</returns>
        public static bool SendKeyPress(Keys key)
        {
            return SendKeyPress((VirtualKeys)key);
        }

        public static async Task<bool> SendKeyPress(VirtualKeys key, int time)
        {
            var result = SendKeyPress(key);
            await Task.Delay(time);
            return result;
        }

        public static async Task<bool> SendKeyPress(Keys key, int time)
        {
            return await SendKeyPress((VirtualKeys)key, time);
        }

        public static async Task<bool> SendUnicodeKeyChar(char c, InputMethods method = InputMethods.Vni, int time = 0)
        {
            return await SendUnicodeKeyChar(c, method, time, GetSymbolTable1(), GetSymbolTable2());
        }

        public static async Task<bool> SendUnicodeString(string s, InputMethods method = InputMethods.Vni, int time = 0)
        {
            //Dictionary<char, VirtualKeys>[] dicts = { GetSymbolTable1(), GetSymbolTable2() };
            var result = true;
            foreach (var c in s)
                result &= await SendUnicodeKeyChar(c, method, time);
            return result;
        }
        #endregion


        #region Implementation
        static bool GetLetterKeys(char c, ref List<VirtualKeys> keys, InputMethods method) //lower character only
        {
            if (char.IsUpper(c))
            {
                keys.Add(VirtualKeys.Shift);
                c = char.ToLower(c);
            }
            if (c >= 'a' && c <= 'z')
            {
                keys.Add((VirtualKeys)Enum.Parse(typeof(VirtualKeys), c.ToString(), true));
                return true;
            }
            if (c == 'đ')
            {
                keys.Add(VirtualKeys.D);
                switch (method)
                {
                    case InputMethods.Vni:
                        keys.Add(VirtualKeys.N9);
                        return true;
                    case InputMethods.Telex:
                        keys.Add(VirtualKeys.D);
                        return true;
                    default:
                        throw new NotImplementedException();
                }
            }
            for (var i = 0; i < letterChars.Length; i++)
                if (letterChars[i].Contains(c) && GetLetterKeys(letterChars[i][0], ref keys, method))
                {
                    var index = letterChars[i].IndexOf(c);
                    switch (method)
                    {
                        case InputMethods.Vni:
                            keys.Add((VirtualKeys)((int)VirtualKeys.N0 + index));
                            return true;
                        case InputMethods.Telex:
                            if (index == 6)
                                keys.Add(keys.Last());
                            else
                                keys.Add(_telexInput[index]);
                            return true;
                        default:
                            throw new NotImplementedException();
                    }
                }
            return false;
        }

        static Dictionary<char, VirtualKeys> GetSymbolTable1()
        {
            var chars = new char[] { '`', '-', '=', '\\', '[', ']', ';', '\'', ',', '.', '/' };
            var keys = new VirtualKeys[]
            {
                VirtualKeys.OEM3, VirtualKeys.OEMMinus, VirtualKeys.OEMPlus,
                VirtualKeys.OEM5, VirtualKeys.OEM4, VirtualKeys.OEM6, VirtualKeys.OEM1, VirtualKeys.OEM7,
                VirtualKeys.OEMComma, VirtualKeys.OEMPeriod, VirtualKeys.OEM2
            };
            return chars.Zip(keys, (c, k) => new { Character = c, VirtualKey = k }).ToDictionary(ck => ck.Character,
                ck => ck.VirtualKey);
        }

        static Dictionary<char, VirtualKeys> GetSymbolTable2()
        {
            var chars = new[]
            {
                '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '|',
                '{', '}', ':', '\"', '<', '>', '?'
            };
            var keys = new[]
            {
                VirtualKeys.OEM3, VirtualKeys.N1, VirtualKeys.N2, VirtualKeys.N3,
                VirtualKeys.N4, VirtualKeys.N5, VirtualKeys.N6, VirtualKeys.N7, VirtualKeys.N8, VirtualKeys.N9,
                VirtualKeys.N0, VirtualKeys.OEMMinus, VirtualKeys.OEMPlus, VirtualKeys.OEM5, VirtualKeys.OEM4,
                VirtualKeys.OEM6, VirtualKeys.OEM1, VirtualKeys.OEM7, VirtualKeys.OEMComma, VirtualKeys.OEMPeriod,
                VirtualKeys.OEM2
            };
            return chars.Zip(keys, (c, k) => new { Character = c, VirtualKey = k }).ToDictionary(ck => ck.Character,
                ck => ck.VirtualKey);
        }

        static int SendKeyDown(VirtualKeys key)
        {
            var kInput = new KeyboardInput { wVk = key, dwFlags = 0 };
            var input = new Input { type = InputTypes.Keyboard, U = new InputUnion { ki = kInput } };
            return SendInput(1, new[] { input }, Marshal.SizeOf(input));
        }

        static int SendKeyUp(VirtualKeys key)
        {
            var kInput = new KeyboardInput { wVk = key, dwFlags = KeyEventFlags.KeyUp };
            var input = new Input { type = InputTypes.Keyboard, U = new InputUnion { ki = kInput } };
            return SendInput(1, new[] { input }, Marshal.SizeOf(input));
        }

        static async Task<bool> SendUnicodeKeyChar(char c, InputMethods method, int time,
            params Dictionary<char, VirtualKeys>[] dicts)
        {
            // Controls, digits, escape and space
            if (c >= 8 && c <= 13 || c >= 48 && c <= 57 || c == 32 || c == 27)
                return await SendKeyPress((VirtualKeys)c, time);
            var keys = new List<VirtualKeys>();
            if (GetLetterKeys(c, ref keys, method))
            {
                if (keys[0] == VirtualKeys.Shift)
                {
                    SendKeyDown(VirtualKeys.Shift);
                    SendKeyDown(keys[1]);
                    SendKeyUp(VirtualKeys.Shift);
                    SendKeyUp(keys[1]);

                    await Task.Delay(time / 2);

                    for (var i = 2; i < keys.Count; i++)
                    {
                        SendKeyPress(keys[i]);
                    }
                    await Task.Delay(time / 2);
                    return true;
                }
                return await SendKeyCombination(keys.ToArray(), time);
            }
            /*if (dicts[0].ContainsKey(c))
                return await SendKeyPress(dicts[0][c], time);
            if (dicts[1].ContainsKey(c))
                return await SendKeyCombination(new VirtualKeys[] { VirtualKeys.Shift, dicts[1][c] }, time);*/
            if (_symbolTable1.ContainsKey(c))
                return await SendKeyPress(_symbolTable1[c], time);
            if (_symbolTable2.ContainsKey(c))
                return await SendKeyCombination(new[] { VirtualKeys.Shift, _symbolTable2[c] }, time);
            return false;
        }
        #endregion
    }
}