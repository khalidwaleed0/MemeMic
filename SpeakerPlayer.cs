using NAudio.Wave;
using System.Globalization;

namespace MemeMic
{
    class SpeakerPlayer
    {
        readonly WaveOutEvent speakerPlayer = new WaveOutEvent();
        MediaFoundationReader reader;

        public SpeakerPlayer()
        {                                                          //max volume = 1.0F
            speakerPlayer.Volume = float.Parse(AppSetup.GetInstance().ReadSettingsFile(AppSetup.speakerVolumeLine),
                CultureInfo.InvariantCulture);                     // stored with '.'; parse culture-independently
            speakerPlayer.DeviceNumber = 0;                        // -1 or 0 mean the default playback device
            speakerPlayer.PlaybackStopped += (s, e) => { reader?.Dispose(); reader = null; };
        }
        public void Play(string path)
        {
            reader = new MediaFoundationReader(path);
            speakerPlayer.Init(reader);
            speakerPlayer.Play();
        }
        public void Stop()
        {
            speakerPlayer.Stop();     // reader disposed in PlaybackStopped; device is reused, never disposed
        }
    }
}
