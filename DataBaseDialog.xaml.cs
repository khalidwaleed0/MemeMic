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
    /// Interaction logic for DataBaseDialog.xaml
    /// </summary>
    public partial class DataBaseDialog : Window
    {
        public DataBaseDialog()
        {
            InitializeComponent();
        }

        private void MemeMicButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }
        private void CommunityButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }
    }
}
