using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeClassLib;

namespace Stoper {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Stoper!");
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            var timePeriod = new TimePeriod(0);
            var second = new TimePeriod(1);

            while(true) {
                Console.Clear();
                Console.WriteLine("Time: " + timePeriod);
                Thread.Sleep(1000);
                timePeriod += second;
            }
        }
    }
}
