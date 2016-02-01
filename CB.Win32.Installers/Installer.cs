using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using IWshRuntimeLibrary;
using File = System.IO.File;


namespace CB.Win32.Installers
{
    public static class Installer
    {
        #region Import
        /// <summary>
        ///     The MsiGetShortcutTarget function examines a shortcut and returns its product, feature name, and
        ///     component if available. MsiGetShortcutTargetW is the Unicode version of MsiGetShortcutTarget.
        /// </summary>
        /// <param name="szShortcutTarget">A null-terminated string specifying the full path to a shortcut.</param>
        /// <param name="szProductCode">
        ///     A GUID for the product code of the shortcut. This string buffer must be 39
        ///     characters long. The first 38 characters are for the GUID, and the last character is for the terminating
        ///     null character. This parameter can be null.
        /// </param>
        /// <param name="szFeatureId">
        ///     The feature name of the shortcut. The string buffer must be
        ///     MAX_FEATURE_CHARS+1 characters long. This parameter can be null.
        /// </param>
        /// <param name="szComponentCode">
        ///     A GUID of the component code. This string buffer must be 39 characters
        ///     long. The first 38 characters are for the GUID, and the last character is for the terminating null
        ///     character. This parameter can be null.
        /// </param>
        /// <returns>
        ///     ERROR_SUCCESS = 0 (0x0)        The function succeeded.
        ///     ERROR_FUNCTION_FAILED = 1627 (0x65B)    The function failed.
        /// </returns>
        /// <remarks>
        ///     If the function fails, and the shortcut exists, the regular contents of the shortcut may be
        ///     accessed through the IShellLink interface. http://msdn.microsoft.com/en-us/library/bb774950(v=vs.85).aspx
        ///     Otherwise, the state of the target may be determined by using the Installer Selection Functions.
        ///     http://msdn.microsoft.com/en-us/library/aa368250(v=vs.85).aspx#_msi_installer_selection_functions
        ///     http://msdn.microsoft.com/en-us/library/aa370299%28v=vs.85%29.aspx
        /// </remarks>
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        public static extern uint MsiGetShortcutTargetW(string szShortcutTarget,
            [Out] StringBuilder szProductCode, [Out] StringBuilder szFeatureId, [Out] StringBuilder szComponentCode);
        #endregion


        #region Methods
        /// <summary>
        ///     Retrieves target path from the given shortcut file. Shortcut file is a file with extension ".lnk"
        /// </summary>
        /// <param name="shortcutFile">Full path or relative path of the shortcut file.</param>
        /// <returns>The target path if the function succeeded.</returns>
        public static string GetShortcutTargetPath(string shortcutFile)
        {
            /* TODO Add implementations
             * See https://astoundingprogramming.wordpress.com/2012/12/17/how-to-get-the-target-of-a-windows-shortcut-c/
             * See http://stackoverflow.com/questions/13079569/how-do-i-get-the-path-name-from-a-file-shortcut-getting-exception
             */

            if (IsShortcutFile(shortcutFile))
            {
                var shell = new WshShell();
                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutFile);
                return shortcut.TargetPath;
            }
            throw new ArgumentException(shortcutFile);
        }

        /// <summary>
        ///     Checks whether the given path exists and is of shortcut file type. Shortcut files end with ".lnk"
        /// </summary>
        /// <param name="path">Full path or relative path of the file.</param>
        /// <returns>True if the given path exists and is of shortcut file type; otherwise, false.</returns>
        public static bool IsShortcutFile(string path)
        {
            return File.Exists(path) && Path.GetExtension(path)?.ToLower() == ".lnk";
        }
        #endregion
    }
}