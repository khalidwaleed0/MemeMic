using System;
using System.Threading;
using System.Windows;

namespace MemeMic
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public bool isMemeBoxOpen = false;
        bool iWantToClose = false;
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!iWantToClose)
            {
                e.Cancel = true;
                isMemeBoxOpen = true;
                MessageBox.Show("يسطا انت بتقفل ليه هو أنا عامل التحديث ده لأمي طب مش قافل بقى ايه رأيك","MemeMic"
                    ,MessageBoxButton.OK,MessageBoxImage.Exclamation);
                isMemeBoxOpen = false;
            }
        }
        public void forceClose()
        {
            iWantToClose = true;
            Close();
        }
    }
}
