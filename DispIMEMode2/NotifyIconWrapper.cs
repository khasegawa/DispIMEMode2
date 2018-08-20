using System;
using System.Windows;
using System.ComponentModel;

namespace DispIMEMode2
{
    public partial class NotifyIconWrapper : Component
    {
        public NotifyIconWrapper()
        {
            this.InitializeComponent();

            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
        }

        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            this.InitializeComponent();
        }

        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
