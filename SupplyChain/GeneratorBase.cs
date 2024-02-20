using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Strings;

namespace SupplyChain
{
    internal abstract class GeneratorBase
    {

        protected CodeEnvironment Env{ get; }
        protected bool Force { get; }

        protected GeneratorBase(CodeEnvironment env,  bool force)
        {
            Env = env;
            Force = force;
        }

        protected abstract string NewContent(string name);
        public void Generate(string name, string path)
        {
            if (!Path.IsPathRooted(path)) path = Path.Combine(Env.BasePath, path);
            var old = File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8).RemoveAllWhitespace() : "";
            var current = NewContent(name);
            if (!Force && old.Equals(current.RemoveAllWhitespace()))
            {
                Console.WriteLine("Unchanged.");
                return;
            }
            File.WriteAllText(path, current, Encoding.UTF8);
            Console.WriteLine("Done!");
        }
    }
}
