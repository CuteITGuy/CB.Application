using System;


namespace CB.Win32.Configurations
{
    /// <summary>
    /// </summary>
    [Flags]
    public enum SystemParameterFlags
    {
        /// <summary> Do not update the user profile or broadcast the WM_SETTINGCHANGE message</summary>
        None = 0x00,

        /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
        UpdateInFile = 0x01,

        /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
        SendChange = 0x02,

        /// <summary>Same as SendChange.</summary>
        SendWinIniChange = 0x02
    }
}