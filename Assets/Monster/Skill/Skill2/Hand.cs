using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject Effect {get;set;}


     [SerializeField]
    GameObject[] Position = new GameObject[2];
    public GameObject[] _position { get => Position; }
    [SerializeField]
    private bool Check = false;
    public bool _check { get => Check; set => Check = value; }
    public float Patteren_Time { get; set; }
    public Vector3 SavePlayer { get; set; }
    public Coroutine _coroutine { get; set; }
    void Start()
    {
        Effect = transform.GetChild(0).gameObject;
        gameObject.TryGetComponent(out Hand _Hand);
        _fsm[0] = new Hand_Fsm0(_Hand);
        _fsm[1] = new Hand_Fsm1(_Hand);
        _fsm[2] = new Hand_Fsm2(_Hand);
        _fsm[3] = new Hand_Fsm3(_Hand);
        _fsm[4] = _fsm[1];
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
