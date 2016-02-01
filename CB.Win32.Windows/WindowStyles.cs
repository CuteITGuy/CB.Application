using System;


namespace CB.Win32.Windows
{
    /// <summary>
    ///     <para>Window Styles.</para>
    ///     <para>
    ///         The following styles can be specified wherever a window style is required. After the control
    ///         has been created, these styles cannot be modified, except as noted.
    ///     </para>
    /// </summary>
    [Flags]
    public enum WindowStyles: uint
    {
        /// <summary>The window has a thin-line border.</summary>
        Border = 0x800000,

        /// <summary>The window has a title bar (includes the Border style).</summary>
        Caption = 0xc00000,

        /// <summary>
        ///     The window is a child window. A window with this style cannot have a menu bar.
        ///     This style cannot be used with the Popup style.
        /// </summary>
        Child = 0x40000000,

        /// <summary>
        ///     Excludes the area occupied by child windows when drawing occurs within the
        ///     parent window. This style is used when creating the parent window.
        /// </summary>
        ClipChildren = 0x2000000,

        /// <summary>
        ///     Clips child windows relative to each other; that is, when a particular child window receives
        ///     a WM_PAINT message, the ClipSiblings style clips all other overlapping child windows out
        ///     of the region of the child window to be updated. If ClipSiblings is not specified and child
        ///     windows overlap, it is possible, when drawing within the client area of a child window,
        ///     to draw within the client area of a neighboring child window.
        /// </summary>
        ClipSiblings = 0x4000000,

        /// <summary>
        ///     The window is initially disabled. A disabled window cannot receive input from the user.
        ///     To change this after a window has been created, use the EnableWindow function.
        /// </summary>
        Disabled = 0x8000000,

        /// <summary>
        ///     The window has a border of a style typically used with dialog boxes. A window with
        ///     this style cannot have a title bar.
        /// </summary>
        DialogFrame = 0x400000,

        /// <summary>
        ///     The window is the first control of a group of controls. The group consists of this first control
        ///     and all controls defined after it, up to the next control with the Group style.
        ///     The first control in each group usually has the TabStop style so that the user can move from
        ///     group to group. The user can subsequently change the keyboard focus from one control in the group
        ///     to the next control in the group by using the direction keys.
        ///     You can turn this style on and off to change dialog box navigation. To change this style after
        ///     a window has been created, use the SetWindowLong function.
        /// </summary>
        Group = 0x20000,

        /// <summary>The window has a horizontal scroll bar.</summary>
        HorizoltalScrollBar = 0x100000,

        /// <summary>The window is initially maximized.</summary>
        Maximize = 0x1000000,

        /// <summary>
        ///     The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style.
        ///     The SystemMenu style must also be specified.
        /// </summary>
        MaximizeBox = 0x10000,

        /// <summary>The window is initially minimized.</summary>
        Minimize = 0x20000000,

        /// <summary>
        ///     The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style.
        ///     The SystemMenu style must also be specified.
        /// </summary>
        MinimizeBox = 0x20000,

        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
        Overlapped = 0x0,

        /// <summary>The window is an overlapped window.</summary>
        OverlappedWindow = Overlapped | Caption | SystemMenu | SizeFrame | MinimizeBox | MaximizeBox,

        /// <summary>The window is a pop-up window. This style cannot be used with the Child style.</summary>
        Popup = 0x80000000u,

        /// <summary>
        ///     The window is a pop-up window. The Caption and PopupWindow styles must be combined
        ///     to make the window menu visible.
        /// </summary>
        PopupWindow = Popup | Border | SystemMenu,

        /// <summary>The window has a sizing border.</summary>
        SizeFrame = 0x40000,

        /// <summary>The window has a window menu on its title bar. The Caption style must also be specified.</summary>
        SystemMenu = 0x80000,

        /// <summary>
        ///     <para>
        ///         The window is a control that can receive the keyboard focus when the user presses the TAB key.
        ///         Pressing the TAB key changes the keyboard focus to the next control with the TabStop style.
        ///     </para>
        ///     <para>
        ///         You can turn this style on and off to change dialog box navigation. To change this style after
        ///         a window has been created, use the SetWindowLong function. For user-created windows and modeless
        ///         dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
        ///     </para>
        /// </summary>
        TabStop = 0x10000,

        /// <summary>
        ///     The window is initially visible. This style can be turned on and off by using the
        ///     ShowWindow or SetWindowPos function.
        /// </summary>
        Visible = 0x10000000,

        /// <summary>The window has a vertical scroll bar.</summary>
        VerticalScrollBar = 0x200000
    }
}