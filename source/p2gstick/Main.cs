using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
// using System.Linq;
using p2gstick.Properties;
using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace p2gstick
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public static class netLegacyFunctions
        {
            // imitate .net > 4 String.IsNullOrWhitespace
            public static bool IsNullOrWhiteSpace(string aString)
            {
                if (aString != null)
                {
                    for (int i = 0; i < aString.Length; i++)
                    {
                        if (!char.IsWhiteSpace(aString[i]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void p2gstickWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://p2gstick.org");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Other other = new Other();
            other.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Other other = new Other();
            other.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            CardStatus cardStatus = new CardStatus();
            cardStatus.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encrypt encrypt = new Encrypt();
            encrypt.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Encrypt encrypt = new Encrypt();
            encrypt.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Decrypt decrypt = new Decrypt();
            decrypt.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Decrypt decrypt = new Decrypt();
            decrypt.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sign sign = new Sign();
            sign.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Sign sign = new Sign();
            sign.Show();
        }

        public static class Globals
        {
            public static string exePath = AppDomain.CurrentDomain.BaseDirectory;
            public static string gpgExe = exePath + "\\GPG4Win_Portable\\gpg2.exe";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("'The Answer to the Great Question... Of Life, the Universe and Everything... Is... Forty-two' said Deep Thought, with infinite majesty and calm.", "EASTER EGG!!!!!!1111 42!", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}
