namespace CB.Win32.Hooks
{
    public class HookInfo
    {
        #region  Constructors & Destructor


        #region Constructors
        public HookInfo(int nCode, int wParam, int lParam)
        {
            this.nCode = nCode;
            this.wParam = wParam;
            this.lParam = lParam;
        }
        #endregion


        #endregion


        #region  Properties & Indexers
        public bool Handled { get; set; }
        public int lParam { get; private set; }
        public int nCode { get; private set; }
        public int wParam { get; private set; }
        #endregion
    }
}