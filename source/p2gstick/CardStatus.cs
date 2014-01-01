using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
// using System.Linq;
using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;

namespace p2gstick
{
    public partial class CardStatus : Form
    {
        public CardStatus()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (!File.Exists(p2gstick.Main.Globals.gpgExe))
            {
                MessageBox.Show("Your USB flash drive does not contain the GnuPG executable. Please make sure you use the correct software downloaded from p2gstick.org.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                error = true;
            }
            if (!error)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(p2gstick.Main.Globals.gpgExe, "--card-status");
                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.CreateNoWindow = true;

                Process gnuPgProcess = new Process();
                gnuPgProcess.StartInfo = processStartInfo;
                bool processStarted = gnuPgProcess.Start();

                if (processStarted)
                {
                    StreamReader outputReader = gnuPgProcess.StandardOutput;
                    string output = "";

                    output = gnuPgProcess.StandardOutput.ReadToEnd();
                    gnuPgProcess.WaitForExit();

                    if (output == "")
                    {
                        output = "No card inserted.";
                    }
                    outputRichTextBox.Text = output;
                }
            }
        }

        private void CardStatus_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}
