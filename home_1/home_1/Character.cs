﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_1
{
    public class Character
    {
        //private으로 변수들을 생성함
        private string name;            //이름
        private int hp = 100;           //체력
        private int maxHP = 100;        //max체력
        private int strenth = 10;       //힘
        private int dexterity = 5;      //민첩
        private int intellegence = 7;   //지능

        //nameArry에 기본값 설정 (선언과 할당을 동시에 함)
        string[] nameArry = { "전사", "마법사", "궁수", "도적", "해적" };

        //랜덤 함수 rand를 선언
        Random rand;

        public int HP
        {
            get //출력
            {
                return hp;
            }
            private set //출력
            {
                hp = value;
                if (hp > maxHP)
                {
                    hp = maxHP;
                }
                if (hp <= 0)
                {
                    //사망 처리용 함수 호출
                    Console.WriteLine($"{name} 이 사망");
                }
            }
        }

        public Character()
        {
            //rand에 새로운 랜덤값을 넣음
            rand = new Random();
            int randomNumber = rand.Next();
            randomNumber %= 5;
            name = nameArry[randomNumber];

            GenerateStatus();
            TestPrintStatus();
        }

        public Character(string newName)
        {
            rand = new Random();
            name = newName;

            GenerateStatus();
            TestPrintStatus();
        }

        private void GenerateStatus()
        {
            maxHP = rand.Next(100, 201);
            hp = maxHP;

            strenth = rand.Next(20)+1;
            dexterity = rand.Next(20)+1;
            intellegence = rand.Next(20)+1;
        }

        public void Attack(Character target)
        {
            int damage = strenth;
            Console.WriteLine($"{name}이 {target.name}에게 공격을 합니다. (공격력 {damage})");
            target.TakeDamage(damage);
         }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            Console.WriteLine($"{name}이 {damage}만큼의 피해를 입었습니다.");
        }

        public void TestPrintStatus()
        {
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine($"┃ 이름\t: {name}");
            Console.WriteLine($"┃ HP\t:{hp,4} / {maxHP,4}");
            Console.WriteLine($"┃ 힘\t: {strenth,2}");
            Console.WriteLine($"┃ 민첩\t: {dexterity,2}");
            Console.WriteLine($"┃ 지능\t: {intellegence,2}");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }
    }
}
