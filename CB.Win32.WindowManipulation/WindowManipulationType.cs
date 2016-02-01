namespace CB.Win32.WindowManipulation
{
    /// <summary>
    ///     Options used in WindowManipulation action to specify the chosen window.
    /// </summary>
    public enum WindowManipulationType
    {
        /// <summary>
        ///     The first window found, including child and parent windows, will be the chosen window.
        /// </summary>
        FirstWindowFound,

        /// <summary>
        ///     Only choose top-level window to be the action target.
        /// </summary>
        TopLevelWindow
    }
}