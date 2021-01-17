using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MemeMic
{
    class MemePlayer
    {
        WaveOutEvent player = new WaveOutEvent();
        PlayingOverlay playingOverlay;
        public MemePlayer(PlayingOverlay playingOverlay)
        {
            player.PlaybackStopped += Player_PlaybackStopped;
            this.playingOverlay = playingOverlay;
        }

        public void play(string path)
        {
            MediaFoundationReader reader = new MediaFoundationReader(path);
            player.Init(reader);
            player.Play();
        }

        private void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => {
                playingOverlay.Hide();
            });
        }

        public void stop()
        {
            player.Dispose();
        }
    }
}
