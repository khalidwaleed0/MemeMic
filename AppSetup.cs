using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemeMic
{
    class AppSetup
    {
        public static AppSetup appSetup = null;
        public List<string> filteredMemeFiles = new List<string>();
        public const byte pathLine = 0;
        public const byte overlayButtonLine = 1;
        public const byte speakerVolumeLine = 2;
        public const byte screenNumberLine = 3;
        private readonly string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\MemeMic";
        public string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\MemeMic\settings.txt";
        
        public static AppSetup getInstance()
        {
            if (appSetup == null)
                appSetup = new AppSetup();
            return appSetup;
        }
        
        public void CheckDirectory()
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
        public void CheckSettingsFile()
        {
            if (!File.Exists(settingsFilePath))
                CreateSettingsFile();
        }
        public void CreateSettingsFile(string path = "", string overlayButton = "", string speakerVolume = "0.3",string screenNumber = "0")
        {
            TextWriter writer = new StreamWriter(settingsFilePath);
            writer.WriteLine(path + "," + overlayButton + "," + speakerVolume + "," + screenNumber);
            writer.Close();
        }
        public string ReadSettingsFile(byte lineNumber)
        {
            TextReader reader = new StreamReader(settingsFilePath);
            string settingsLine = reader.ReadLine();
            reader.Close();
            string[] separatedLines = settingsLine.Split(',');
            return separatedLines[lineNumber];
        }
        public void ModifyFolderPath(string newPath)
        {
            CreateSettingsFile(newPath, ReadSettingsFile(overlayButtonLine), ReadSettingsFile(speakerVolumeLine)
                ,ReadSettingsFile(screenNumberLine));
        }
        public void ModifyOptions(string overlayButton, string speakerVolume, string screenNumber)
        {
            CreateSettingsFile(ReadSettingsFile(pathLine), overlayButton, speakerVolume, screenNumber);
        }
        public void FilterMemeFiles()
        {
            string supportedExtensions = "*.mp3,*.aac,*.wav,*.webm,*.m4a,*.mp4,*.mkv";
            string[] memeFiles = Directory.GetFiles(ReadSettingsFile(pathLine));
            foreach (string supportedMemeFile in memeFiles.Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
            {
                filteredMemeFiles.Add(supportedMemeFile);
            }
        }
        public int GetVirtualSpeakerNumber()
        {                         // -1 refers to the "Audio Mapper" and values starting from 0 refer to the devices you have
            int vbSpeaker = -2;   // so I used -2 so that it means the virtual speaker doesn't exist
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var caps = WaveOut.GetCapabilities(i);
                if (caps.ProductName.Contains("VB-Audio"))
                {
                    vbSpeaker = i;
                    break;
                }
            }
            return vbSpeaker;
        }
    }
}
