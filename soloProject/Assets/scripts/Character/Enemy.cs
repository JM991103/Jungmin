using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Ilogging
{
    Animator anim;
    public int hp = 3;
    public int attackPower = 1;
    public int AttackPower => attackPower;

    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            
            if (hp < 1)
            {
                Die();
            }
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack(Ilogging target)
    {
        //target?.Defence(AttackPower);
    }

    public void Defence(int damage)
    {
        HP -= damage;
        anim.SetTrigger("IsHit");
    }

    void Die()
    {
        anim.SetTrigger("IsDead");
        Destroy(this.gameObject,1.5f);
    }
}
