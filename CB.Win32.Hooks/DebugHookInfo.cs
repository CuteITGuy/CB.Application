using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Hooks
{
    /// <summary>
    ///     Contains informations of a debug hook caught by DebugProc
    /// </summary>
    public class DebugHookInfo: HookInfo
    {
        #region Fields
        /// <summary>
        ///     The _type of hook about to be called.
        /// </summary>
        protected HookTypes _hookType;

        /// <summary>
        ///     A _handle to the thread that installed the debugging filter function.
        /// </summary>
        protected int _installerThreadId;

        /// <summary>
        ///     A _handle to the thread containing the filter function.
        /// </summary>
        protected int _threadId;
        #endregion


        #region  Constructors & Destructor
        /// <summary>
        ///     Initializes a new instance of DebugHookInfo class
        /// </summary>
        /// <param name="nCode">The <c>nCode</c> parameter from DebugProc</param>
        /// <param name="wParam">The <c>wParam</c> parameter from DebugProc</param>
        /// <param name="lParam">The <c>lParam</c> parameter from DebugProc</param>
        public DebugHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            HookType = (HookTypes)wParam;
            var info = (DebugHookStruct)Marshal.PtrToStructure((IntPtr)lParam, typeof(DebugHookStruct));
            ThreadId = info.IdThread;
            InstallerThreadId = info.IdThreadInstaller;
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     The _type of hook about to be called.
        /// </summary>
        public HookTypes HookType
        {
            get { return _hookType; }
            set { _hookType = value; }
        }

        /// <summary>
        ///     A _handle to the thread that installed the debugging filter function.
        /// </summary>
        public int InstallerThreadId
        {
            get { return _installerThreadId; }
            set { _installerThreadId = value; }
        }

        /// <summary>
        ///     A _handle to the thread containing the filter function.
        /// </summary>
        public int ThreadId
        {
            get { return _threadId; }
            set { _threadId = value; }
        }
        #endregion
    }
}