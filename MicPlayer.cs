using NAudio.Wave;
using System;
using System.Threading;
using System.Windows;

namespace MemeMic
{
    class MicPlayer
    {
        WaveOutEvent micPlayer = new WaveOutEvent();
        PlayingOverlay playingOverlay;

        public MicPlayer(PlayingOverlay playingOverlay)
        {
            micPlayer.DeviceNumber = AppSetup.GetInstance().GetVirtualSpeakerNumber();
            micPlayer.PlaybackStopped += Player_PlaybackStopped;
            this.playingOverlay = playingOverlay;
        }

        public void play(string path)
        {
            MediaFoundationReader reader = new MediaFoundationReader(path);
            micPlayer.Init(reader); 
            micPlayer.Play();
        }

        private void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {                                                       // It only needs to be added either in this class or
            Application.Current.Dispatcher.Invoke(() => {       // SpeakerPlayer class and same goes for the playingOverlay
                playingOverlay.Hide();
            });
        }

        public void stop()
        {
            micPlayer.Dispose();
        }
    }
}
