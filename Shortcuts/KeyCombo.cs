using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProSnap
{
    public class KeyCombo
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey); 

        public Keys Key { get; private set; }

        public bool isAltPressed { get; private set; }
        public bool isCtrlPressed { get; private set; }
        public bool isShiftPressed { get; private set; }
        public bool isWinPressed { get; private set; }

        //public bool isAltPressed { get { return isLAltPressed || isRAltPressed; } }
        //public bool isLAltPressed { get; private set; }
        //public bool isRAltPressed { get; private set; }

        //public bool isCtrlPressed { get { return isLCtrlPressed || isRCtrlPressed; } }
        //public bool isLCtrlPressed { get; private set; }
        //public bool isRCtrlPressed { get; private set; }

        //public bool isShiftPressed { get { return isLShiftPressed || isRShiftPressed; } }
        //public bool isLShiftPressed { get; private set; }
        //public bool isRShiftPressed { get; private set; }

        //public bool isWinPressed { get { return isLWinPressed || isRWinPressed; } }
        //public bool isLWinPressed { get; private set; }
        //public bool isRWinPressed { get; private set; }

        public KeyCombo(Keys key, bool alt = false, bool ctrl = false, bool shift = false, bool win = false)
        {
            this.Key = key;

            this.isAltPressed = alt;
            this.isCtrlPressed = ctrl;
            this.isShiftPressed = shift;
            this.isWinPressed = win;
        }

        public override string ToString()
        {
            List<string> tokens = new List<string>();

            if (isCtrlPressed)
                tokens.Add("Ctrl");
            if (isAltPressed)
                tokens.Add("Alt");
            if (isShiftPressed)
                tokens.Add("Shift");
            if (isWinPressed)
                tokens.Add("Win");

            return string.Join(" + ", tokens.ToArray()) + ((tokens.Count > 0 && Key != Keys.None) ? " + " : "") + (Key == Keys.None ? "" : Key.ToString());
        }

        public static KeyCombo FromKeyEventArgs(KeyEventArgs e)
        {
            return new KeyCombo(e.KeyCode == Keys.Alt || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin ? Keys.None : e.KeyCode, e.Alt, e.Control, e.Shift, (GetAsyncKeyState(Keys.LWin) & 0x8000) != 0 || (GetAsyncKeyState(Keys.RWin) & 0x8000) != 0);
        }

        internal static KeyCombo FromKeyboardHookEventArgs(Utilities.KeyboardHook.KeyboardHookEventArgs e)
        {
            return new KeyCombo(e.Key == Keys.LMenu || e.Key == Keys.RMenu || e.Key == Keys.LControlKey || e.Key == Keys.RControlKey || e.Key == Keys.LShiftKey || e.Key == Keys.RShiftKey || e.Key == Keys.LWin || e.Key == Keys.RWin ? Keys.None : e.Key, e.isAltPressed, e.isCtrlPressed, e.isShiftPressed, e.isWinPressed);
        }
    }
}
