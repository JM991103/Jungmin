using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ilogging
{
    int AttackPower { get; }

    void Attack(Ilogging target);
    void Defence(int damage);
}
