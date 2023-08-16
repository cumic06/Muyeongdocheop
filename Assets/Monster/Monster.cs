using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;
using System;

public class Monster : Unit, IDamageable
{
    [SerializeField]
    private bool Check = false;
    public bool _check { get => Check; }
    public Fsm[] _fsm { get; set; }
    public Fsm Save_Fsm { get; set; }
    public RaycastHit2D hit { get; set; }
    [SerializeField]
    public GameObject Skill1_;
    public GameObject Skill1 { get => Skill1_; }
    protected override void Start()
>>>>>>> KimTaeYeon
    {
        gameObject.TryGetComponent(out Monster _monster);
        _fsm = new Fsm[7];
        _fsm[0] = new Fsm_Patteren1(_monster);
        _fsm[1] = new Fsm_Patteren2(_monster);
        _fsm[2] = new Fsm_Patteren3(_monster);
        _fsm[3] = new Fsm_Patteren4(_monster);
        _fsm[4] = new Fsm_Patteren5(_monster);
        _fsm[5] = new Fsm_Patteren6(_monster);
        _fsm[6] = _fsm[0];
        StartCoroutine(Patteren());
        base.Start();
    }
    IEnumerator Patteren()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        while (true)
        {
            yield return wait;
            _fsm[6].Fsm_Action();
            yield return new WaitUntil(() => _check);

        }
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
//public void AttackRange(float diameter)
//{
//    hit = Physics2D.CircleCast(_Monster.transform.position, diameter, Vector2.right, 1, layer);
//}

//private void Fsm_Change()
//{

//}
//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.collider.TryGetComponent(out Player player))
//        _Monster.GetComponent<MonsterSkill>().Attack(player.gameObject);
//}