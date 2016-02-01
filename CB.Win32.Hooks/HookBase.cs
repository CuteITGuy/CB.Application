using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using CB.Win32.DynamicLinkLibraries;
using CB.Win32.Threading;


namespace CB.Win32.Hooks
{
    public class HookBase: IDisposable
    {
        #region Fields
        protected const int HC_ACTION = 0, HC_GETNEXT = 1, HC_SKIP = 2, HC_NOREMOVE = 3;
        protected bool _enabled;

        protected IntPtr _handle;
        protected HookProc _procedure;
        protected HookTypes _type;
        #endregion


        #region  Constructors & Destructor
        protected HookBase(HookTypes type)
        {
            Type = type;
        }

        public HookBase(HookTypes type, HookProc proc)
            : this(type)
        {
            Procedure = new HookProc(proc);
        }

        ~HookBase()
        {
            Unhook(false);
        }
        #endregion


        #region  Properties & Indexers
        public virtual bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value && !_enabled)
                    SetHook();
                else if (!value && _enabled)
                    Unhook(true);
            }
        }

        public virtual IntPtr Handle
        {
            get { return _handle; }
            protected set { _handle = value; }
        }

        public virtual HookProc Procedure
        {
            get { return _procedure; }
            protected set { _procedure = value; }
        }

        public object Tag { get; set; }

        public virtual HookTypes Type
        {
            get { return _type; }
            protected set { _type = value; }
        }
        #endregion


        #region Events
        public virtual event HookEventHandler Event;
        #endregion


        #region Methods
        public void Dispose()
        {
            Unhook(true);
        }
        #endregion


        #region Implementation
        protected virtual void SetHook()
        {
            //ModuleHandle module = Assembly.GetExecutingAssembly().GetModules()[0].ModuleHandle;
            //var modHwnd1 = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
            var modHwnd = DynamicLinkLibrary.LoadLibrary(Assembly.GetExecutingAssembly().Location);
            var threaId = _type == HookTypes.Keyboard || _type == HookTypes.Mouse
                              ? ProcessThread.GetCurrentThreadId() : 0;
            if (Handle == IntPtr.Zero &&
                (Handle = Hook.SetWindowsHookEx(Type, Procedure, /*IntPtr.Zero*/modHwnd, threaId)) == IntPtr.Zero)
            {
                Unhook(false);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            _enabled = true;
            GC.ReRegisterForFinalize(this);
        }

        protected virtual void Unhook(bool throwException)
        {
            if (Handle != IntPtr.Zero && !Hook.UnhookWindowsHookEx(Handle) && throwException)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            Handle = IntPtr.Zero;
            _enabled = false;
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}