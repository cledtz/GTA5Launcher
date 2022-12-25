using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTALauncher
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            options.Items.Add("Cockstar games launcher");
            options.Items.Add("EpicGaymes launcher");
            options.Items.Add("Steam launcher");

            options.SelectedItem = "Cockstar games launcher";
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DoJobThread()
        {
            status.Text = "Gathering installation informations...";
            gui.print("Gathering installation informations...");
            progressBar1.Value = 35;
            progressText.Text = progressBar1.Value.ToString() + "%";
            Thread.Sleep(100);
            if (options.SelectedItem == (object)"Cockstar games launcher")
            {
                gui.print("Opening registery...");
                progressBar1.Value = 60;
                progressText.Text = progressBar1.Value.ToString() + "%";
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Rockstar Games\\Launcher"))
                {
                    gui.print("Read registery key: " + registryKey);
                    string cnt = (string)((registryKey != null) ? registryKey.GetValue("InstallFolder") : null);
                    gui.print("Registery result: " + cnt);
                    progressBar1.Value = 80;
                    progressText.Text = progressBar1.Value.ToString() + "%";
                    if (cnt != null)
                    {
                        status.Text = "Launching...";
                        gui.print("Launching... -> " + cnt + "\\Launcher.exe" + " with parameter: -minmodeApp=gta5");
                        Process.Start(cnt + "\\Launcher.exe", "-minmodeApp=gta5");
                        progressBar1.Value = 100;
                        progressText.Text = progressBar1.Value.ToString() + "%";
                    }
                    else
                    {
                        status.Text = "Failure: 01x48";
                    }
                }
            }
            if (options.SelectedItem == (object)"EpicGaymes launcher")
            {
                gui.print("Launching...");
                progressBar1.Value = 80;
                progressText.Text = progressBar1.Value.ToString() + "%";
                status.Text = "Launching...";
                Process.Start("com.epicgames.launcher://apps/9d2d0eb64d5c44529cece33fe2a46482?action=launch&silent=true");
                progressBar1.Value = 100;
                progressText.Text = progressBar1.Value.ToString() + "%";
            }
            if (options.SelectedItem == (object)"Steam launcher")
            {
                gui.print("Launching...");
                progressBar1.Value = 80;
                progressText.Text = progressBar1.Value.ToString() + "%";
                status.Text = "Launching...";
                Process.Start("steam://run/271590");
                progressBar1.Value = 100;
                progressText.Text = progressBar1.Value.ToString() + "%";
            }

            Thread.Sleep(1000);
            gui.print("Success.");
            status.Text = "Success, you may now close the program...";
            gui.print("Waiting for exit...");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            Launch.Enabled = false;
            gui.print("Starting task thread...");
            Thread ac = new Thread(DoJobThread);
            ac.Start();
        }
    }
}
