using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{

}

public struct UnitStatInfo
{
    public int MaxHp;
    public float MoveSpeed;
    public float AttackPower;
}

public class Unit : MonoBehaviour
{
    UnitStatInfo unitStatInfo;

    public void OnHit(int damage)
    {

    }
}
