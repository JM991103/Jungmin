using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace home_1
{
    internal class Human : Character
    {
        int mp, maxMP;
        bool Skill = false;


        public Human()
        {

        }

        public override void GenerateStatus()
        {
            base.GenerateStatus();
            mp = rand.Next()%100;
            maxMP = mp;
        }

        public override void TestPrintStatus()
        {
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine($"┃ 이름\t: {name}");
            Console.WriteLine($"┃ HP\t:{hp,4} / {maxHP,4}");
            Console.WriteLine($"┃ MP\t:{mp,4} / {maxMP,4}");
            Console.WriteLine($"┃ 힘\t: {strenth,2}");
            Console.WriteLine($"┃ 민첩\t: {dexterity,2}");
            Console.WriteLine($"┃ 지능\t: {intellegence,2}");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }

        public override void Attack(Character target)
        {
            base.Attack(target);
            int damage = strenth;

            if (Skill == true)
            {
                damage = damage * 2;
                Skill = false;
            }

            if (rand.NextDouble() < 0.3f)
            {
                damage *= 2;
                Console.WriteLine("크리티컬 히트!");
            }

            Console.WriteLine($"{name}이(가) {target.name}에게 공격을 합니다. (공격력 {damage})");
            target.TakeDamage(damage);
        }

        public void HumanSkill(Character target)
        {
            Skill = true;
            Console.WriteLine($"{name}이(가) 스킬을 사용했습니다.");
            Console.WriteLine($"{name}이(가) 데미지가 2배가 됩니다.");
            Attack(target);
        }






    }
}
