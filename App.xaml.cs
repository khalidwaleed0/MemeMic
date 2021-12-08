using System.Threading;
using System.Windows;

namespace MemeMic
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppSetup.getInstance().CheckDirectory();
            AppSetup.getInstance().CheckSettingsFile();
            if(AppSetup.getInstance().GetVirtualSpeakerNumber() != -2)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                UpdateWindow updateWindow = new UpdateWindow();
                Updater updater = new Updater(updateWindow);
                Thread updateThread = new Thread(() => { updater.update(); });
                updateThread.Start();
                updateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please install VB-CABLE Virtual Audio Device first\nhttps://vb-audio.com/Cable/"
                    , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Diagnostics.Process.Start("https://vb-audio.com/Cable/");
            }
        }
    }
}
