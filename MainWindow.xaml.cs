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
namespace MemeMic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveOutEvent player = new WaveOutEvent();
        public MainWindow()
        {

            InitializeComponent();
        }
        
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            player.Dispose();
            MediaFoundationReader reader = new MediaFoundationReader(@"F:\memes\Videos\يلعن ميتين أبوكوا.mp4");
            player.Init(reader);
            player.Play();
        }
    }
}
