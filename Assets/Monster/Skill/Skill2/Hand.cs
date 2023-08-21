using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Hand : MonoBehaviour
{
    public float _Time;
    [SerializeField]
    private GameObject _player;
    public GameObject player { get => _player; }
    [SerializeField]
    private GameObject _hand;
    public GameObject hand { get => _hand; }
    [SerializeField]
    private Fsm[] _fsm = new Fsm[5];
    public Fsm[] fsm { get => _fsm; set => _fsm = value; }
    public GameObject Effect { get; set; }
    [SerializeField]
    GameObject[] Position1 = new GameObject[2];
    public GameObject[] _position1 { get => Position1; }
    [SerializeField]
    GameObject[] Position2 = new GameObject[2];
    public GameObject[] _position2 { get => Position2; }


    [SerializeField]
    private bool Check = false;
    public bool _check { get => Check; set => Check = value; }
    public float Patteren_Time { get; set; }
    public Vector3 SavePlayer { get; set; }
    public Coroutine _coroutine { get; set; }
    void Start()
    {
        Effect = transform.GetChild(0).gameObject;
        Debug.Log(Effect.name);
        gameObject.TryGetComponent(out Hand _Hand);
        _fsm[0] = new Hand_Fsm0(_Hand);
        _fsm[1] = new Hand_Fsm1(_Hand);
        _fsm[2] = new Hand_Fsm2(_Hand);
        _fsm[3] = new Hand_Fsm3(_Hand);
        _fsm[4] = _fsm[2];
        StartCoroutine(Turn());
    }



    IEnumerator Turn()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            _fsm[4].Fsm_Action();
            yield return new WaitUntil(() => _check);
        }
    }

    public IEnumerator PosMove(GameObject[] Pos)
    {
        while (true)
        {
            for (int i = 0; i < Pos.Length; i++)
            {
                if (_fsm[4] == _fsm[1])
                {
                    yield return new WaitForSeconds(_Time);
                }
                Vector3 startPos = Pos[i].transform.position;
                Vector3 targetPos = Pos[i].transform.position;
                for (float j = 0; j < 1; j += Time.fixedDeltaTime * 1.3f)
                {
                    transform.position = Vector3.Lerp(startPos, targetPos, j);
                    yield return new WaitForFixedUpdate();
                }
                if (i == 0 && _fsm[4] == _fsm[1])
                {
                    Effect.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    Effect.gameObject.SetActive(false);
                }
            }
            if (_fsm[4] == _fsm[1])
            {
                yield return new WaitForSeconds(4 - (_Time * 2));
            }
            else
                yield return new WaitForSeconds(1.5f);
        }
    }


    //public IEnumerator Timer(int X_num)
    //{
    //    hand.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(10);
    //    StopCoroutine(_coroutine);
    //    fsm[4] = fsm[1];
    //    hand.gameObject.SetActive(false);
    //    _check = true;
    //}


}
