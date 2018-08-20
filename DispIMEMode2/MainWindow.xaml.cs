using System;
using System.Windows;
using System.Threading;
using System.Windows.Forms;

namespace DispIMEMode2 {
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private bool IsIMEOn(IntPtr hWnd) {
            return SendMessage(hWnd, 0x283, 0x005, 0) == 1;
        }

        private Int32 GetIMEMode(IntPtr hWnd) {
            return SendMessage(hWnd, 0x283, 0x001, 0);
        }

        private delegate void getIMEModeFunc(GUITHREADINFO info);

        private void OnLoad(object sender, RoutedEventArgs e) {
            (new Thread(new ThreadStart(() => {
                var aGetIMEMode = new getIMEModeFunc(GetIMEMode);
                var info = new GUITHREADINFO();
                info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
                while (true) {
                    Thread.Sleep(250);
                    Dispatcher.BeginInvoke(aGetIMEMode, info);
                }
            })) {
                IsBackground = true
            }).Start();
        }

        private void GetIMEMode(GUITHREADINFO info) {
            GetGUIThreadInfo(0, ref info);
            IntPtr hIMEWnd = ImmGetDefaultIMEWnd(info.hwndFocus);

            if (IsIMEOn(hIMEWnd)) {
                Int32 ret = GetIMEMode(hIMEWnd);
                IMEMode.Content = ((ret & 0x10) == 0x0 ? "ｶﾅ " : "")
                                + ((ret & 0xF) == 0x0 ? "A" :
                                   (ret & 0xF) == 0x3 ? "ｶ" :
                                   (ret & 0xF) == 0x8 ? "Ａ" :
                                   (ret & 0xF) == 0x9 ? "あ" :
                                   (ret & 0xF) == 0xB ? "カ" : "");
#if false
                Int32 ActiveThreadID = GetWindowThreadProcessId(GetForegroundWindow(), out int ActiveProcessID);
                if (AttachThreadInput(GetCurrentThreadId(), ActiveThreadID, true)) {
                    IntPtr hWndActiveControl = GetFocus();
                    AttachThreadInput(GetCurrentThreadId(), ActiveThreadID, false);
                    GetWindowRect(hWndActiveControl, out RECT rect);
                    var p = new Point((rect.left + rect.right) / 2, (rect.top + rect.bottom) / 2);
                    var d = new Point(Control.MousePosition.X - p.X, Control.MousePosition.Y - p.Y);
                    if (d.X >= 0 && d.X <= this.Width && d.Y >= 0 && d.Y <= this.Height) {
                        p.X += this.Width;
                    }
                    this.Left = p.X;
                    this.Top = p.Y;
                }
#else
                var rect = Screen.PrimaryScreen.Bounds;
                var p = new Point(rect.Width / 2, rect.Height / 2);
                var d = new Point(Control.MousePosition.X - p.X, Control.MousePosition.Y - p.Y);
                if (d.X >= 0 && d.X <= this.Width && d.Y >= 0 && d.Y <= this.Height) {
                    p.X += this.Width;
                }
                this.Left = p.X;
                this.Top = p.Y;
#endif
                this.Opacity = 0.5;
            } else {
                IMEMode.Content = "";
                this.Opacity = 0.0;
            }
        }
    }
}