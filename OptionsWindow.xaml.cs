using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MemeMic
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }
        private void ShowOverlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PushToTalkRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DiscordGrid.Visibility = Visibility.Visible;
        }

        private void VoiceActivityRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DiscordGrid.Visibility = Visibility.Hidden;
        }
    }
}
