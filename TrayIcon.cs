using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MemeMic
{
    class TrayIcon
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        public TrayIcon()
        {
            Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MemeMic.Resources.small_logo.ico");
            notifyIcon.Icon = new System.Drawing.Icon(imageStream);
            notifyIcon.Text = "MemeMic";
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Exit",null,onExitClick);
        }

        private void onExitClick(object sender, System.EventArgs e)
        {
            notifyIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        public void Show()
        {
            notifyIcon.Visible = true;
        }
    }
}
