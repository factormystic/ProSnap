using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ProSnap
{
    public static class Helper
    {
        internal static string ExpandParameters(string p, ExtendedScreenshot ss)
        {
            if (string.IsNullOrEmpty(p))
                return string.Empty;

            p = p.Replace(":yyyy", ss.Date.ToString("yyyy"));
            p = p.Replace(":yy", ss.Date.ToString("yy"));

            p = p.Replace(":MMMM", ss.Date.ToString("MMMM"));
            p = p.Replace(":MMM", ss.Date.ToString("MMM"));
            p = p.Replace(":MM", ss.Date.ToString("MM"));
            p = p.Replace(":M", ss.Date.ToString("M"));

            p = p.Replace(":dddd", ss.Date.ToString("dddd"));
            p = p.Replace(":ddd", ss.Date.ToString("ddd"));
            p = p.Replace(":dd", ss.Date.ToString("dd"));
            p = p.Replace(":d", ss.Date.ToString("d"));

            p = p.Replace(":hh", ss.Date.ToString("hh"));
            p = p.Replace(":HH", ss.Date.ToString("HH"));

            p = p.Replace(":mm", ss.Date.ToString("mm"));
            p = p.Replace(":m", ss.Date.ToString("m"));

            p = p.Replace(":ss", ss.Date.ToString("ss"));
            p = p.Replace(":s", ss.Date.ToString("s"));

            p = p.Replace(":tt", ss.Date.ToString("tt"));
            p = p.Replace(":t", ss.Date.ToString("t"));

            p = p.Replace(":zzz", ss.Date.ToString("zzz"));
            p = p.Replace(":zz", ss.Date.ToString("zz"));

            p = p.Replace(":w", ss.WindowTitle);
            
            return p;
        }

        internal static ImageFormat ExtToImageFormat(string p)
        {
            switch (p.ToLower())
            {
                case ".jpeg":
                case ".jpg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                default: return ImageFormat.Png;
            }
        }

        internal static bool isMouseOver(this Control c)
        {
            return c.ClientRectangle.Contains(c.PointToClient(Cursor.Position));
        }
    }
}
