using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedattackMonster : Monster, MonsterSkill
{


    protected override void Start()
    {
        diameter = 3;
        base.Start();
    }
    public void Attack(GameObject Object)
    {
        Debug.Log(Object);
    }
    
}
