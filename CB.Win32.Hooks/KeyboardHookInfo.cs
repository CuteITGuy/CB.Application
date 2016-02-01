using CB.Win32.Inputs;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains informations of a keyboard hook caught by KeyboardProc
    /// </summary>
    public class KeyboardHookInfo: KeyboardInfo
    {
        #region Fields
        /// <summary>
        ///     Indicates whether the key is an extended key, such as a function key or a key on the numeric keypad.
        /// </summary>
        protected bool _extendedKey;

        /// <summary>
        ///     The previous key state. The value is true if the key is down before the _message is sent; otherwise the key is up.
        /// </summary>
        protected bool _keyDown;

        /// <summary>
        ///     The number of times the keystroke is repeated as a result of the user's holding down the key.
        /// </summary>
        protected int _repeatCount;
        #endregion


        #region  Constructors & Destructor
        /// <summary>
        ///     Initializes a new instance of KeyboardHookInfo class
        /// </summary>
        /// <param name="nCode">The <c>nCode</c> parameter from KeyboardProc</param>
        /// <param name="wParam">The <c>wParam</c> parameter from KeyboardProc</param>
        /// <param name="lParam">The <c>lParam</c> parameter from KeyboardProc</param>
        public KeyboardHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            _keyCode = (VirtualKeys)wParam;
            ScanCode = (ScanCodes)((lParam >> 16) & 0xff);
            AltKey = (lParam & 0x20000000) == 0x20000000;
            Pressed = (lParam & 0x80000000) == 0;
            RepeatCount = lParam & 0xffff;
            ExtendedKey = (lParam & 0x1000000) == 0x1000000;
            KeyDown = (lParam & 0x40000000) == 0x40000000;
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Indicates whether the key is an extended key, such as a function key or a key on the numeric keypad.
        /// </summary>
        public virtual bool ExtendedKey
        {
            get { return _extendedKey; }
            protected set { _extendedKey = value; }
        }

        /// <summary>
        ///     The previous key state. The value is true if the key is down before the _message is sent; otherwise the key is up.
        /// </summary>
        public virtual bool KeyDown
        {
            get { return _keyDown; }
            protected set { _keyDown = value; }
        }

        /// <summary>
        ///     The number of times the keystroke is repeated as a result of the user's holding down the key.
        /// </summary>
        public virtual int RepeatCount
        {
            get { return _repeatCount; }
            protected set { _repeatCount = value; }
        }
        #endregion
    }
}