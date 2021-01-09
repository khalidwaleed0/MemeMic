using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MemeMic
{
    
    public static class AppSetup
    {
        public static string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\MemeMic\settings.txt";
        public static void createSettingsFile(string path, string overlayButton = "", string pushToTalkButton = "")
        {
            TextWriter writer = new StreamWriter(settingsFilePath);
            writer.WriteLine(path + "," + overlayButton + "," + pushToTalkButton);
            writer.Close();
        }

        public static string readSettingsFile(int lineNumber)
        {
            TextReader reader = new StreamReader(settingsFilePath);
            string settingsLine = reader.ReadLine();
            reader.Close();
            string[] separatedLines = settingsLine.Split(',');
            return separatedLines[lineNumber];
        }
        public static void modifyFolderPath(string newPath)
        {
            createSettingsFile(newPath, readSettingsFile(1), readSettingsFile(2));
        }

    }
}
