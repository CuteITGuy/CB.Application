using System;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     The extended-key flag, event-injected flags, context code, and transition-state flag. This member is
    ///     specified as follows. An application can use the following values to test the keystroke flags. Testing
    ///     LLKHF_INJECTED (bit 4) will tell you whether the event was injected. If it was, then testing
    ///     LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process
    ///     running at lower integrity level.
    /// </summary>
    [Flags]
    public enum ExtendedKeyFlags: uint
    {
        /// <summary>
        ///     LLKHF_EXTENDED flag.
        ///     <para>Test the extended-key flag.</para>
        /// </summary>
        Extended = 0x00000001,

        /// <summary>
        ///     LLKHF_LOWER_IL_INJECTED flag.
        ///     <para> Test the event-injected (from a process running at lower integrity level) flag.</para>
        /// </summary>
        LowerIntegrityLevelInjected = 0x00000002,

        /// <summary>
        ///     LLKHF_INJECTED flag
        ///     <para>Test the event-injected (from any process) flag.</para>
        /// </summary>
        Injected = 0x00000010,

        /// <summary>
        ///     LLKHF_ALTDOWN flag
        ///     <para>Test the context code.</para>
        /// </summary>
        AltKeyDown = 0x00000020,

        /// <summary>
        ///     LLKHF_UP flag
        ///     <para>Test the transition-state flag.</para>
        /// </summary>
        Up = 0x00000080,
    }
}