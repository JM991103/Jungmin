using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour , Ilogging
{
    public int hp = 3;

    public int HP
    {
        get => hp;
        set
        {
            hp = value;

            if (hp < 1)
            {
                Destroy(this.gameObject, 0.4f);
            }
        }
    }

    public int AttackPower => 0;

    public void Attack(Ilogging target)
    {        
    }

    public void Defence(int damage)
    {
        HP -= damage;
    }
}
