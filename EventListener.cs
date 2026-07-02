using System;
using System.Windows.Threading;
using System.Windows.Controls;
using EventHook;

namespace MemeMic
{
    public class EventListener : Control
    {
        private static EventListener _instance;
        private static readonly object _lock = new object();

        public static EventListener Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EventListener();
                        }
                    }
                }
                return _instance;
            }
        }

        private EventHookFactory eventHookFactory;
        private KeyboardWatcher keyboardWatcher;
        private MouseWatcher mouseWatcher;
        private string overlayState = "Hidden";
        private readonly OverlayWindow overlay;
        private static readonly PlayingOverlay playingOverlay;
        private readonly MicPlayer micPlayer;
        private SpeakerPlayer speakerPlayer;
        private int memeIndex = 0;

        private EventListener()
        {
            eventHookFactory = new EventHookFactory();
            overlay = new OverlayWindow();
            micPlayer = new MicPlayer(OnPlaybackFinished);
            speakerPlayer = new SpeakerPlayer();
        }

        static EventListener()
        {
            playingOverlay = new PlayingOverlay();
        }

        // Static methods for public access
        public static void StartKeyboardCapture()
        {
            Instance.CaptureKeyboardEvent();
        }

        public static void StartMouseCapture()
        {
            Instance.CaptureMouseEvent();
        }

        public static void Stop()
        {
            Instance.CloseListener();
        }

        // Private instance methods
        private void CaptureKeyboardEvent()
        {
            string savedButton = AppSetup.GetInstance().ReadSettingsFile(AppSetup.overlayButtonLine);
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            keyboardWatcher.Start();
            keyboardWatcher.OnKeyInput += (s, e) =>
            {
                // Hook callbacks run on a background thread; do only the cheap match here,
                // then hand the actual work to the UI thread (which also serializes it).
                if(e.KeyData.Keyname.ToString().Equals(savedButton) && e.KeyData.EventType.ToString().Equals("down"))
                    Dispatcher.BeginInvoke(new Action(ChangeOverlayState));
            };
        }

        private void CaptureMouseEvent()
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

                if (mouseInputData.Equals(mouseWheelDownData))
                    Dispatcher.BeginInvoke(new Action(ScrollDown));
                else if (mouseInputData.Equals(mouseWheelUpData))
                    Dispatcher.BeginInvoke(new Action(ScrollUp));

                bool triggerPressed = savedButton.Contains("XButton")
                    ? (mouseInputData.Equals(correctMouseData) && mouseInputName.Contains("DOWN"))
                    : mouseInputName.Equals(correctMouseButton);
                if (triggerPressed)
                    Dispatcher.BeginInvoke(new Action(ChangeOverlayState));
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

        // Everything below runs on the UI thread (marshalled from the hook callbacks),
        // so state transitions can't interleave and it is safe to touch the windows directly.
        private void ChangeOverlayState()
        {
            switch (overlayState)
            {
                case "Hidden":
                    overlay.Show();
                    overlay.HighlightText(0);
                    memeIndex = 0;
                    overlayState = "Shown";
                    break;
                case "Shown":
                    overlay.UnhighlightText(memeIndex);
                    overlay.Hide();
                    playingOverlay.Show();
                    micPlayer.play(AppSetup.GetInstance().filteredMemeFiles[memeIndex]);
                    speakerPlayer.Play(AppSetup.GetInstance().filteredMemeFiles[memeIndex]);
                    overlayState = "Playing";
                    break;
                case "Playing":
                    micPlayer.stop();
                    speakerPlayer.Stop();
                    playingOverlay.Hide();
                    overlayState = "Hidden";
                    break;
            }
        }

        // Called (via MicPlayer) when a clip stops, including reaching its own end.
        private void OnPlaybackFinished()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(OnPlaybackFinished));
                return;
            }
            if (overlayState.Equals("Playing"))
            {
                playingOverlay.Hide();
                overlayState = "Hidden";
            }
        }

        private void ScrollDown()
        {
            if (!overlayState.Equals("Shown"))
                return;
            if (memeIndex != (AppSetup.GetInstance().filteredMemeFiles.Count - 1))
            {
                memeIndex++;
                overlay.UnhighlightText(memeIndex - 1);
                overlay.HighlightText(memeIndex);
            }
        }

        private void ScrollUp()
        {
            if (!overlayState.Equals("Shown"))
                return;
            if (memeIndex != 0)
            {
                memeIndex--;
                overlay.UnhighlightText(memeIndex + 1);
                overlay.HighlightText(memeIndex);
            }
        }

        private void CloseListener()
        {
            keyboardWatcher?.Stop();
            mouseWatcher?.Stop();
            eventHookFactory?.Dispose();
        }

        ~EventListener()
        {
            CloseListener();
        }
    }
}
