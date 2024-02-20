using System;

namespace SQLInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var res = SQLInjector.Run("Eco");

            Console.WriteLine(res);
        }
    }
}
