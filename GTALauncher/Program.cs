using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTALauncher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                gui.debug_prints = false;
                ShowWindow(GetConsoleWindow(), SW_HIDE);
                gui.print("Normal");
            }
            else
            {
                var command = args[0];

                switch (command)
                {
                    case "-p" when args.Length == 2:
                        {
                            if (args[1] == "debug")
                            {
                                gui.debug_prints = true;
                                ShowWindow(GetConsoleWindow(), SW_SHOW);
                                gui.print("Debug");
                            }
                            if (args[1] == "contact" || args[1] == "credits")
                            {
                                Process.Start("https://github.com/celerityXD");
                                Environment.Exit(0);
                            }
                            break;
                        }
                    default:
                        gui.print("Invalid parameter.");
                        break;
                }

            }
            Console.Title = "GTALauncher debug prints";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
