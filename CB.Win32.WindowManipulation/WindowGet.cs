using System;
using CB.Win32.Hooks;
using CB.Win32.Messages;
using CB.Win32.Windows;


namespace CB.Win32.WindowManipulation
{
    public static class WindowGet
    {
        #region Fields
        private static CursorLoad _cursorLoad;
        private static MouseLLHook _mouseHook;
        private static IntPtr _windowHandle = IntPtr.Zero;
        #endregion


        #region  Constructors & Destructor
        static WindowGet() { }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Gets and sets the mouse button used to get window handle.
        /// </summary>
        public static WindowManipulationButton Button { get; set; } = WindowManipulationButton.LeftButton;

        /// <summary>
        ///     Sets a nullable relative or absolute path to the cursor file.
        ///     <para>If this value is null or empty, the default cursor will be used.</para>
        ///     <para>Throw Win32Exception if file not found or not a valid cursor.</para>
        /// </summary>
        public static string CursorFile
        {
            set { _cursorLoad.CursorFile = value; }
        }

        /// <summary>
        ///     Gets and sets the type of choosing the target window to move.
        /// </summary>
        public static WindowManipulationType ManipulationType { get; set; } = WindowManipulationType.TopLevelWindow;

        /// <summary>
        ///     The handle of the moved window.
        /// </summary>
        public static IntPtr WindowHandle
        {
            get { return _windowHandle; }
            private set
            {
                _windowHandle = value;
                OnWindowHandleChanged();
            }
        }
        #endregion


        #region Events
        /// <summary>
        ///     Occurs when the WindowHandle property is changed.
        /// </summary>
        public static event EventHandler WindowHandleChanged;
        #endregion


        #region Methods
        /// <summary>
        ///     Begin to track mouse click and get the window under the mouse cursor.
        /// </summary>
        public static void BeginGet()
        {
            InitializeComponent();
            _mouseHook.Enabled = true;
            _cursorLoad.SetCursor();
        }

        /// <summary>
        ///     Stop tracking mouse click, dispose resources
        /// </summary>
        public static void EndGet()
        {
            if (_mouseHook != null)
            {
                _mouseHook.Dispose();
                _mouseHook = null;
            }
            if (_cursorLoad != null)
            {
                _cursorLoad.Dispose();
                _cursorLoad = null;
            }
        }
        #endregion


        #region Implementation
        private static void GetWindowHandle()
        {
            // Get window handle at mouse
            switch (ManipulationType)
            {
                case WindowManipulationType.FirstWindowFound:
                    WindowHandle = Window.GetWindowAtMouse();
                    break;

                case WindowManipulationType.TopLevelWindow:
                    WindowHandle = Window.GetTopLevelWindowAtMouse();
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Initialize mouse hook and set it's event.
        /// </summary>
        private static void InitializeComponent()
        {
            _mouseHook = new MouseLLHook();

            // Listen to mouse messages
            _mouseHook.Event += (sender, info) =>
            {
                var mouseInfo = (MouseLLHookInfo)info;
                if (mouseInfo.Message == (WindowsMessages)Button)
                {
                    GetWindowHandle();
                }
                /*switch (mouseInfo.Message)
                {
                    case WindowsMessages.LeftButtonDown:
                        GetWindowHandle();
                        break;
                    default: break;
                }*/
            };

            _cursorLoad = new CursorLoad(@"Binary\GetCursor.cur");
        }

        /// <summary>
        ///     Calls the WindowHandleChanged event.
        /// </summary>
        private static void OnWindowHandleChanged()
        {
            WindowHandleChanged?.Invoke(null, EventArgs.Empty);
        }
        #endregion
    }
}