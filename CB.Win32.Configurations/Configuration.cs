using System;
using System.Runtime.InteropServices;


namespace CB.Win32.Configurations
{
    public static class Configuration
    {
        #region Import
        /// <summary>
        ///     Retrieves or sets the value of one of the system-wide parameters. This function can also update the user
        ///     profile while setting a parameter.
        /// </summary>
        /// <param name="uiAction">The system-wide parameter to be retrieved or set.</param>
        /// <param name="uiParam">
        ///     A parameter whose usage and format depends on the system parameter being queried
        ///     or set. For more information about system-wide parameters, see the uiAction parameter. If not otherwise
        ///     indicated, you must specify zero for this parameter.
        /// </param>
        /// <param name="pvParam">
        ///     A parameter whose usage and format depends on the system parameter being queried
        ///     or set. For more information about system-wide parameters, see the uiAction parameter. If not otherwise
        ///     indicated, you must specify NULL for this parameter. For information on the PVOID datatype, see Windows
        ///     Data Types http://msdn.microsoft.com/en-us/library/windows/desktop/aa383751(v=vs.85).aspx
        /// </param>
        /// <param name="fWinIni">
        ///     If a system parameter is being set, specifies whether the user profile is to be
        ///     updated, and if so, whether the WM_SETTINGCHANGE message is to be broadcast to all top-level windows to
        ///     notify them of the change.
        /// </param>
        /// <returns>
        ///     f the function succeeds, the return value is a nonzero value.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     This function is intended for use with applications that allow the user to customize the
        ///     environment.
        ///     A keyboard layout name should be derived from the hexadecimal value of the language identifier corresponding
        ///     to the layout. For example, U.S. English has a language identifier of 0x0409, so the primary U.S. English
        ///     layout is named "00000409". Variants of U.S. English layout, such as the Dvorak layout, are named
        ///     "00010409", "00020409" and so on. For a list of the primary language identifiers and sublanguage
        ///     identifiers that make up a language identifier, see the MAKELANGID macro.
        ///     There is a difference between the High Contrast color scheme and the High Contrast Mode. The High
        ///     Contrast color scheme changes the system colors to colors that have obvious contrast; you switch to
        ///     this color scheme by using the Display Options in the control panel. The High Contrast Mode, which uses
        ///     SPI_GETHIGHCONTRAST and SPI_SETHIGHCONTRAST, advises applications to modify their appearance for
        ///     visually-impaired users. It involves such things as audible warning to users and customized color scheme
        ///     (using the Accessibility Options in the control panel). For more information, see HIGHCONTRAST. For
        ///     more information on general accessibility features, see Accessibility.
        ///     During the time that the primary button is held down to activate the Mouse ClickLock feature, the user
        ///     can move the mouse. After the primary button is locked down, releasing the primary button does not result
        ///     in a WM_LBUTTONUP message. Thus, it will appear to an application that the primary button is still down.
        ///     Any subsequent button message releases the primary button, sending a WM_LBUTTONUP message to the
        ///     application, thus the button can be unlocked programmatically or through the user clicking any button.
        ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SystemParameters uiAction, int uiParam, IntPtr pvParam,
            SystemParameterFlags fWinIni);
        #endregion
    }
}