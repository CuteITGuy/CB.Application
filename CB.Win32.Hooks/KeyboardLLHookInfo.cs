using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains informations of a keyboard hook caught by LowLevelKeyboardProc
    /// </summary>
    public class KeyboardLLHookInfo: KeyboardInfo
    {
        #region Fields
        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        protected IntPtr _extraInfo;

        /// <summary>
        ///     Specifies the _time stamp in milliseconds for this _message.
        /// </summary>
        protected int _time;
        #endregion


        #region  Constructors & Destructor
        /// <summary>
        ///     Initializes a new instance of KeyboardInfo class
        /// </summary>
        public KeyboardLLHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            var info = (KeyboardLLHookStruct)Marshal.PtrToStructure((IntPtr)lParam, typeof(KeyboardLLHookStruct));
            _keyCode = info.KeyCode;
            ScanCode = info.ScanCode;
            AltKey = ((int)info.Flags & 0x20) == 0x20;
            Pressed = ((int)info.Flags & 0x80) == 0;
            Time = info.Time;
            ExtraInfo = info.ExtraInfo;
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        public virtual IntPtr ExtraInfo
        {
            get { return _extraInfo; }
            protected set { _extraInfo = value; }
        }

        /// <summary>
        ///     Specifies the _time stamp in milliseconds for this _message.
        /// </summary>
        public virtual int Time
        {
            get { return _time; }
            protected set { _time = value; }
        }
        #endregion
    }
}