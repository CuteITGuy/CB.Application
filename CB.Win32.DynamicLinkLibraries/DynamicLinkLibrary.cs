using System;
using System.Runtime.InteropServices;


namespace CB.Win32.DynamicLinkLibraries
{
    public static class DynamicLinkLibrary
    {
        #region Import
        /// <summary>
        ///     Loads the specified module into the address space of the calling process. The specified module may cause
        ///     other modules to be loaded.
        ///     <para>For additional load options, use the LoadLibraryEx function.</para>
        /// </summary>
        /// <param name="lpFileName">
        ///     The name of the module. This can be either a library module (a .dll file) or an
        ///     executable module (an .exe file). The name specified is the file name of the module and is not related to
        ///     the name stored in the library module itself, as specified by the LIBRARY keyword in the module-definition
        ///     (.def) file.
        ///     <para>If the string specifies a full path, the function searches only that path for the module.</para>
        ///     <para>
        ///         If the string specifies a relative path or a module name without a path, the function uses a standard
        ///         search strategy to find the module; for more information, see the Remarks.
        ///     </para>
        ///     <para>
        ///         If the function cannot
        ///         find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward
        ///         slashes (/). For more information about paths, see Naming a File or Directory.
        ///     </para>
        ///     <para>
        ///         If the string
        ///         specifies a module name without a path and the file name extension is omitted, the function appends the
        ///         default library extension .dll to the module name. To prevent the function from appending .dll to the module
        ///         name, include a trailing point character (.) in the module name string.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the module.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms684175%28v=vs.85%29.aspx</remarks>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
        #endregion
    }
}