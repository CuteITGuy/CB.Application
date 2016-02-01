using System;


namespace CB.Win32.Windows
{
    /// <summary>
    ///     The flags that control the position of the minimized window and the method by which the window is restored.
    /// </summary>
    [Flags]
    public enum WindowPlacementFlags: uint
    {
        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues,
        ///     the system posts the request to the thread that owns the window. This prevents the calling thread
        ///     from blocking its execution while other threads process the request.
        /// </summary>
        AsyncWindowPlacement = 0x0004,

        /// <summary>
        ///     <para>
        ///         The restored window will be maximized, regardless of whether it was maximized before it was
        ///         minimized. This setting is only valid the next time the window is restored. It does not change the
        ///         default restoration behavior.
        ///     </para>
        ///     <para>This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.</para>
        /// </summary>
        RestoreToMaximized = 0x0002,

        /// <summary>
        ///     <para>The coordinates of the minimized window may be specified.</para>
        ///     <para>This flag must be specified if the coordinates are set in the ptMinPosition member.</para>
        /// </summary>
        SetMinPosition = 0x0001
    }
}