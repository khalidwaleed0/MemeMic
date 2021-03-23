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
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1B9sk4PMx1W40chkqL1t5MLXK8VwVcQuJJtQNBZOlwZA/edit?fbclid=IwAR0UEMe2gj0Wu8TGuii3JCvfo9_yP-hznRi6aGkFtIsUe1uPxZmG09K13s8#gid=0");
        }
        private void CommunityButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1sVNmhwQzJBdm9_RKaR_cp3SFwMQ8BntUzr8_Jb-4K-c/edit?usp=sharing");
        }
    }
}
