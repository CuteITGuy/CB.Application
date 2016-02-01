using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains informations if a mouse hook caught by LowLevelMouseProc
    /// </summary>
    public class MouseLLHookInfo: MouseInfo
    {
        #region Fields
        /// <summary>
        ///     The event-injected flags. An application can use the following values to test the flags. Testing
        ///     LLMHF_INJECTED (bit 0) will tell you whether the event was injected. If it was, then testing
        ///     LLMHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process
        ///     running at lower integrity level.
        ///     <para>LLMHF_INJECTED = 0x00000001. Test the event-injected (from any process) flag.</para>
        ///     <para>
        ///         LLMHF_LOWER_IL_INJECTED = 0x00000002. Test the event-injected (from a process running at lower
        ///         integrity level) flag.
        ///     </para>
        /// </summary>
        protected int _eventInjectedFlags;

        /// <summary>
        ///     Specifies the _time stamp for this _message.
        /// </summary>
        protected int _time;
        #endregion


        #region  Constructors & Destructor
        public MouseLLHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            var info = (MouseLLHookStruct)Marshal.PtrToStructure((IntPtr)lParam, typeof(MouseLLHookStruct));
            Location = info.Location;
            MouseData = info.MouseData;
            ExtraInfo = info.ExtraInfo;
            EventInjectedFlags = info.EventInjectedFlags;
            Time = info.Time;
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     The event-injected flags. An application can use the following values to test the flags. Testing
        ///     LLMHF_INJECTED (bit 0) will tell you whether the event was injected. If it was, then testing
        ///     LLMHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process
        ///     running at lower integrity level.
        ///     <para>LLMHF_INJECTED = 0x00000001. Test the event-injected (from any process) flag.</para>
        ///     <para>
        ///         LLMHF_LOWER_IL_INJECTED = 0x00000002. Test the event-injected (from a process running at lower
        ///         integrity level) flag.
        ///     </para>
        /// </summary>
        public virtual int EventInjectedFlags
        {
            get { return _eventInjectedFlags; }
            protected set { _eventInjectedFlags = value; }
        }

        /// <summary>
        ///     Specifies the _time stamp for this _message.
        /// </summary>
        public virtual int Time
        {
            get { return _time; }
            protected set { _time = value; }
        }
        #endregion
    }
}