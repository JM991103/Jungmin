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
            Human human1 = new Human();
            Orc enmay = new Orc("오크");

            human1.Attack(enmay);
            enmay.Attack(human1);
           

            Console.ReadLine();
        }
    }
}
