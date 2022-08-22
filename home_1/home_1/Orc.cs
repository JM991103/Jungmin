using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_1
{
    internal class Orc : Character
    {
        bool OrcSkill = false;

        public Orc(string newName) : base(newName)
        {
          
        }

        public override void GenerateStatus()
        {
            
            
        }

        public override void TestPrintStatus()
        {
            
            
        }


        public override void Attack(Character target)
        {
            base.Attack(target);
            int damage = strenth;

            if (OrcSkill == true)
            {
                damage *= 2;
                Console.WriteLine("스킬을 사용하여 공격합니다.");

                OrcSkill = false;
            }


            if (rand.NextDouble() < 0.3f)
            {

                damage *= 2;
                Console.WriteLine("크리티컬 히트!");
            }

            Console.WriteLine($"{name}이(가) {target.name}에게 공격을 합니다. (공격력 {damage})");
            target.TakeDamage(damage);
        }


    }
}
