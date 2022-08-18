using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_1
{
    static class Program
    {
        static void Main(string[] args)
        {
            Character human1 = new Character();
            Character human2 = new Character("새한얀");

            human1.Attack(human2);
            human1.TestPrintStatus();
            human2.TestPrintStatus();
            human2.Attack(human1);
            human2.TestPrintStatus();
            human1.TestPrintStatus();


            Console.ReadLine();
        }
    }
}
