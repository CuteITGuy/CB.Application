using System;


namespace CB.Win32.Hooks
{
    [Flags]
    public enum MouseActions
    {
        None = 0,
        Move = 1,
        Down = 2,
        Up = 4,
        Click = Down | Up,
        DoubleClick = 8,
        Wheel = 16
    }
}