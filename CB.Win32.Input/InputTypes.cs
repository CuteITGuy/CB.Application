namespace CB.Win32.Inputs
{
    /// <summary>
    ///     The type of the input event.
    /// </summary>
    public enum InputTypes
    {
        /// <summary>
        ///     The event is a mouse event. Use the mi structure of the union.
        /// </summary>
        Mouse = 0,

        /// <summary>
        ///     The event is a keyboard event. Use the ki structure of the union.
        /// </summary>
        Keyboard = 1,

        /// <summary>
        ///     The event is a hardware event. Use the hi structure of the union.
        /// </summary>
        Hardward = 2,
    }
}