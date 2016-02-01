using System;


namespace CB.Win32.Windows
{
    /// <summary>
    ///     For use with ChildWindowFromPointEx
    /// </summary>
    [Flags]
    public enum WindowFromPointFlags
    {
        /// <summary>
        ///     Does not skip any child windows
        /// </summary>
        All = 0x0000,

        /// <summary>
        ///     Skips invisible child windows
        /// </summary>
        SkipInvisible = 0x0001,

        /// <summary>
        ///     Skips disabled child windows
        /// </summary>
        SkipDisabled = 0x0002,

        /// <summary>
        ///     Skips transparent child windows
        /// </summary>
        SkipTransparent = 0x0004
    }
}