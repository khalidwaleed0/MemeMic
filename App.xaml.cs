using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MemeMic
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppSetup.checkDirectory();
            AppSetup.checkSettingsFile();
            if(AppSetup.getVirtualSpeakerNumber() != -2)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                TrayIcon.Singleton().Show();
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
