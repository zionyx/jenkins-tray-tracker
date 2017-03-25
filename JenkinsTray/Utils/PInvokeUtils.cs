using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JenkinsTray.Utils
{
    internal static class PInvokeUtils
    {
        private const int SC_RESTORE = 0xF120;
        private const int WM_SYSCOMMAND = 0x112;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public static void RestoreForm(Form form)
        {
            SendMessage(form.Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
        }
    }
}