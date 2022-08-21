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
            Human human2 = new Human();

            human1.Attack(human2);


            Console.ReadLine();
        }
    }
}
