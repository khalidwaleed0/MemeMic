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
    class AppSetup
    {
        public static void createText(string path)
        {
            TextWriter writer = new StreamWriter("path.txt");
            writer.WriteLine(path);
            writer.Close();

        }

        public static void readText()
        {
            TextReader reader = new StreamReader("path.txt");

            //    MessageBox.Show(reader.ReadToEnd());
            reader.Close();
        }
    }
}
