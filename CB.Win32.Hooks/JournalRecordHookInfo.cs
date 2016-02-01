using System;
using System.Drawing;
using System.Runtime.InteropServices;
using CB.Win32.Inputs;
using CB.Win32.Messages;


namespace CB.Win32.Hooks
{
    public class JournalRecordHookInfo: HookInfo
    {
        #region Fields
        protected EventMessage _eventMessage;
        #endregion


        #region  Constructors & Destructor
        /*protected WindowsMessages _message;


        protected int paramL;


        protected int paramH;


        protected int _time;


        protected IntPtr _handle;*/

        public JournalRecordHookInfo(int nCode, int wParam, int lParam)
            : base(nCode, wParam, lParam)
        {
            EventMessage = (EventMessage)Marshal.PtrToStructure((IntPtr)lParam, typeof(EventMessage));
            /*Message = (WindowsMessages)info.Message;
            ParamL = info.ParamL;
            ParamH = info.ParamH;
            Time = info.Time;
            Handle = info.Handle;*/
        }
        #endregion


        #region  Properties & Indexers
        public virtual EventMessage EventMessage
        {
            get { return _eventMessage; }
            protected set { _eventMessage = value; }
        }

        public virtual bool ExtendedKey
        {
            get
            {
                if (IsKeyboardMessage)
                    return (EventMessage.ParamH & 0x8000) == 0x8000;
                throw new InvalidOperationException("Not a keyboard _message");
            }
        }

        public virtual bool IsKeyboardMessage => EventMessage.Message >= (int)WindowsMessages.KeyFirst &&
                                                 EventMessage.Message <= (int)WindowsMessages.KeyLast;

        /*public virtual WindowsMessages Message
        {
            get { return _message; }
            protected set { _message = value; }
        }
        public virtual int ParamL
        {
            get { return paramL; }
            protected set { paramL = value; }
        }
        public virtual int ParamH
        {
            get { return paramH; }
            protected set { paramH = value; }
        }
        public virtual int Time
        {
            get { return _time; }
            protected set { _time = value; }
        }
        public virtual IntPtr Handle
        {
            get { return _handle; }
            protected set { _handle = value; }
        }*/

        public virtual bool IsMouseMessage => EventMessage.Message >= (int)WindowsMessages.MouseFirst &&
                                              EventMessage.Message <= (int)WindowsMessages.MouseLast;

        public virtual VirtualKeys KeyCode
        {
            get
            {
                if (IsKeyboardMessage)
                    return (VirtualKeys)(EventMessage.ParamL & 0x000000ff);
                throw new InvalidOperationException("Not a keyboard _message");
            }
        }

        public virtual Point Location
        {
            get
            {
                if (IsMouseMessage)
                    return new Point(EventMessage.ParamL, EventMessage.ParamH);
                throw new InvalidOperationException("Not a mouse _message");
            }
        }

        public virtual ScanCodes ScanCode
        {
            get
            {
                if (IsKeyboardMessage)
                    return (ScanCodes)(EventMessage.ParamH & 0x000000ff);
                throw new InvalidOperationException("Not a keyboard _message");
            }
        }
        #endregion
    }
}