using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MemeMic
{
    public partial class OptionsWindow : Window
    {
        bool isOverlayButtonClicked = false;
        public OptionsWindow()
        {
            InitializeComponent();
            for(int i=1; i<Screen.AllScreens.Length ; i++)
            {
                screenComboBox.Items.Add("Screen "+(i+1));
            }
            displayLatestSettings();
        }
        private void OverlayListenButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOverlayTextBox.Text = "Recording..";
            PlayMemeTextBox.Text = "Recording..";
            recordingGif_1.Visibility = Visibility.Visible;
            recordingGif_2.Visibility = Visibility.Visible;
            isOverlayButtonClicked = true;
        }
        private void displayLatestSettings()
        {
            ShowOverlayTextBox.Text = AppSetup.getInstance().readSettingsFile(AppSetup.overlayButtonLine);
            PlayMemeTextBox.Text = AppSetup.getInstance().readSettingsFile(AppSetup.overlayButtonLine);
            double speakerVolume = 100*double.Parse(AppSetup.getInstance().readSettingsFile(AppSetup.speakerVolumeLine));
            volumeTextBlock.Text = speakerVolume.ToString();
            volumeSlider.Value = speakerVolume;
        }
        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {                               // the event is running all the time but we only need to get the pressed key after
            if (isOverlayButtonClicked) // the listen button is pressed
            {
                ShowOverlayTextBox.Text = e.ChangedButton.ToString();
                PlayMemeTextBox.Text = e.ChangedButton.ToString();
                recordingGif_1.Visibility = Visibility.Hidden;
                recordingGif_2.Visibility = Visibility.Hidden;
                isOverlayButtonClicked = false;
            }
        }
        private void onKeyboardDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(isOverlayButtonClicked)  // the event is running all the time but we only need to get the pressed key after
            {                           // the listen button is pressed
                ShowOverlayTextBox.Text = e.Key.ToString();
                PlayMemeTextBox.Text = e.Key.ToString();
                recordingGif_1.Visibility = Visibility.Hidden;
                recordingGif_2.Visibility = Visibility.Hidden;
                isOverlayButtonClicked = false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            About_us option = new About_us();
            option.ShowDialog();
        }

        private void Donate_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.paypal.com/paypalme/khalidwaleed0");
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
                AppSetup.getInstance().modifyOptions(ShowOverlayTextBox.Text,Convert.ToString(volumeSlider.Value/100),
                    screenComboBox.SelectedIndex.ToString());
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            volumeTextBlock.Text = Convert.ToString(volumeSlider.Value)+"%";
        }
    }
}
