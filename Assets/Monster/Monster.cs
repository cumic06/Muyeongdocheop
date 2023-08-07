using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

public class Monster : Unit, IDamageable
{


    [SerializeField]
    private GameObject monster;
    public GameObject _Monster { get => monster; }
    //private MonsterSkill
    [SerializeField]
    private bool OnAndOff;
    public bool _OnAndOff { get => OnAndOff; }

    public float diameter { get; set; }

    [SerializeField]
    private LayerMask layer;

    protected override void Start()
    {
        if (_OnAndOff)
        {
            StartCoroutine(Move());
            StartCoroutine(Flip());
        }
        base.Start();
    }

    private void Update()
    {
        AttackRange(diameter);
    }

    public IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            _Monster.transform.Translate(new Vector3(0.0025f * _Monster.transform.localScale.x, 0, 0));
        }
    }
    public IEnumerator Flip()
    {

        WaitForSeconds wait = new WaitForSeconds(2.5f);
        while (true)
        {
            yield return wait;
            _Monster.transform.localScale = new Vector3(-(_Monster.transform.localScale.x), 2, 2);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.collider.TryGetComponent(out Player player))
        //    _Monster.GetComponent<MonsterSkill>().Attack(player.gameObject);
    }


    public void AttackRange(float diameter)
    {
        RaycastHit2D hit = Physics2D.CircleCast(_Monster.transform.position, diameter, Vector2.right, 1,layer);
        if (hit)
        {
            _Monster.transform.Translate(hit.transform.position.normalized * 0.025f);
        }
    }
}
