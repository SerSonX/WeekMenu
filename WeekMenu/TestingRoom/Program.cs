using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "10.07.1995";
            var cur = DateTime.Now.Date;
            DateTime d;
            if (DateTime.TryParse(s, out d))
                Console.WriteLine(cur);
            else
                Console.WriteLine("No");
            Console.ReadLine();
        }
    }
}
