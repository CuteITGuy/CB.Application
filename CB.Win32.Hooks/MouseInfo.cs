using System;
using System.Drawing;
using CB.Win32.Messages;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Base class of MouseHookInfo and MouseLLHookInfo used in MouseHookEventHandler
    /// </summary>
    public class MouseInfo: HookInfo
    {
        #region Fields
        /// <summary>
        ///     Specifies extra information associated with the _message.
        /// </summary>
        protected IntPtr _extraInfo;

        /// <summary>
        ///     Specifies a Point structure that contains the x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        protected Point _location;

        /*public MouseInfo(int _message, MouseHookStructEx info)
        {
            Message = (WindowsMessages)_message;
            Location = info.MouseHookStruct.Location;
            MouseData = info.MouseData;
            ExtraInfo = info.MouseHookStruct.ExtraInfo;
        }*/
        /*public MouseInfo(int _message, MouseLLHookStruct info)
        {
            Message = (WindowsMessages)_message;
            Location = info.Location;
            MouseData = info.MouseData;
            ExtraInfo = info.ExtraInfo;
        }*/

        /// <summary>
        ///     The identifier of the mouse _message.
        /// </summary>
        protected WindowsMessages _message;

        /// <summary>
        ///     If the _message is WM_MOUSEWHEEL, the HIWORD of this member is the wheel delta. The LOWORD is undefined and
        ///     reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative
        ///     value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as
        ///     WHEEL_DELTA, which is 120.
        ///     <para>
        ///         If the _message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK,
        ///         WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the HIWORD of _mouseData specifies which X button
        ///         was _pressed or released, and the LOWORD is undefined and reserved. This member can be one or more of the
        ///         following values. Otherwise, _mouseData is not used.
        ///     </para>
        ///     <para>XBUTTON1 = 0x0001 The first X button was _pressed or released.</para>
        ///     <para>XBUTTON2 = 0x0002 The second X button was _pressed or released.</para>
        /// </summary>
        protected int _mouseData;
        #endregion


        #region  Constructors & Destructor
        public MouseInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            Message = (WindowsMessages)wParam;
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
        ///     Specifies a Point structure that contains the x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public virtual Point Location
        {
            get { return _location; }
            protected set { _location = value; }
        }

        /// <summary>
        ///     The identifier of the mouse _message.
        /// </summary>
        public virtual WindowsMessages Message
        {
            get { return _message; }
            protected set { _message = value; }
        }

        /// <summary>
        ///     If the _message is WM_MOUSEWHEEL, the HIWORD of this member is the wheel delta. The LOWORD is undefined and
        ///     reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative
        ///     value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as
        ///     WHEEL_DELTA, which is 120.
        ///     <para>
        ///         If the _message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK,
        ///         WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the HIWORD of _mouseData specifies which X button
        ///         was _pressed or released, and the LOWORD is undefined and reserved. This member can be one or more of the
        ///         following values. Otherwise, _mouseData is not used.
        ///     </para>
        ///     <para>XBUTTON1 = 0x0001 The first X button was _pressed or released.</para>
        ///     <para>XBUTTON2 = 0x0002 The second X button was _pressed or released.</para>
        /// </summary>
        public virtual int MouseData
        {
            get { return _mouseData; }
            protected set { _mouseData = value; }
        }
        #endregion
    }
}