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
        OverlayWindow overlay = new OverlayWindow();
        private bool isClosedNotExited = false;
        public MainWindow()
        {
            InitializeComponent();
            DirectoryTextBox.Text = AppSetup.readSettingsFile(AppSetup.pathLine);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppSetup.readSettingsFile(AppSetup.pathLine).Equals("") || AppSetup.readSettingsFile(AppSetup.overlayButtonLine).Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Make sure to choose the folder meme\nand the overlay button"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AppSetup.filterMemeFiles();
                if(AppSetup.filteredMemeFiles.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("The selected folder meme does not contain any valid memes" +
                        "\nSupported Extensions: .mp3,.aac,.wav,.webm,.m4a,.mp4,.mkv"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    isClosedNotExited = true;
                    Close();
                    EventListener listener = new EventListener();
                    string overlayButton = AppSetup.readSettingsFile(AppSetup.overlayButtonLine);
                    listener.captureMouseEvent();
                    listener.captureKeyboardEvent();
                }
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();
        }

        private void MemeDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseDialog databaseDialog = new DataBaseDialog();
            databaseDialog.ShowDialog();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            bool isFolderSelected = (bool)folderDialog.ShowDialog();
            if(isFolderSelected)
            {
                DirectoryTextBox.Text = folderDialog.SelectedPath;
                AppSetup.modifyFolderPath(folderDialog.SelectedPath);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please choose a directory", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!isClosedNotExited)
                System.Windows.Application.Current.Shutdown();
            //if it is closed by the x button then it will shutdown
            //otherwise, it will just close the window but keep running in background
        }
    }
}
