using NAudio.Wave;
using System;

namespace MemeMic
{
    class MicPlayer
    {
        readonly WaveOutEvent micPlayer = new WaveOutEvent();
        MediaFoundationReader reader;
        readonly Action onStopped;

        public MicPlayer(Action onStopped)
        {
            this.onStopped = onStopped;
            micPlayer.DeviceNumber = AppSetup.GetInstance().GetVirtualSpeakerNumber();
            micPlayer.PlaybackStopped += OnPlaybackStopped;
        }

        public void play(string path)
        {
            reader = new MediaFoundationReader(path);
            micPlayer.Init(reader);   // must run on the UI thread so PlaybackStopped comes back on it
            micPlayer.Play();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            reader?.Dispose();        // release the file here, after playback has fully stopped
            reader = null;
            onStopped?.Invoke();
        }

        public void stop()
        {
            micPlayer.Stop();         // reader is disposed in OnPlaybackStopped; device is reused, never disposed
        }
    }
}
