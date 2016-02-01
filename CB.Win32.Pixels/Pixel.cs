using System.Runtime.InteropServices;


namespace CB.Win32.Pixels
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Pixel
    {
        [FieldOffset(0)]
        internal int code;

        [FieldOffset(2)]
        public byte Blue;

        [FieldOffset(1)]
        public byte Green;

        [FieldOffset(0)]
        public byte Red;

        [FieldOffset(3)]
        public byte Alpha;

        public static implicit operator Pixel(int pixelCode)
        {
            return new Pixel { code = pixelCode };
        }

        public static implicit operator System.Drawing.Color(Pixel pixel)
        {
            return System.Drawing.Color.FromArgb(255, pixel.Red, pixel.Green, pixel.Blue);
        }

        public static implicit operator System.Windows.Media.Color(Pixel pixel)
        {
            return System.Windows.Media.Color.FromArgb(255, pixel.Red, pixel.Green, pixel.Blue);
        }
    }
}