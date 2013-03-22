using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ProSnap.ActionItems
{
    class EndScrollingScreenshotAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.EndScrollingScreenshot; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new EndScrollingScreenshotAction();
        }

        #endregion

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            if (!Program.isTakingScrollingScreenshot)
                return null;

            Program.isTakingScrollingScreenshot = false;

            Bitmap final = null;

            Bitmap b = new Bitmap(Program.timelapse.First().BaseScreenshotImage.Width, Program.timelapse.First().BaseScreenshotImage.Height * Program.timelapse.Count);
            Graphics g = Graphics.FromImage(b);
            int yoffset = 0;
            int working_height = 0;

            int ignore_header = 100;
            int ignore_footer = 10;

            var offsets = new Dictionary<int, int>();

            //foreach (var ss in timelapse)
            for (int c = 0; c < Program.timelapse.Count; c++)
            {
                var debug = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"prosnap-debug");
                if (Directory.Exists(debug))
                {
                    Program.timelapse[c].BaseScreenshotImage.Save(Path.Combine(debug, @"ss-" + c + ".png"), ImageFormat.Png);
                }

                if (c == 0)
                {
                    g.DrawImageUnscaled(Program.timelapse[c].BaseScreenshotImage, new Point(0, 0));
                    working_height = Program.timelapse[c].BaseScreenshotImage.Height;
                    offsets.Add(c, Program.timelapse[c].BaseScreenshotImage.Height);
                }
                else
                {
                    int old_yoffset = yoffset;
                    yoffset = GetYOffset(Program.timelapse[c - 1].BaseScreenshotImage, Program.timelapse[c].BaseScreenshotImage, ignore_header, ignore_footer, old_yoffset, working_height, c);

                    offsets.Add(c, yoffset);

                    int sum = 0;
                    if (offsets.Count > 0)
                        for (int k = offsets.First().Key; k <= offsets.Last().Key; k++)
                            sum += offsets[k];

                    working_height = sum;

                    g.DrawImage(Program.timelapse[c].BaseScreenshotImage, new RectangleF(0, working_height - (Program.timelapse[c].BaseScreenshotImage.Height - ignore_header), Program.timelapse[c].BaseScreenshotImage.Width, Program.timelapse[c].BaseScreenshotImage.Height - ignore_header), new RectangleF(0, ignore_header, Program.timelapse[c].BaseScreenshotImage.Width, Program.timelapse[c].BaseScreenshotImage.Height - ignore_header), GraphicsUnit.Pixel);
                }

                g.Flush();
                g.Save();

                final = new Bitmap(b.Width, working_height);
                Graphics gfinal = Graphics.FromImage(final);
                gfinal.DrawImage(b, new Rectangle(0, 0, final.Width, final.Height), new Rectangle(0, 0, b.Width, working_height), GraphicsUnit.Pixel);

                gfinal.Flush();
                gfinal.Save();

                if (Directory.Exists(debug))
                {
                    final.Save(Path.Combine(debug, @"final-" + c + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            Program.timelapse.First().ReplaceWithBitmap(final);
            Program.History.Add(Program.timelapse.First());
            Program.Preview.GroomBackForwardIcons();

            return Program.timelapse.First();
        }


        private static int GetYOffset(Bitmap previous, Bitmap current, int ignore_header, int ignore_footer, int last_offset, int previous_working_height, int n)
        {
            BitmapData previous_bmd = previous.LockBits(new Rectangle(0, ignore_header, previous.Width, previous.Height - ignore_header - ignore_footer), ImageLockMode.ReadOnly, previous.PixelFormat);
            BitmapData current_bmd = current.LockBits(new Rectangle(0, ignore_header, current.Width, current.Height - ignore_header - ignore_footer), ImageLockMode.ReadOnly, current.PixelFormat);

            int current_byte_count = Math.Abs(current_bmd.Stride) * current.Height;
            byte[] current_bytes = new byte[current_byte_count];
            //Marshal.Copy(current_bmd.Scan0, current_bytes, 0, current_byte_count);

            int bpp = current_bmd.Stride / current_bmd.Width;
            int offset = 0;
            long current_ptr = current_bmd.Scan0.ToInt32();//.ToInt64();
            for (int i = 0; i < current_bmd.Height; i++)
            {
                Marshal.Copy(new IntPtr((int)current_bmd.Scan0 + current_bmd.Stride * i), current_bytes, current_bmd.Width * bpp * i, current_bmd.Width * bpp);

            }




            int previous_byte_count = Math.Abs(previous_bmd.Stride) * previous_bmd.Height;
            byte[] previous_bytes = new byte[previous_byte_count];
            //Marshal.Copy(previous_bmd.Scan0, previous_bytes, 0, previous_byte_count);


            bpp = previous_bmd.Stride / previous_bmd.Width;
            offset = 0;
            long previous_ptr = previous_bmd.Scan0.ToInt64();
            for (int i = 0; i < previous_bmd.Height; i++)
            {
                Marshal.Copy(new IntPtr((int)previous_bmd.Scan0 + previous_bmd.Stride * i), previous_bytes, previous_bmd.Width * bpp * i, previous_bmd.Width * bpp);
            }


            previous.UnlockBits(previous_bmd);
            current.UnlockBits(current_bmd);

            int scan_window_height = 10;
            var score = new Dictionary<int, int>();

            for (int orig_y = previous_bmd.Height - scan_window_height - 1; orig_y >= 0; orig_y--)
            {
                //byte[] previous_window_section = new byte[previous_bmd.Stride * scan_window_height];
                //Array.Copy(previous_bytes, previous_bmd.Stride * orig_y, previous_window_section, 0, previous_window_section.Length);

                score.Add(orig_y, GetScore(previous_bytes.Skip(previous_bmd.Stride * orig_y).Take(previous_bmd.Stride * scan_window_height).ToArray(), current_bytes.Skip(0).Take(current_bmd.Stride * scan_window_height).ToArray(), orig_y));
            }

            //Write(score, n);

            int minscore = int.MaxValue;
            int minscoreindex = -1;

            foreach (var kvp in score)
            {
                if (kvp.Value < minscore)
                {
                    minscoreindex = kvp.Key;
                    minscore = kvp.Value;
                }
            }

            return minscoreindex > 0 ? minscoreindex : previous_bmd.Height;
        }

        private static int GetScore(byte[] current_bytes, byte[] previous_bytes, int orig_y)
        {
            int score = 0;

            for (int i = 0; i < current_bytes.Length; i++)
            {
                score += current_bytes[i] != previous_bytes[i] ? 1 : 0;
            }

            return score;
        }
    }
}
