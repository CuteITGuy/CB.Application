using System;
using System.Diagnostics;
using CB.Win32.Cursors;
using CB.Win32.Hooks;
using CB.Win32.Messages;
using CB.Win32.Windows;


namespace CB.Win32.WindowManipulation
{
    public static class WindowMove
    {
        #region Fields
        private static CursorLoad _cursorLoad;
        private static bool _findWindow = true;
        private static MouseLLHook _mouseHook;
        private static int _mouseX, _mouseY, _windowX, _windowY;
        private static bool _movable;
        private static IntPtr _windowHandle = IntPtr.Zero;
        #endregion


        #region  Constructors & Destructor
        static WindowMove() { }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Gets and sets the mouse button triggering and halting the move action.
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
        ///     Begin to track mouse move and move window under the mouse cursor along.
        /// </summary>
        public static void BeginMove()
        {
            InitializeComponents();
            _mouseHook.Enabled = true;
            _cursorLoad.SetCursor();
        }

        /// <summary>
        ///     Stop tracking mouse move and moving window, dispose resources
        /// </summary>
        public static void EndMove()
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

        /// <summary>
        ///     Sets the handle of the target window to move. After setting window handle this way, every time the mouse cursor
        ///     moves the program will not try to find what window to move but use this window instead.
        ///     <para>
        ///         To make the program to try to find what window to move, call this method again with IntPtr.Zero as an
        ///         argument.
        ///     </para>
        /// </summary>
        /// <param name="windowHandle">The handle of the target window.</param>
        /// <returns>True if <paramref name="windowHandle" /> is not Zero and valid; false otherwise.</returns>
        public static bool SetWindowHandle(IntPtr windowHandle)
        {
            if (windowHandle != IntPtr.Zero && Window.IsWindow(windowHandle))
            {
                _windowHandle = windowHandle;
                _findWindow = false;
                return true;
            }
            else
            {
                _windowHandle = IntPtr.Zero;
                _findWindow = true;
                return false;
            }
        }
        #endregion


        #region Implementation
        private static bool GetWindowAndMouseInfos()
        {
            if (_findWindow)
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

            if (WindowHandle != IntPtr.Zero)
            {
                Debug.WriteLine(Window.GetParent(WindowHandle));

                /*// Get window position relative to screen
                Rect winRect;
                if (Window.GetWindowRect(WindowHandle, out winRect))
                {
                    windowX = winRect.X;
                    windowY = winRect.Y;

                    // Get mouse position relative to screen
                    System.Drawing.Point mousePos = Cursor.GetCursorPos();
                    mouseX = mousePos.X;
                    mouseY = mousePos.Y;
                    return true;
                }*/

                // Get window's relative position
                var pos = Window.GetRelativePos(WindowHandle);
                _windowX = pos.X;
                _windowY = pos.Y;

                // Get mouse position relative to screen
                var mousePos = Cursor.GetCursorPos();
                _mouseX = mousePos.X;
                _mouseY = mousePos.Y;
                return true;
            }

            //windowX = windowY = mouseX = mouseY = -1;
            return false;
        }

        /// <summary>
        ///     Initialize mouse hook and set it's event.
        /// </summary>
        private static void InitializeComponents()
        {
            _mouseHook = new MouseLLHook();

            // Listen to mouse messages
            _mouseHook.Event += (sender, info) =>
            {
                var mouseInfo = (MouseLLHookInfo)info;
                var mouseMsg = mouseInfo.Message;
                if (mouseMsg == WindowsMessages.MouseMove)
                {
                    if (_movable)
                    {
                        int difX = mouseInfo.Location.X - _mouseX,
                            difY = mouseInfo.Location.Y - _mouseY;
                        Window.SetWindowPosition(WindowHandle, _windowX + difX, _windowY + difY);
                    }
                }
                else if (mouseMsg == (WindowsMessages)Button)
                {
                    if (GetWindowAndMouseInfos())
                    {
                        _movable = true;
                    }
                }
                else if (mouseMsg == (WindowsMessages)(Button + 1))
                {
                    _movable = false;
                }

                /*switch (mouseInfo.Message)
                {
                    case WindowsMessages.MouseMove:
                        if (movable)
                        {
                            int difX = mouseInfo.Location.X - mouseX,
                                                difY = mouseInfo.Location.Y - mouseY;
                            Window.SetWindowPosition(WindowHandle, windowX + difX, windowY + difY);
                        }
                        break;

                    case WindowsMessages.LeftButtonDown:
                        if (GetWindowAndMouseInfos())
                        {
                            movable = true;
                        }
                        break;

                    case WindowsMessages.LeftButtonUp:
                        movable = false;
                        break;
                    default: break;
                }*/
            };

            _cursorLoad = new CursorLoad(@"Binary\MoveCursor.cur");
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