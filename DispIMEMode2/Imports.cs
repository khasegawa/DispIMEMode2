using System;
using System.Windows;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DispIMEMode2
{
    public partial class MainWindow : Window
    {
        public struct GUITHREADINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public Rectangle rcCaret;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [DllImport("User32.dll")]
        private static extern UInt32 GetGUIThreadInfo(UInt32 dwthreadid, ref GUITHREADINFO lpguithreadinfo);
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern Int32 SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, Int32 lParam);
        [DllImport("User32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("User32.dll")]
        private static extern bool AttachThreadInput(Int32 idAttach, Int32 idAttachTo, bool fAttach);
        [DllImport("kernel32.dll")]
        private static extern Int32 GetCurrentThreadId();
        [DllImport("User32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);
        [DllImport("User32.dll")]
        private static extern IntPtr GetFocus();
        [DllImport("User32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}