using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Monster : Unit
{
    public float _Time;
    public GameObject Effect { get; set; }

    [SerializeField]
    private GameObject _player;
    public GameObject Player { get => _player; }

    [SerializeField]
    private GameObject _hand;
    public GameObject hand { get => _hand; }

    [SerializeField]
    private IFsm[] _fsm = new IFsm[4];
    public IFsm[] fsm { get => _fsm; set => _fsm = value; }

    [SerializeField]
    private GameObject[] position0 = new GameObject[3];
    public GameObject[] Position0 { get => position0; }

    [SerializeField]
    private GameObject[] position1 = new GameObject[2];
    public GameObject[] Position1 { get => position1; }

    [SerializeField]
    private GameObject[] position2 = new GameObject[2];
    public GameObject[] Position2 { get => position2; }


    [SerializeField]private bool check = false;
    public bool Check { get => check; set => check = value; }

    public float Patteren_Time { get; set; }
    public Vector3 SavePlayer { get; set; }
    public Coroutine _coroutine { get; set; }



    public GameObject[] StrObject = new GameObject[3];

    [SerializeField]
    private GameObject[] handObject = new GameObject[3];
    void Awake()
    {
        GetEffectObject();
        SetFsmPattern();
        StartCoroutine(Turn());
        
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Die());
    }



    // -------------------------------------------------------------------------------


    IEnumerator Die()
    {
        yield return new WaitUntil(()=>Hp <= 0);
        gameObject.SetActive(false);
    }







    protected override void ResetHp()
    {
        unitStat.MaxHp = 1;
        base.ResetHp();
    }

    protected override void ResetSpeed()
    {

    }

    protected override void ResetAttackPower()
    {
        unitStat.AttackPower = 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(UnitStat.AttackPower);
        }
    }



    // -------------------------------------------------------------------------------





    private void SetFsmPattern()
    {
        gameObject.TryGetComponent(out Monster _Hand);
        _fsm[0] = new Hand_Fsm0(_Hand);
        _fsm[1] = new Hand_Fsm1(_Hand);
        _fsm[2] = new Hand_Fsm2(_Hand);
        _fsm[3] = _fsm[0];
    }

    private void GetEffectObject()
    {
        Effect = transform.GetChild(0).gameObject;

    }


    IEnumerator Turn()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            _fsm[3].Fsm_Action();
            yield return new WaitUntil(() => Check);
        }
    }

    public IEnumerator PosMove(GameObject[] Pos)
    {
        while (true)
        {

            for (int i = 0; i < Pos.Length; i++)
            {
                Vector3 startPos = transform.position;
                Vector3 targetPos = Pos[i].transform.position;
                transform.rotation = Pos[i].transform.rotation;
                for (float j = 0; j < 1; j += Time.fixedDeltaTime * 1.3f)
                {
                    transform.position = Vector3.Lerp(startPos, targetPos, j);
                    yield return new WaitForFixedUpdate();
                }


                if (i == 0 && IsSmashing())
                {
                    Effect.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    Effect.gameObject.SetActive(false);
                }
                if (IsSmashing())
                {
                    yield return new WaitForSeconds(_Time);
                }
            }
            if (IsSmashing())
            {

                yield return new WaitForSeconds(4 - (_Time * 2));
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
            }
        }
    }


    private bool IsSmashing()
    {
        return fsm[1] == fsm[3];
    }

    public IEnumerator Timer(int X_num)
    {

        yield return new WaitForSeconds(10);
        StopCoroutine(_coroutine);
        fsm[3] = fsm[Random.Range(0, 3)];
        Check = true;
    }

    public void InputSetActive(int[] Index)
    {
        foreach (var item in Index)
        {
            handObject[item].SetActive((handObject[item].activeSelf) ? true : false);
        }
    }


    
}
