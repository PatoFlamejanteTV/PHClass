// This code is using the GDI+ framework
using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class GraphicsHandler
{
    [DllImport("gdi32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

    [DllImport("gdi32.dll")]
    private static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll")]
    private static extern void ReleaseDC(IntPtr hWnd, IntPtr hdc);

    private const uint PATINVERT = 0x005A0049; // Raster operation code for PATINVERT

    public static void ClipAndBitBlt(int x, int y, int width, int height)
    {
        IntPtr deviceContext = GetDC(IntPtr.Zero);
        IntPtr regionHandle = CreateEllipticRgn(x, y, width + x, height + y);
        SelectClipRgn(deviceContext, regionHandle);
        BitBlt(deviceContext, x, y, width, height, deviceContext, x, y, PATINVERT);
        DeleteObject(regionHandle);
        ReleaseDC(IntPtr.Zero, deviceContext);
    }
}