using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
}

public interface MonsterSkill
{
    public void Attack(GameObject Object);
}

public interface Fsm
{
    public void Fsm_Action();
}