using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemeMic
{
    class MemePlayer
    {
        WaveOutEvent player = new WaveOutEvent();
        OverlayWindow overlay;
        public MemePlayer()
        {
            player.PlaybackStopped += Player_PlaybackStopped;
        }

        public void play(string path)
        {
            MediaFoundationReader reader = new MediaFoundationReader(path);
            player.Init(reader);
            player.Play();
        }

        private void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {

        }

        public void stop()
        {
            player.Dispose();
        }
    }
}
