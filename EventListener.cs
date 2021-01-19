using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using EventHook;
using System.IO;

namespace MemeMic
{
    class EventListener : Control
    {
        public EventHookFactory eventHookFactory = new EventHookFactory();
        public KeyboardWatcher keyboardWatcher;
        public MouseWatcher mouseWatcher;
        private string overlayState = "Hidden";
        private OverlayWindow overlay = new OverlayWindow();
        private static PlayingOverlay playingOverlay = new PlayingOverlay();
        private MicPlayer micPlayer = new MicPlayer(playingOverlay);
        private SpeakerPlayer speakerPlayer = new SpeakerPlayer();
        private int memeIndex = 0;
        public void captureKeyboardEvent()
        {
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            keyboardWatcher.Start();
            keyboardWatcher.OnKeyInput += (s, e) =>
            {
                if(e.KeyData.EventType.ToString().Equals("down") && e.KeyData.Keyname.ToString().Equals(AppSetup.readSettingsFile(AppSetup.overlayButtonLine)))
                    changeOverlayState();
            };
        }
        public void captureMouseEvent()
        {
            string savedButton = AppSetup.readSettingsFile(AppSetup.overlayButtonLine);
            string correctMouseData = "";
            string correctMouseButton = "";
            get_SavedMouseButton_Data(savedButton, ref correctMouseData, ref correctMouseButton);
            mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start();
            mouseWatcher.OnMouseInput += (s, e) =>
            {
                string mouseInputName = e.Message.ToString();
                string mouseInputData = e.MouseData.ToString();
                if(overlayState.Equals("Shown"))
                {
                    const string mouseWheelUpData = "7864320";
                    const string mouseWheelDownData = "4287102976";
                    if (mouseInputData.Equals(mouseWheelDownData))
                        scrollDown();
                    else if (mouseInputData.Equals(mouseWheelUpData))
                        scrollUp();
                }

                if (savedButton.Contains("XButton"))
                {
                    if (mouseInputData.Equals(correctMouseData) && mouseInputName.Contains("DOWN"))
                        changeOverlayState();
                }
                else if(mouseInputName.Equals(correctMouseButton))
                        changeOverlayState();
            };
        }

        private void get_SavedMouseButton_Data(string savedButton, ref string correctMouseData, ref string correctMouseButton)
        {
            switch (savedButton)
            {
                case "XButton1":
                    correctMouseData = "65536";
                    break;
                case "XButton2":
                    correctMouseData = "131072";
                    break;
                case "Middle":
                    correctMouseData = "0";
                    correctMouseButton = "WM_WHEELBUTTONDOWN";
                    break;
            }
        }
        private void changeOverlayState()
        {
            switch (overlayState)
            {
                case "Hidden":
                    Dispatcher.Invoke(() => { 
                        overlay.Show();
                        overlay.highlightText(0);
                    });
                    overlayState = "Shown";
                    memeIndex = 0;
                    break;
                case "Shown":
                    micPlayer.play(AppSetup.filteredMemeFiles[memeIndex]);
                    speakerPlayer.play(AppSetup.filteredMemeFiles[memeIndex]);
                    Dispatcher.Invoke(() => {
                        overlay.unhighlightText(memeIndex);
                        overlay.Hide();
                        playingOverlay.Show();
                    });
                    overlayState = "Playing";
                    break;
                case "Playing":
                    micPlayer.stop();
                    speakerPlayer.Stop();
                    Dispatcher.Invoke(() => {
                        playingOverlay.Hide();
                    });
                    overlayState = "Hidden";
                    break;
            }
        }
        private void scrollDown()
        {
            if (memeIndex != (AppSetup.filteredMemeFiles.Count - 1))
            {
                memeIndex++;
                Dispatcher.Invoke(() =>
                {
                    overlay.unhighlightText(memeIndex - 1);
                    overlay.highlightText(memeIndex);
                });
            }
        }
        private void scrollUp()
        {
            if (memeIndex != 0)
            {
                memeIndex--;
                Dispatcher.Invoke(() =>
                {
                    overlay.unhighlightText(memeIndex + 1);
                    overlay.highlightText(memeIndex);
                });
            }
        }
        public void closeListener()
        {
            keyboardWatcher.Stop();
            mouseWatcher.Stop();
            eventHookFactory.Dispose();
        }
        ~EventListener()
        {
            keyboardWatcher.Stop();
            mouseWatcher.Stop();
            eventHookFactory.Dispose();
        }
    }
}
