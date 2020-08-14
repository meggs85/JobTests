using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser;

namespace ConsoleWebParser
{
    class Program
    {
        static void Main(string[] args)
        {
            DataModel model = new Parser().ParseDocument("https://www.kellysubaru.com/used-inventory/index.htm");
            model.Output();
            Console.Read();
        }
    }
}
