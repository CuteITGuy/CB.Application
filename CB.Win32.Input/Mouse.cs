using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using CB.Win32.Cursors;


namespace CB.Win32.Inputs
{
    public static class Mouse
    {
        #region Import
        /// <summary>
        ///     Retrieves a handle to the window (if any) that has captured the mouse. Only one window at a time can
        ///     capture the mouse; this window receives mouse input whether or not the cursor is within its borders.
        /// </summary>
        /// <returns>
        ///     The return value is a handle to the capture window associated with the current thread. If no
        ///     window in the thread has captured the mouse, the return value is NULL.
        /// </returns>
        /// <remarks>
        ///     A NULL return value means the current thread has not captured the mouse. However, it is possible that
        ///     another thread or process has captured the mouse.
        ///     To get a handle to the capture window on another thread, use the GetGUIThreadInfo function.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        /// <summary>
        ///     Retrieves the current double-click time for the mouse. A double-click is a series of two clicks of the
        ///     mouse button, the second occurring within a specified time after the first. The double-click time is the
        ///     maximum number of milliseconds that may occur between the first and second click of a double-click. The
        ///     maximum double-click time is 5000 milliseconds.
        /// </summary>
        /// <returns>
        ///     The return value specifies the current double-click time, in milliseconds. The maximum return value
        ///     is 5000 milliseconds.
        /// </returns>
        /// <remarks>
        ///     Similar to System.Windows.Forms.SystemInformation.DoubleClickTime
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern int GetDoubleClickTime();

        /// <summary>
        ///     Releases the mouse capture from a window in the current thread and restores normal mouse input processing.
        ///     A window that has captured the mouse receives all mouse input, regardless of the position of the cursor,
        ///     except when a mouse button is clicked while the cursor hot spot is in the window of another thread.
        /// </summary>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     An application calls this function after calling the SetCapture function.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        ///     Sets the mouse capture to the specified window belonging to the current thread. SetCapture captures mouse
        ///     input either when the mouse is over the capturing window, or when the mouse button was pressed while the
        ///     mouse was over the capturing window and the button is still down. Only one window at a time can capture
        ///     the mouse.
        ///     <para>
        ///         If the mouse cursor is over a window created by another thread, the system will direct
        ///         mouse input to the specified window only if a mouse button is down.
        ///     </para>
        /// </summary>
        /// <param name="hWnd">A handle to the window in the current thread that is to capture the mouse.</param>
        /// <returns>
        ///     The return value is a handle to the window that had previously captured the mouse. If there is
        ///     no such window, the return value is NULL.
        /// </returns>
        /// <remarks>
        ///     Only the foreground window can capture the mouse. When a background window attempts to do so,
        ///     the window receives messages only for mouse events that occur when the cursor hot spot is within the
        ///     visible portion of the window. Also, even if the foreground window has captured the mouse, the user can
        ///     still click another window, bringing it to the foreground.
        ///     When the window no longer requires all mouse input, the thread that created the window should call the
        ///     ReleaseCapture function to release the mouse.
        ///     This function cannot be used to capture mouse input meant for another process.
        ///     When the mouse is captured, menu hotkeys and other keyboard accelerators do not work.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        /// <summary>
        ///     Sets the double-click time for the mouse. A double-click is a series of two clicks of a mouse button,
        ///     the second occurring within a specified time after the first. The double-click time is the maximum number
        ///     of milliseconds that may occur between the first and second clicks of a double-click.
        /// </summary>
        /// <param name="uInterval">
        ///     The number of milliseconds that may occur between the first and second clicks of
        ///     a double-click. If this parameter is set to 0, the system uses the default double-click time of 500
        ///     milliseconds. If this parameter value is greater than 5000 milliseconds, the system sets the value to
        ///     5000 milliseconds.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     The SetDoubleClickTime function alters the double-click time for all windows in the system.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern bool SetDoubleClickTime(int uInterval);

        /// <summary>
        ///     Reverses or restores the meaning of the left and right mouse buttons.
        /// </summary>
        /// <param name="fSwap">
        ///     If this parameter is TRUE, the left button generates right-button messages and the
        ///     right button generates left-button messages. If this parameter is FALSE, the buttons are restored to
        ///     their original meanings.
        /// </param>
        /// <returns>
        ///     If the meaning of the mouse buttons was reversed previously, before the function was called,
        ///     the return value is nonzero.
        ///     If the meaning of the mouse buttons was not reversed, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool SwapMouseButton(bool fSwap);
        #endregion


        #region Methods
        public static void SendMouseClick(MouseButtons button, Point location, bool absoluteLocation = true)
        {
            SendMouseDown(button, location, absoluteLocation);
            SendMouseUp(button, location, absoluteLocation);
        }

        public static void SendMouseClick(MouseButtons button)
        {
            SendMouseClick(button, Cursor.GetCursorPos(), true);
        }

        public static void SendMouseDoubleClick(MouseButtons button, Point location, bool absoluteLocation = true)
        {
            SendMouseDown(button, location, absoluteLocation);
            SendMouseUp(button, location, absoluteLocation);
            SendMouseDown(button, location, absoluteLocation);
            SendMouseUp(button, location, absoluteLocation);
        }

        public static void SendMouseDoubleClick(MouseButtons button)
        {
            SendMouseDoubleClick(button, Cursor.GetCursorPos(), true);
        }

        public static void SendMouseDown(MouseButtons button, Point location, bool absoluteLocation = true)
        {
            MouseEventFlags meFlag = 0;
            var mData = 0;
            switch (button)
            {
                case MouseButtons.Left:
                    meFlag = MouseEventFlags.LeftDown;
                    break;
                case MouseButtons.Right:
                    meFlag = MouseEventFlags.RightDown;
                    break;
                case MouseButtons.Middle:
                    meFlag = MouseEventFlags.MiddleDown;
                    break;
                case MouseButtons.XButton1:
                    meFlag = MouseEventFlags.XDown;
                    mData = 1;
                    break;
                case MouseButtons.XButton2:
                    meFlag = MouseEventFlags.XDown;
                    mData = 2;
                    break;
            }
            if (absoluteLocation)
                meFlag |= MouseEventFlags.Absolute;
            var mInput = new MouseInput { dx = location.X, dy = location.Y, mouseData = mData, dwFlags = meFlag };
            SendMouseInput(ref mInput);
        }

        public static void SendMouseDown(MouseButtons button /*, bool absoluteLocation = true*/)
        {
            SendMouseDown(button, Cursor.GetCursorPos(), /*absoluteLocation*/true);
        }

        public static void SendMouseUp(MouseButtons button, Point location, bool absoluteLocation = true)
        {
            MouseEventFlags meFlag = 0;
            var mData = 0;
            switch (button)
            {
                case MouseButtons.Left:
                    meFlag = MouseEventFlags.LeftUp;
                    break;
                case MouseButtons.Right:
                    meFlag = MouseEventFlags.RightUp;
                    break;
                case MouseButtons.Middle:
                    meFlag = MouseEventFlags.MiddleUp;
                    break;
                case MouseButtons.XButton1:
                    meFlag = MouseEventFlags.XUp;
                    mData = 1;
                    break;
                case MouseButtons.XButton2:
                    meFlag = MouseEventFlags.XUp;
                    mData = 2;
                    break;
            }
            if (absoluteLocation)
                meFlag |= MouseEventFlags.Absolute;
            var mInput = new MouseInput { dx = location.X, dy = location.Y, mouseData = mData, dwFlags = meFlag };
            SendMouseInput(ref mInput);
        }

        public static void SendMouseUp(MouseButtons button /*, bool absoluteLocation = true*/)
        {
            SendMouseUp(button, Cursor.GetCursorPos(), /*absoluteLocation*/true);
        }

        public static void SendMouseWheel(int wheelCount, Point location, bool absoluteLocation)
        {
            var meFlag = MouseEventFlags.Wheel;
            if (absoluteLocation)
                meFlag |= MouseEventFlags.Absolute;
            var mInput = new MouseInput { dx = location.X, dy = location.Y, dwFlags = meFlag, mouseData = wheelCount };
            SendMouseInput(ref mInput);
        }

        public static void SendMouseWheel(int wheelCount)
        {
            SendMouseWheel(wheelCount, Cursor.GetCursorPos(), true);
        }
        #endregion


        #region Implementation
        private static void SendMouseInput(ref MouseInput mouseInput)
        {
            var iUnion = new InputUnion { mi = mouseInput };
            var input = new Input { type = InputTypes.Mouse, U = iUnion };
            var inputs = new[] { input };
            if (Keyboard.SendInput(1, inputs, Marshal.SizeOf(input)) != 1)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private static void SendMouseInput(params MouseInput[] mouseInputs)
        {
            var length = mouseInputs.Length;
            if (length <= 0) return;

            var inputs =
                mouseInputs.Select(m => new Input { type = InputTypes.Mouse, U = new InputUnion { mi = m } }).ToArray();
            if (Keyboard.SendInput(length, inputs, Marshal.SizeOf(inputs[0])) != length)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        #endregion
    }
}