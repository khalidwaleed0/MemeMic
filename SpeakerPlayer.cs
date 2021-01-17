using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMic
{
    class SpeakerPlayer
    {
        WaveOutEvent speakerPlayer = new WaveOutEvent();
        public SpeakerPlayer()
        {
            speakerPlayer.DeviceNumber = 0;                        // -1 is the default playback device
        }
        public void play(string path)
        {
            MediaFoundationReader reader = new MediaFoundationReader(path);
            speakerPlayer.Init(reader);
            speakerPlayer.Play();
        }
        public void Stop()
        {
            speakerPlayer.Dispose();
        }
    }
}
