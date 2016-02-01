using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains informations if a mouse hook caught by MouseProc
    /// </summary>
    public class MouseHookInfo: MouseInfo
    {
        #region Fields
        /// <summary>
        ///     Handle to the window that will receive the mouse _message corresponding to the mouse event.
        /// </summary>
        protected IntPtr _handle;

        /// <summary>
        ///     Specifies the hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST _message.
        /// </summary>
        protected HitTestValues _hitTestCode;
        #endregion


        #region  Constructors & Destructor
        public MouseHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            var info = (MouseHookStructEx)Marshal.PtrToStructure((IntPtr)lParam, typeof(MouseHookStructEx));
            Location = info.MouseHookStruct.Location;
            MouseData = info.MouseData;
            ExtraInfo = info.MouseHookStruct.ExtraInfo;
            Handle = info.MouseHookStruct.Handle;
            HitTestCode = info.MouseHookStruct.HitTestCode;
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Handle to the window that will receive the mouse _message corresponding to the mouse event.
        /// </summary>
        public virtual IntPtr Handle
        {
            get { return _handle; }
            protected set { _handle = value; }
        }

        /// <summary>
        ///     Specifies the hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST _message.
        /// </summary>
        public virtual HitTestValues HitTestCode
        {
            get { return _hitTestCode; }
            protected set { _hitTestCode = value; }
        }
        #endregion
    }
}