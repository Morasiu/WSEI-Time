using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeClassLib;

namespace Minutnik {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Minutnik");
            Console.WriteLine("Enter your time if format hh:mm:ss");
            Time time;
            try {
                time = new Time(Console.ReadLine());
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }

            var zero = new Time(0, 0, 0);
            while(time != zero) {
                Console.Clear();
                time -= new Time(0, 0, 1);
                Console.WriteLine("Left: " + time);
                Thread.Sleep(1000);
            }

            Console.WriteLine("No time left!");
            Console.ReadKey();
        }
    }
}
