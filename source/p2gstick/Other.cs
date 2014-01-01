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
    public partial class Other : Form
    {
        public Other()
        {
            InitializeComponent();
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                button1.PerformClick();
            }
        }

        private static StringBuilder gpgOutput = null;

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
                ProcessStartInfo processStartInfo = new ProcessStartInfo(p2gstick.Main.Globals.gpgExe, consArgsTextBox.Text);
                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                gpgOutput = new StringBuilder("");
                processStartInfo.RedirectStandardError = true;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.CreateNoWindow = true;

                Process gnuPgProcess = new Process();
                gnuPgProcess.StartInfo = processStartInfo;
                gnuPgProcess.OutputDataReceived += new DataReceivedEventHandler(gnuPgProcessOutputHandler);
                gnuPgProcess.ErrorDataReceived += new DataReceivedEventHandler(gnuPgProcessErrorHandler);
                bool processStarted = gnuPgProcess.Start();

                if (processStarted)
                {
                    StreamWriter gpgNoInput = gnuPgProcess.StandardInput;
                    gnuPgProcess.BeginOutputReadLine();
                    gnuPgProcess.BeginErrorReadLine();

                    gpgNoInput.Close();
                    gnuPgProcess.WaitForExit(500);
                    outputRichTextBox.Text = gpgOutput.ToString();

                    gnuPgProcess.Close();
                }
            }
        }

        private static void gnuPgProcessOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                gpgOutput.Append(Environment.NewLine + outLine.Data);
            }
        }

        private static void gnuPgProcessErrorHandler(object sendingProcess, DataReceivedEventArgs errLine)
        {
            if (!Main.netLegacyFunctions.IsNullOrWhiteSpace(errLine.Data))
            {
                MessageBox.Show(errLine.Data, "Error");
            }
        }

        private void Other_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

    }
}
