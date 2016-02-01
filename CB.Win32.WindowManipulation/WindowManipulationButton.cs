using CB.Win32.Messages;


namespace CB.Win32.WindowManipulation
{
    public enum WindowManipulationButton: uint
    {
        LeftButton = WindowsMessages.LeftButtonDown,
        RightButton = WindowsMessages.RightButtonDown,
        MiddleButton = WindowsMessages.MiddleButtonDown,
        XButton = WindowsMessages.XButtonDown
    }
}