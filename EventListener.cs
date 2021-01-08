using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventHook;
using System.IO;

namespace MemeMic
{

    class EventListener : Control
    {
        public EventHookFactory eventHookFactory = new EventHookFactory();
        public KeyboardWatcher keyboardWatcher;
        public MouseWatcher mouseWatcher;

        public void captureKeyboardEvent(TextBox textBox)
        {
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            keyboardWatcher.Start();
            keyboardWatcher.OnKeyInput += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    textBox.Text = e.KeyData.Keyname;
                }
                );
            };
        }
        public void captureMouseEvent(TextBox textbox)
        {
            mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start();

            mouseWatcher.OnMouseInput += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    String mouseEvent = e.Message.ToString();
                    if (!mouseEvent.Equals("WM_MOUSEMOVE"))
                    {
                        textbox.Text = e.Message.ToString();
                    }


                }
                );
            };
        }





    }

}
