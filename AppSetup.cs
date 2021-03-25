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
        public const byte pushToTalkLine = 2;
        private string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\MemeMic";
        public string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\MemeMic\settings.txt";
        
        public static AppSetup getInstance()
        {
            if (appSetup == null)
                appSetup = new AppSetup();
            return appSetup;
        }
        
        public void checkDirectory()
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
        public void checkSettingsFile()
        {
            if (!File.Exists(settingsFilePath))
                createSettingsFile();
        }
        public void createSettingsFile(string path = "", string overlayButton = "", string pushToTalkButton = "")
        {
            TextWriter writer = new StreamWriter(settingsFilePath);
            writer.WriteLine(path + "," + overlayButton + "," + pushToTalkButton);
            writer.Close();
        }
        public string readSettingsFile(byte lineNumber)
        {
            TextReader reader = new StreamReader(settingsFilePath);
            string settingsLine = reader.ReadLine();
            reader.Close();
            string[] separatedLines = settingsLine.Split(',');
            return separatedLines[lineNumber];
        }
        public void modifyFolderPath(string newPath)
        {
            createSettingsFile(newPath, readSettingsFile(overlayButtonLine), readSettingsFile(pushToTalkLine));
        }
        public void modifyButtons(string overlayButton)
        {
            createSettingsFile(readSettingsFile(pathLine), overlayButton);
        }
        public void filterMemeFiles()
        {
            string supportedExtensions = "*.mp3,*.aac,*.wav,*.webm,*.m4a,*.mp4,*.mkv";
            string[] memeFiles = Directory.GetFiles(readSettingsFile(pathLine));
            foreach (string supportedMemeFile in memeFiles.Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower())))
            {
                filteredMemeFiles.Add(supportedMemeFile);
            }
        }
        public int getVirtualSpeakerNumber()
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
