using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCopy {
    public static class Toolkit {
        const int WM_USER = 0x400;
        const int PBM_SETSTATE = WM_USER + 16;
        //const int PBM_GETSTATE = WM_USER + 17;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        public enum ProgressBarStateEnum : int {
            Normal = 1,
            Error = 2,
            Paused = 3,
        }

        public static void SetState(this ProgressBar pBar, int state) {
            SendMessage(pBar.Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
        }

        public static string Truncate(this string value, int maxChars) {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
