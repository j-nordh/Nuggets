using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilClasses.Extensions.Exceptions;
namespace ScriptOMatic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Console.WriteLine(( e.ExceptionObject as Exception)?.DeepToString());
            RealMain(args);
        }
        static void RealMain(string[] args)
        {  
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string cmd = "";
            if (args.Any()) cmd = args[0].ToLower();
            switch (cmd)
            {
                case "generator":
                case "generate":
                case "gen":
                    Application.Run(new Generator());
                    break;
                case "compare":
                case "comp":
                    Application.Run(new CallCompare());
                    break;
                case "crud":
                    Application.Run(new CrudStudio());
                    break;
                case "rebuild":

                    break;
                default:
                    Application.Run(new Launcher());
                    break;
            }
        }
    }
}
