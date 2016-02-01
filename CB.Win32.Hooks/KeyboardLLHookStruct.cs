using System;
using System.Runtime.InteropServices;
using CB.Win32.Inputs;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms644967%28v=vs.85%29.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct KeyboardLLHookStruct
    {
        [FieldOffset(0)]
        private readonly int vkCode;

        /// <summary>
        ///     Specifies a virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        [FieldOffset(0)]
        public VirtualKeys KeyCode;

        [FieldOffset(4)]
        private readonly int scanCode;

        /// <summary>
        ///     Specifies a hardware scan code for the key.
        /// </summary>
        [FieldOffset(4)]
        public ScanCodes ScanCode;

        /// <summary>
        ///     Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        [FieldOffset(8)]
        public ExtendedKeyFlags Flags;

        /// <summary>
        ///     Specifies the _time stamp in milliseconds for this _message.
        /// </summary>
        [FieldOffset(12)]
        public int Time;

        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        [FieldOffset(16)]
        public IntPtr ExtraInfo;
    }
}