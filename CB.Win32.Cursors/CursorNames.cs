using System;


namespace CB.Win32.Cursors
{
    public enum CursorNames: uint
    {
        /// <summary>
        ///     Standard arrow
        /// </summary>
        Arrow = 32512,

        /// <summary>
        ///     I-beam
        /// </summary>
        IBeam = 32513,

        /// <summary>
        ///     Hourglass
        /// </summary>
        Wait = 32514,

        /// <summary>
        ///     Crosshair
        /// </summary>
        Cross = 32515,

        /// <summary>
        ///     Vertical arrow
        /// </summary>
        UpArrow = 32516,

        [Obsolete("Obsolete for applications marked version 4.0 or later. Use SizeAll instead")]
        Size = 32640,

        [Obsolete("Obsolete for applications marked version 4.0 or later.")]
        Icon = 32641,

        /// <summary>
        ///     Double-pointed arrow pointing northwest and southeast
        /// </summary>
        SizeNWSE = 32642,

        /// <summary>
        ///     Double-pointed arrow pointing northeast and southwest
        /// </summary>
        SizeNESW = 32643,

        /// <summary>
        ///     Double-pointed arrow pointing west and east
        /// </summary>
        SizeWE = 32644,

        /// <summary>
        ///     Double-pointed arrow pointing north and south
        /// </summary>
        SizeNS = 32645,

        /// <summary>
        ///     Four-pointed arrow pointing north, south, east, and west
        /// </summary>
        SizeAll = 32646,

        /// <summary>
        ///     Slashed circle
        /// </summary>
        No = 32648,

        /// <summary>
        ///     Hand
        /// </summary>
        Hand = 32649,

        /// <summary>
        ///     Standard arrow and small hourglass
        /// </summary>
        AppStarting = 32650,

        /// <summary>
        ///     Arrow and question mark
        /// </summary>
        Help = 32651
    }
}