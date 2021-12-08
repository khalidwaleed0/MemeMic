using System.Windows.Threading;
using System.Windows.Controls;
using EventHook;

namespace MemeMic
{
    class EventListener : Control
    {
        public EventHookFactory eventHookFactory = new EventHookFactory();
        public KeyboardWatcher keyboardWatcher;
        public MouseWatcher mouseWatcher;
        private string overlayState = "Hidden";
        private readonly OverlayWindow overlay = new OverlayWindow();
        private static readonly PlayingOverlay playingOverlay = new PlayingOverlay();
        private readonly MicPlayer micPlayer = new MicPlayer(playingOverlay);
        private SpeakerPlayer speakerPlayer = new SpeakerPlayer();
        private int memeIndex = 0;
        public void CaptureKeyboardEvent()
        {
            string savedButton = AppSetup.GetInstance().ReadSettingsFile(AppSetup.overlayButtonLine);
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            keyboardWatcher.Start();
            keyboardWatcher.OnKeyInput += (s, e) =>
            {
                if(e.KeyData.Keyname.ToString().Equals(savedButton) && e.KeyData.EventType.ToString().Equals("down"))
                    ChangeOverlayState();
            };
        }
        public void CaptureMouseEvent()
        {
            string savedButton = AppSetup.GetInstance().ReadSettingsFile(AppSetup.overlayButtonLine);
            string correctMouseData = "";
            string correctMouseButton = "";
            const string mouseWheelUpData = "7864320";
            const string mouseWheelDownData = "4287102976";
            Get_SavedMouseButton_Data(savedButton, ref correctMouseData, ref correctMouseButton);
            mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start();
            mouseWatcher.OnMouseInput += (s, e) =>
            {
                string mouseInputName = e.Message.ToString();
                string mouseInputData = e.MouseData.ToString();
                if(overlayState.Equals("Shown"))
                {
                    if (mouseInputData.Equals(mouseWheelDownData))
                        ScrollDown();
                    else if (mouseInputData.Equals(mouseWheelUpData))
                        ScrollUp();
                }

                if (savedButton.Contains("XButton"))
                {
                    if (mouseInputData.Equals(correctMouseData) && mouseInputName.Contains("DOWN"))
                        ChangeOverlayState();
                }
                else if(mouseInputName.Equals(correctMouseButton))
                        ChangeOverlayState();
            };
        }

        private void Get_SavedMouseButton_Data(string savedButton, ref string correctMouseData, ref string correctMouseButton)
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
        private void ChangeOverlayState()
        {
            switch (overlayState)
            {
                case "Hidden":
                    Dispatcher.Invoke(() => { 
                        overlay.Show();
                        overlay.HighlightText(0);
                    });
                    overlayState = "Shown";
                    memeIndex = 0;
                    break;
                case "Shown":
                    micPlayer.play(AppSetup.GetInstance().filteredMemeFiles[memeIndex]);
                    speakerPlayer.Play(AppSetup.GetInstance().filteredMemeFiles[memeIndex]);
                    Dispatcher.Invoke(() => {
                        overlay.UnhighlightText(memeIndex);
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
        private void ScrollDown()
        {
            if (memeIndex != (AppSetup.GetInstance().filteredMemeFiles.Count - 1))
            {
                memeIndex++;
                Dispatcher.Invoke(() =>
                {
                    overlay.UnhighlightText(memeIndex - 1);
                    overlay.HighlightText(memeIndex);
                });
            }
        }
        private void ScrollUp()
        {
            if (memeIndex != 0)
            {
                memeIndex--;
                Dispatcher.Invoke(() =>
                {
                    overlay.UnhighlightText(memeIndex + 1);
                    overlay.HighlightText(memeIndex);
                });
            }
        }
        public void CloseListener()
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
