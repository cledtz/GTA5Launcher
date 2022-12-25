using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTALauncher
{
    internal class gui
    {
        public static bool debug_prints = false;
        public static void print(string msg, int ver = 0)
        {
            if (ver == 0)
            {
                if (debug_prints)
                Console.WriteLine("[github.com/cledtz/GTA5Launche] " + msg);
            }
        }
    }
}
