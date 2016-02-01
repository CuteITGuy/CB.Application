using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GC.WinAPI
{
    public static class DeviceContext
    {
        #region Import
        /// <summary>
        /// Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc">A handle to an existing DC. If this handle is NULL, the function creates a memory 
        /// DC compatible with the application's current screen.</param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to a memory DC.
        /// If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        /// <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources
        /// associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns> If the function succeeds, the return value is nonzero.
        /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.</returns>
        /// <remarks> Do not delete a drawing object (pen or brush) while it is still selected into a DC.
        /// When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must
        /// be deleted independently.
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd183539%28v=vs.85%29.aspx </remarks>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified 
        /// window or for the entire screen. You can use the returned handle in subsequent GDI functions to draw
        /// in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
        /// <para>The GetDCEx function is an extension to GetDC, which gives an application more control over 
        /// how and whether clipping occurs in the client area.</para>
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC 
        /// retrieves the DC for the entire screen.</param>
        /// <returns>If the function succeeds, the return value is a handle to the DC for the specified window's
        /// client area.
        /// If the function fails, the return value is NULL.</returns>
        /// <remarks>The GetDC function retrieves a common, class, or private DC depending on the class style of
        /// the specified window. For class and private DCs, GetDC leaves the previously assigned attributes 
        /// unchanged. However, for common DCs, GetDC assigns default attributes to the DC each time it is 
        /// retrieved. For example, the default font is System, which is a bitmap font. Because of this, the
        /// handle to a common DC returned by GetDC does not tell you what font, color, or brush was used when
        /// the window was drawn. To determine the font, call GetTextFace.
        /// Note that the handle to the DC can only be used by a single thread at any one time.
        /// After painting with a common DC, the ReleaseDC function must be called to release the DC. Class and 
        /// private DCs do not have to be released. ReleaseDC must be called from the same thread that called
        /// GetDC. The number of DCs is limited only by available memory. </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications. The 
        /// effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. It 
        /// has no effect on class or private DCs.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
        /// <param name="hDC">A handle to the DC to be released.</param>
        /// <returns>The return value indicates whether the DC was released. If the DC was released, the return 
        /// value is 1.
        /// If the DC was not released, the return value is zero.</returns>
        /// <remarks>The application must call the ReleaseDC function for each call to the GetWindowDC function 
        /// and for each call to the GetDC function that retrieves a common DC.
        /// An application cannot use the ReleaseDC function to release a DC that was created by calling the 
        /// CreateDC function; instead, it must use the DeleteDC function. ReleaseDC must be called from the 
        /// same thread that called GetDC.
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd162920%28v=vs.85%29.aspx </remarks>
        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>Selects an object into the specified device context (DC). The new object replaces the
        /// previous object of the same type.</summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="hgdiobj">A handle to the object to be selected.</param>
        /// <returns> If the selected object is not a region and the function succeeds, the return value is a 
        ///  handle to the object being replaced. If the selected object is a region and the function succeeds,
        ///  the return value is one of the following values.
        ///  SIMPLEREGION - Region consists of a single rectangle.
        ///  COMPLEXREGION - Region consists of more than one rectangle.
        ///  NULLREGION - Region is empty.
        ///  If an error occurs and the selected object is not a region, the return value is <c>NULL</c>.
        ///  Otherwise, it is <c>HGDI_ERROR</c>.</returns>
        /// <remarks>This function returns the previously selected object of the specified type. An application
        /// should always replace a new object with the original, default object after it has finished drawing 
        /// with the new object.An application cannot select a single bitmap into more than one DC at a time.
        /// ICM: If the object being selected is a brush or a pen, color management is performed.
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd162957%28v=vs.85%29.aspx </remarks>
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);
        #endregion
    }
}
