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
        public static void checkDirectory()
        {
            if (!Directory.Exists(@"D:\MemeMic Memes"))
            {
                Directory.CreateDirectory(@"D:\MemeMic Memes");
                System.Windows.MessageBox.Show("Please move all of your memes in \"MemeMic Memes\" Directory \n You will find it in your D:/");
            }
            else
            {
                string[] arr = Directory.GetFiles(@"D:\MemeMic Memes", "*.mp3");
                foreach (string file in arr)
                {
                    System.Windows.MessageBox.Show(file);
                }
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
