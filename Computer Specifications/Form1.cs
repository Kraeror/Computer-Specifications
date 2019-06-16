using System;
using System.Management.Instrumentation;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Computer_Specifications
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (label3.Text == "Random Acess Memory (RAM)")
            {
                long kbs;
                GetPhysicallyInstalledSystemMemory(out kbs).ToString();
                var result = kbs / 1024 / 1024;
                if (result < 1)
                {
                    var mbs = kbs / 1024;
                    label3.Text = "You've got less than a gigabyte of random acess memory (" + mbs + " megabytes)!";
                }
                else if (result == 1)
                {
                    label3.Text = "You've got only ONE GB of random acess memory!";
                }
                else
                {
                    label3.Text = "You've got " + result.ToString() + " GBs of random acess memory!";
                }
                label6.Text = GetCPU();
                label2.Text = GetGPU();
                label1.Text = GetOS().Substring(0, GetOS().LastIndexOf("|C")).Trim();
            }
        }
        public static string RemoveLastChars(string input, int amount = 2)
        {
            if (input.Length > amount)
                input = input.Remove(input.Length - amount);
            return input;
        }
        public static string GetCPU()
        {
            try
            {
                string Name = string.Empty;
                using (ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (ManagementObject MO in MOS.Get()) Name += MO["Name"] + "; ";
                }

                Name = RemoveLastChars(Name);
                return !string.IsNullOrEmpty(Name) ? Name : "N/A";
            }
            catch
            {
            }

            return "N/A";
        }
        public static string GetGPU()
        {
            try
            {
                string Name = string.Empty;
                using (ManagementObjectSearcher MOS =
                    new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration"))
                {
                    foreach (ManagementObject MO in MOS.Get()) Name += MO["Description"] + " ;";
                }

                Name = RemoveLastChars(Name);
                return !string.IsNullOrEmpty(Name) ? Name : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }
        public static string GetOS()
        {
            try
            {
                string Name = string.Empty;
                using (ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (ManagementObject MO in MOS.Get()) Name += MO["Name"] + "; ";
                }

                Name = RemoveLastChars(Name);
                return !string.IsNullOrEmpty(Name) ? Name : "N/A";
            }
            catch
            {
            }

            return "N/A";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AgentXzX (A.K.A. Kraeror) - The Developer!\nNinjahZ - Helped with much constructors!", "Credits");
        }
    }
}
