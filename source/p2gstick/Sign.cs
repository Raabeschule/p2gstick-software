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
    public partial class Sign : Form
    {
        public Sign()
        {
            InitializeComponent();
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
                string consArgs;

                if (radioButton1.Checked)
                {
                    consArgs = "-s";
                }
                else if (radioButton2.Checked)
                {
                    consArgs = "--clearsign";
                }
                else if (radioButton3.Checked)
                {
                    consArgs = "-b";
                }
                else
                {
                    MessageBox.Show("No signing method selected. Using 'Sign'.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    consArgs = "-s";
                }
                consArgs += " -a";

                ProcessStartInfo processStartInfo = new ProcessStartInfo(p2gstick.Main.Globals.gpgExe, consArgs);
                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                gpgOutput = new StringBuilder("");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;

                Process gnuPgProcess = new Process();
                gnuPgProcess.StartInfo = processStartInfo;
                gnuPgProcess.OutputDataReceived += new DataReceivedEventHandler(gnuPgProcessOutputHandler);
                gnuPgProcess.ErrorDataReceived += new DataReceivedEventHandler(gnuPgProcessErrorHandler);
                bool processStarted = gnuPgProcess.Start();

                if (processStarted)
                {
                    StreamWriter inputWriter = gnuPgProcess.StandardInput;
                    gnuPgProcess.BeginOutputReadLine();
                    gnuPgProcess.BeginErrorReadLine();

                    inputWriter.WriteLine(messageTextBox.Text);
                    inputWriter.Close();

                    gnuPgProcess.WaitForExit();
                    signatureTextBox.Text = gpgOutput.ToString();

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

        private void button3_Click(object sender, EventArgs e)
        {
            messageTextBox.Clear();
            signatureTextBox.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            signedMessageTextBox.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (!File.Exists(p2gstick.Main.Globals.gpgExe))
            {
                MessageBox.Show("Your USB flash drive does not contain the GnuPG executable. Please make sure you use the correct software downloaded from p2gstick.org.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                error = true;
            }
            if (!error)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(p2gstick.Main.Globals.gpgExe, "--verify");
                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                gpgOutput = new StringBuilder("");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;

                Process gnuPgProcess = new Process();
                gnuPgProcess.StartInfo = processStartInfo;
                gnuPgProcess.OutputDataReceived += new DataReceivedEventHandler(gnuPgProcessOutputHandler);
                gnuPgProcess.ErrorDataReceived += new DataReceivedEventHandler(gnuPgProcessErrorHandler);
                bool processStarted = gnuPgProcess.Start();

                if (processStarted)
                {
                    StreamWriter inputWriter = gnuPgProcess.StandardInput;
                    gnuPgProcess.BeginOutputReadLine();
                    gnuPgProcess.BeginErrorReadLine();

                    inputWriter.WriteLine(signedMessageTextBox.Text);
                    inputWriter.Close();

                    gnuPgProcess.WaitForExit();
                    gnuPgProcess.Close();
                }
            }
        }
    }
}
