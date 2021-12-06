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
        private readonly List<TextBlock> textBlocks = new List<TextBlock>();

        public OverlayWindow()
        {
            InitializeComponent();
            try
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens
                    [Convert.ToInt32(AppSetup.getInstance().readSettingsFile(AppSetup.screenNumberLine))];
                var workingArea = secondaryScreen.WorkingArea;
                Left = workingArea.Left;
                Top = workingArea.Top;
            }
            catch (Exception) { }
            DisplayMemes();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }
        private void DisplayMemes()
        {
            foreach (string filePath in AppSetup.getInstance().filteredMemeFiles)
            {
                string[] pathParts = filePath.Split(new char[] { '\\', '.' });
                string memeName = pathParts[pathParts.Length - 2];
                AddTextBlock(memeName);
                HighlightText(0);
            }
        }
        private void AddTextBlock(string text)
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = text;
            stackPanel.Children.Add(newTextBlock);
            textBlocks.Add(newTextBlock);
        }
        public void HighlightText(int i)
        {
            textBlocks[i].Foreground = new SolidColorBrush(Colors.Aqua);
        }
        public void UnhighlightText(int i)
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
