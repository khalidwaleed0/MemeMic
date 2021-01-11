using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemeMic
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        bool isOverlayButtonClicked = false;
        bool isDiscordButtonClicked = false;
        public OptionsWindow()
        {
            InitializeComponent();
            displayLatestSettings();
        }
        private void OverlayListenButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOverlayTextBox.Text = "Recording..";
            PlayMemeTextBox.Text = "Recording..";
            isOverlayButtonClicked = true;
        }
        private void displayLatestSettings()
        {
            ShowOverlayTextBox.Text = AppSetup.readSettingsFile(AppSetup.overlayButtonLine);
            PlayMemeTextBox.Text = AppSetup.readSettingsFile(AppSetup.overlayButtonLine);
            string pushToTalkButton = AppSetup.readSettingsFile(AppSetup.pushToTalkLine);
            if (!pushToTalkButton.Equals(""))
            {
                PushToTalkRadioButton.IsChecked = true;
                DiscordKeyTextBox.Text = pushToTalkButton;
            }
        }
        private void PushToTalkRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DiscordGrid.Visibility = Visibility.Visible;
        }
        private void VoiceActivityRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                DiscordKeyTextBox.Text = "";
                DiscordGrid.Visibility = Visibility.Hidden;
            }
            catch(NullReferenceException ex)
            {
                ex.ToString();
            }
            
        }
        private void DiscordKeyButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordKeyTextBox.Text = "Recording..";
            isDiscordButtonClicked = true;
        }
        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {                               // the event is running all the time but we only need to get the pressed key after
            if (isOverlayButtonClicked) // the listen button is pressed
            {
                ShowOverlayTextBox.Text = e.ChangedButton.ToString();
                PlayMemeTextBox.Text = e.ChangedButton.ToString();
                isOverlayButtonClicked = false;
            }
            else if(isDiscordButtonClicked)
            {
                DiscordKeyTextBox.Text = e.ChangedButton.ToString();
                isDiscordButtonClicked = false;
            }
        }
        private void onKeyboardDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(isOverlayButtonClicked)  // the event is running all the time but we only need to get the pressed key after
            {                           // the listen button is pressed
                ShowOverlayTextBox.Text = e.Key.ToString();
                PlayMemeTextBox.Text = e.Key.ToString();
                isOverlayButtonClicked = false;
            }
            else if(isDiscordButtonClicked)
            {
                DiscordKeyTextBox.Text = e.Key.ToString();
                isDiscordButtonClicked = false;
            }
        }
        private void onDispose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ShowOverlayTextBox.Text.Equals("Recording.."))
            {
                e.Cancel = true;
                System.Windows.Forms.MessageBox.Show("Please choose a button first", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                AppSetup.modifyButtons(ShowOverlayTextBox.Text, DiscordKeyTextBox.Text);
                Close();
            }
        }
    }
}
