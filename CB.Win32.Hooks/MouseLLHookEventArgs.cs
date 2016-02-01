using System;
using System.Drawing;
using CB.Win32.Inputs;


namespace CB.Win32.Hooks
{
    public class MouseLLHookEventArgs: EventArgs
    {
        #region  Constructors & Destructor
        public MouseLLHookEventArgs(MouseButtons button, MouseActions action, int x, int y, int delta, int message)
        {
            Button = button;
            Action = action;
            Location = new Point(x, y);
            Delta = delta;
            Message = message;
        }
        #endregion


        #region  Properties & Indexers
        public MouseActions Action { get; }
        public MouseButtons Button { get; }
        public int Delta { get; }
        public Point Location { get; }

        public int Message { get; }

        public int X => Location.X;

        public int Y => Location.Y;
        #endregion
    }
}