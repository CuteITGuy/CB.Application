using System;


namespace CB.Win32.Windows
{
    /// <summary>
    ///     Enumeration used in AnimateWindow Function
    /// </summary>
    [Flags]
    public enum AnimateWindowFlags
    {
        /// <summary>
        ///     Animates the window from left to right. This flag can be used with roll or slide animation.
        ///     It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        LeftToRight = 0x00000001,

        /// <summary>
        ///     Animates the window from right to left. This flag can be used with roll or slide animation.
        ///     It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        RightToLeft = 0x00000002,

        /// <summary>
        ///     Animates the window from top to bottom. This flag can be used with roll or slide animation.
        ///     It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        TopToBottom = 0x00000004,

        /// <summary>
        ///     Animates the window from bottom to top. This flag can be used with roll or slide animation.
        ///     It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        BottomToTop = 0x00000008,

        /// <summary>
        ///     Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the
        ///     AW_HIDE is not used. The various direction flags have no effect.
        /// </summary>
        Center = 0x00000010,

        /// <summary>
        ///     Hides the window. By default, the window is shown.
        /// </summary>
        Hide = 0x00010000,

        /// <summary>
        ///     Activates the window. Do not use this value with AW_HIDE.
        /// </summary>
        Activate = 0x00020000,

        /// <summary>
        ///     Uses slide animation. By default, roll animation is used. This flag is ignored when used with AW_CENTER.
        /// </summary>
        Slide = 0x00040000,

        /// <summary>
        ///     Uses a fade effect. This flag can be used only if hwnd is a top-level window.
        /// </summary>
        Blend = 0x00080000
    }
}