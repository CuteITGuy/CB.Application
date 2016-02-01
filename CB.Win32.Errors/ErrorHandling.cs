using System.Runtime.InteropServices;


namespace CB.Win32.Errors
{
    public class ErrorHandling
    {
        #region Import
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern int GetLastError();
        #endregion
    }
}