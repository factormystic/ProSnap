using System;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProSnap
{
    public static class Helper
    {
        internal static string ExpandParameters(string p, ExtendedScreenshot ss)
        {
            if (string.IsNullOrEmpty(p))
                return string.Empty;

            return Regex.Replace(p, @":[a-z]+", new MatchEvaluator(m =>
            {
                try
                {
                    switch (m.Value)
                    {
                        case ":w": return ss.WindowTitle;
                        case ":url": return ss.Remote.ImageLink;
                        case ":delete": return ss.Remote.DeleteLink;
                        case ":file": return string.IsNullOrEmpty(ss.SavedFileName) ? ss.InternalFileName : ss.SavedFileName;
                        case ":image": return "";
                        default: return ss.Date.ToString(m.Value.TrimStart(':'));
                    }
                }
                catch (FormatException fe)
                {
                    return m.Value;
                }
            }), RegexOptions.IgnoreCase);
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
