using System;
using System.Windows;

namespace MemeMic
{
    public partial class PlayingOverlay : Window
    {
        public PlayingOverlay()
        {
            InitializeComponent();
            try
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens
                    [Convert.ToInt32(AppSetup.getInstance().ReadSettingsFile(AppSetup.screenNumberLine))];
                var workingArea = secondaryScreen.WorkingArea;
                Left = workingArea.Left;
                Top = workingArea.Top;
            }
            catch (Exception) { }
        }
    }
}
