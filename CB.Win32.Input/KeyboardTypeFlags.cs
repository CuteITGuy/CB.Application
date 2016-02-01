namespace CB.Win32.Inputs
{
    /// <summary>
    ///     The type of keyboard information to be retrieved.
    /// </summary>
    public enum KeyboardTypeFlags
    {
        /// <summary>
        ///     Keyboard type
        /// </summary>
        Type = 0,

        /// <summary>
        ///     Keyboard subtype
        /// </summary>
        Subtype = 1,

        /// <summary>
        ///     The number of function keys on the keyboard
        /// </summary>
        FunctionKeys = 2,
    }
}