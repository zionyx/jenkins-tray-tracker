using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JenkinsTray.Utils
{
    static class PInvokeUtils
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        const Int32 SC_RESTORE = 0xF120;
        const Int32 WM_SYSCOMMAND = 0x112;

        public static void RestoreForm(Form form)
        {
            SendMessage(form.Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
        }
    }
}
