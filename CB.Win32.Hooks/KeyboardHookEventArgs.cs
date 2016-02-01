using System;


namespace CB.Win32.Hooks
{
    public class KeyboardHookEventArgs: EventArgs
    {
        #region Fields
        public KeyboardLLHookStruct Info;
        public int Message;
        #endregion
    }
}