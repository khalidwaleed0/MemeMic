using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Threading;

namespace MemeMic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveOutEvent player = new WaveOutEvent();
        OverlayWindow overlay = new OverlayWindow();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            player.Dispose();
            MediaFoundationReader reader = new MediaFoundationReader(@"F:\memes\Videos\يلعن ميتين أبوكوا.mp4");
            player.Init(reader);
            player.Play();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();

        }

        private void DownloadMemesButton_Click(object sender, RoutedEventArgs e)
        {
            overlay.Show();
            //System.Diagnostics.Process.Start("http://www.google.com");          // opens in a different process
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            folderDialog.ShowDialog();
            DirectoryTextBox.Text = folderDialog.SelectedPath;
            if (!File.Exists(AppSetup.settingsFilePath))
                AppSetup.createSettingsFile(folderDialog.SelectedPath);
            else
            {
                AppSetup.modifyFolderPath(folderDialog.SelectedPath);
            }
            try
            {
                string[] filePaths = Directory.GetFiles(folderDialog.SelectedPath);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Please choose a directory", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
