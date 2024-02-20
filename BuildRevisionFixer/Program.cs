using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilClasses.Extensions.Exceptions;
using UtilClasses.Extensions.Strings;

namespace BuildRevisionFixer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Console.WriteLine((e.ExceptionObject as Exception)?.DeepToString());
            RealMain(args);
        }
        //Path 
        static void RealMain(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No command line arguments provided, starting GUI...");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                var path = args[0];

                //!Directory.Exists(dirPath)
                if (!File.Exists(path) && !Path.GetExtension(path).EndsWithOic("csproj"))
                {
                    Console.WriteLine("Invalid file path, terminating program");
                    return;
                }

                var majorVer = 1;
                var minorVer = 0;
                var buildVer = 0;
                if (args.Length > 1 && !int.TryParse(args[1], out majorVer)) majorVer = 1;
                if (args.Length > 2) int.TryParse(args[2], out minorVer);
                if (args.Length > 3) int.TryParse(args[3], out buildVer);

                Console.WriteLine($"Running BuildRevisionFixer with \n Path :{path}, Ver: {majorVer}.{minorVer}.{buildVer}");

                BuildRevisionFixer.Run(path, majorVer, minorVer, buildVer);

                Console.WriteLine("Run was succesful, terminating");
                
            }
            
        }
    }
}
