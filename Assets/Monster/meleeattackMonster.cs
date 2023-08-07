using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeattackMonster : Monster, MonsterSkill
{
    
    public void Attack(GameObject Object)
    {
        Debug.Log(Object);
    }
    protected override void Start()
    {
        diameter =  6;
        base.Start();
    }
}
