using AngleSharp;
using System;
using System.Net;
using System.Windows;
using System.Threading;
namespace MemeMic
{
    class Updater
    {
        UpdateWindow updateWindow;
        public Updater(UpdateWindow updateWindow)
        {
            this.updateWindow = updateWindow;
        }
        public async void update()
        {
            WebClient client = new WebClient();
            string source = client.DownloadString("https://github.com/khalidwaleed0/MemeMic/releases");
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(source));
            var element = document.QuerySelector("h1 .Link--primary");
            string latestReleaseName = element.TextContent;
            if (!latestReleaseName.Equals("v1.0.1"))
            {
                Application.Current.Dispatcher.Invoke(() =>{
                    updateWindow.firstTextBlock.Text = "Downloading latest version...";
                });
                var latestReleaseElement = document.QuerySelector(".Box.Box--condensed.mt-3 a");
                string latestReleaseLink = "https://github.com/" + latestReleaseElement.GetAttribute("href");
                download(latestReleaseLink);
                try
                {
                    waitForMemeBox();
                    System.Diagnostics.Process.Start("update.bat");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("The new installation file is on desktop");
                }
                client.Dispose();
                Environment.Exit(0);
            }
            else
            {
                waitForMemeBox();
                Application.Current.Dispatcher.Invoke(() => { updateWindow.forceClose(); });
                client.Dispose();
            }
        }
        public void download(string latestReleaseLink)
        {
            using(WebClient client = new WebClient())
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                client.DownloadFile(latestReleaseLink, desktopPath + "\\MemeMic.exe");
            }
        }
        private void waitForMemeBox()
        {
            while(updateWindow.isMemeBoxOpen)
                Thread.Sleep(100);
        }
    }
}
