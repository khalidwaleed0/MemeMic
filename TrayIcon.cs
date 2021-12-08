using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MemeMic
{
    class TrayIcon
    {
        private static TrayIcon trayIcon = null;
        public NotifyIcon notifyIcon = new NotifyIcon();
        public static TrayIcon GetInstance()
        {
            if (trayIcon == null)
                trayIcon = new TrayIcon();
            return trayIcon;
        }
        private TrayIcon()
        {
            Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MemeMic.Resources.Tray_Logo.ico");
            notifyIcon.Icon = new System.Drawing.Icon(imageStream);
            notifyIcon.Text = "MemeMic";
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, OnExitClick);
        }
        private void OnExitClick(object sender, System.EventArgs e)
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
