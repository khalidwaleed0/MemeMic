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
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        private List<TextBlock> textBlocks = new List<TextBlock>(); 

        public OverlayWindow()
        {
            InitializeComponent();
            displayMemes();
        }
        private void displayMemes()
        {
            foreach (string filePath in AppSetup.filteredMemeFiles)
            {
                string[] pathParts = filePath.Split(new char[] { '\\', '.' });
                string memeName = pathParts[pathParts.Length - 2];
                addTextBlock(memeName);
                highlightText(0);
            }
            /*            var desktopWorkingArea = SystemParameters.WorkArea;
                        Left = desktopWorkingArea.Right - ActualWidth;
                        Top = desktopWorkingArea.Bottom - ActualHeight;*/
/*            Height = SystemParameters.WorkArea.Height;
            Width = SystemParameters.WorkArea.Width;
            Left = SystemParameters.WorkArea.Location.X;
            Top = SystemParameters.WorkArea.Location.Y;*/
        }
        private void addTextBlock(string text)
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = text;
            stackPanel.Children.Add(newTextBlock);
            textBlocks.Add(newTextBlock);
        }
        public void highlightText(int i)
        {
            textBlocks[i].Foreground = new SolidColorBrush(Colors.Aqua);
        }
        public void unhighlightText(int i)
        {
            textBlocks[i].Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
