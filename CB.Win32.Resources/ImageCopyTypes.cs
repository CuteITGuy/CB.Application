namespace CB.Win32.Resources
{
    /// <summary>
    /// </summary>
    public enum ImageCopyTypes
    {
        /// <summary>
        ///     The default flag; it does nothing. All it means is "not LR_MONOCHROME".
        /// </summary>
        DefaultColor = 0x0000,

        /// <summary>
        ///     Creates a new monochrome image.
        /// </summary>
        Monochrome = 0x00000001,

        /// <summary>
        ///     Returns the original hImage if it satisfies the criteria for the copy—that is, correct dimensions and color
        ///     depth—in which case the CopyDeleteOrg flag is ignored. If this flag is not specified, a new object is always
        ///     created.
        /// </summary>
        CopyReturnOrg = 0x00000004,

        /// <summary>
        ///     Deletes the original image after creating the copy.
        /// </summary>
        CopyDeleteOrg = 0x00000008,

        /// <summary>
        ///     Specifies the image to load. If the hinst parameter is non-NULL and the fuLoad parameter omits
        ///     LR_LOADFROMFILE, lpszName specifies the image resource in the hinst module. If the image resource is to
        ///     be loaded by name, the lpszName parameter is a pointer to a null-terminated string that contains the name of
        ///     the image resource. If the image resource is to be loaded by ordinal, use the MAKEINTRESOURCE macro to
        ///     convert the image ordinal into a form that can be passed to the LoadImage function.
        ///     <para>
        ///         If the hinst
        ///         parameter is NULL and the fuLoad parameter omits the LR_LOADFROMFILE value, the lpszName specifies the OEM
        ///         image to load. The OEM image identifiers are defined in Winuser.h and have the following prefixes.
        ///     </para>
        ///     <para>OBM_ OEM bitmaps</para>
        ///     <para>OIC_ OEM icons</para>
        ///     <para>OCR_ OEM cursors</para>
        ///     <para>
        ///         To pass these constants to the LoadImage function, use the MAKEINTRESOURCE macro. For example, to load
        ///         the OCR_NORMAL cursor, pass MAKEINTRESOURCE(OCR_NORMAL) as the lpszName parameter and NULL as the hinst
        ///         parameter.
        ///     </para>
        ///     <para>
        ///         If the fuLoad parameter includes the LR_LOADFROMFILE value, lpszName is the name of the file that
        ///         contains the image.
        ///     </para>
        /// </summary>
        LoadFromFile = 0x0010,

        /// <summary>
        ///     Retrieves the color value of the first pixel in the image and replaces the corresponding entry in
        ///     the color table with the default window color (COLOR_WINDOW). All pixels in the image that use that entry
        ///     become the default window color.
        ///     <para>
        ///         This value applies only to images that have corresponding color tables.
        ///         Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
        ///     </para>
        ///     <para>
        ///         If fuLoad includes both the LR_LOADTRANSPARENT and LR_LOADMAP3DCOLORS values, LRLOADTRANSPARENT takes
        ///         precedence.
        ///     </para>
        ///     <para>However, the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.</para>
        /// </summary>
        LoadTransparent = 0x0020,

        /// <summary>
        ///     Uses the width or height specified by the system metric values for cursors or icons, if the cxDesired or cyDesired
        ///     values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero, the function
        ///     uses the actual resource size. If the resource contains multiple images, the function uses the size of the first
        ///     image.
        /// </summary>
        DefaultSize = 0x00000040,

        /// <summary>
        ///     Uses true VGA colors.
        /// </summary>
        VGAColor = 0x0080,

        /// <summary>
        ///     Searches the color table for the image and replaces the following shades of gray with the
        ///     corresponding 3-D color: Color Replaced with
        ///     Dk Gray, RGB(128,128,128) COLOR_3DSHADOW
        ///     Gray, RGB(192,192,192) COLOR_3DFACE
        ///     Lt Gray, RGB(223,223,223) COLOR_3DLIGHT
        ///     Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
        /// </summary>
        LoadMap3DColors = 0x1000,

        /// <summary>
        ///     If this is set and a new bitmap is created, the bitmap is created as a DIB section. Otherwise, the bitmap
        ///     image is created as a device-dependent bitmap. This flag is only valid if uType is IMAGE_BITMAP.
        /// </summary>
        CreateDIBSection = 0x00002000,

        /// <summary>
        ///     Tries to reload an icon or cursor resource from the original resource file rather than simply copying the
        ///     current image. This is useful for creating a different-sized copy when the resource file contains multiple
        ///     sizes of the resource. Without this flag, CopyImage stretches the original image to the new size. If this
        ///     flag is set, CopyImage uses the size in the resource file closest to the desired size. This will succeed
        ///     only if hImage was loaded by LoadIcon or LoadCursor, or by LoadImage with the SHARED flag.
        /// </summary>
        CopyFromResource = 0x00004000,

        /// <summary>
        ///     Shares the image handle if the image is loaded multiple times. If LR_SHARED is not set, a second
        ///     call to LoadImage for the same resource will load the image again and return a different handle.
        ///     <para>
        ///         When you use this flag, the system will destroy the resource when it is no longer needed.
        ///     </para>
        ///     <para>
        ///         Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that
        ///         are loaded from a file.
        ///     </para>
        ///     <para>
        ///         When loading a system icon or cursor, you must use LR_SHARED or the
        ///         function will fail to load the resource.
        ///     </para>
        ///     <para>
        ///         Windows 95/98/Me: The function finds the first image
        ///         with the requested resource name in the cache, regardless of the size requested.
        ///     </para>
        ///     ///
        /// </summary>
        Shared = 0x8000
    }
}