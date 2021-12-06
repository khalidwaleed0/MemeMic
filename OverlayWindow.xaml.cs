using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace MemeMic
{
    public partial class OverlayWindow : Window
    {
        private List<TextBlock> textBlocks = new List<TextBlock>(); 

        public OverlayWindow()
        {
            InitializeComponent();
            displayMemes();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }
        private void displayMemes()
        {
            foreach (string filePath in AppSetup.getInstance().filteredMemeFiles)
            {
                string[] pathParts = filePath.Split(new char[] { '\\', '.' });
                string memeName = pathParts[pathParts.Length - 2];
                addTextBlock(memeName);
                highlightText(0);
            }
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
    public static class WindowsServices
    {
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowExTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
    }
}
