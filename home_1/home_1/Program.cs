using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
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
            

            while (!(human1.HP == 0) || !(enmay.HP == 0))
            {
                Console.WriteLine("숫자를 입력하세요");
                Console.WriteLine("1. 공격 2. 방어 3. 도망");
                string a = Console.ReadLine();
                int.TryParse(a, out int num);

                switch (num)
                {
                    case 1:
                        Console.WriteLine("숫자를 입력하세요");
                        Console.WriteLine("1. 공격 2. 스킬 3. 뒤로가기");
                        string b = Console.ReadLine();
                        int.TryParse(a, out int num2);
                        switch (num2)
                        {
                            case 1:
                                human1.Attack(enmay);
                                break;
                            case 2:
                                human1.HumanSkill(enmay);
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("숫자를 다시 입력해주세요");
                                break;
                        }
                        break;
                    case 2:
                        human1.brr();
                        break;
                    case 3:
                        break;

                    default:
                        continue;
                        break;
                        
                }
            }





           





            Console.ReadLine();
        }
    }
}
