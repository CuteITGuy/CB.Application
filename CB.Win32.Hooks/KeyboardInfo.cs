using CB.Win32.Inputs;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Used in KeyboardHookEventHandler and is base class of KeyboardLLHookInfo
    /// </summary>
    public class KeyboardInfo: HookInfo
    {
        #region Fields
        /// <summary>
        ///     Specifies whether the ALT key is down
        /// </summary>
        protected bool _altKey;

        /// <summary>
        ///     The virtual-key code of the key that generated the keystroke _message.
        /// </summary>
        protected VirtualKeys _keyCode;

        /// <summary>
        ///     The transition state. The value is true if the key is being _pressed and false if it is being released.
        /// </summary>
        protected bool _pressed;

        /// <summary>
        ///     The scan code. The value depends on the OEM.
        /// </summary>
        protected ScanCodes _scanCode;
        #endregion


        #region  Constructors & Destructor
        /// <summary>
        ///     Initializes a new instance of KeyboardInfo class
        /// </summary>
        /// <param name="nCode">The nCode parameter from KeyboardProc</param>
        /// <param name="wParam">The wParam parameter from KeyboardProc</param>
        /// <param name="lParam">The lParam parameter from KeyboardProc</param>
        public KeyboardInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam) { }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Specifies whether the ALT key is down
        /// </summary>
        public virtual bool AltKey
        {
            get { return _altKey; }
            protected set { _altKey = value; }
        }

        /// <summary>
        ///     The virtual-key code of the key that generated the keystroke _message.
        /// </summary>
        public virtual VirtualKeys KeyCode
        {
            get { return _keyCode; }
            protected set { _keyCode = value; }
        }

        /// <summary>
        ///     The transition state. The value is true if the key is being _pressed and false if it is being released.
        /// </summary>
        public virtual bool Pressed
        {
            get { return _pressed; }
            protected set { _pressed = value; }
        }

        /// <summary>
        ///     The scan code. The value depends on the OEM.
        /// </summary>
        public virtual ScanCodes ScanCode
        {
            get { return _scanCode; }
            protected set { _scanCode = value; }
        }
        #endregion
    }
}