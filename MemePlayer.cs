using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMic
{
    class MemePlayer
    {
        WaveOutEvent player = new WaveOutEvent();
        public void play()
        {
            MediaFoundationReader reader = new MediaFoundationReader(@"F:\memes\Videos\يلعن ميتين أبوكوا.mp4");
            player.Init(reader);
            player.Play();
        }
        public void stop()
        {
            player.Dispose();
        }
    }
}
