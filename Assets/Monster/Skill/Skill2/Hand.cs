using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float _Time = 2;
    [SerializeField]
    private GameObject _player;
    public GameObject player { get => _player; }
    [SerializeField]
    private bool Check = false;
    public bool _check { get => Check; set => Check = value; }
    [SerializeField]
    private GameObject _hand;
    public GameObject hand { get => _hand; }
    [SerializeField]
    private Fsm[] _fsm = new Fsm[5];
    public Fsm[] fsm { get => _fsm; set => _fsm = value; }
    public float Patteren_Time { get; set; }
    public Vector3 SavePlayer { get; set; }
    public Coroutine _coroutine { get; set; }
    void Start()
    {
        gameObject.TryGetComponent(out Hand _Hand);
        _fsm[0] = new Hand_Fsm0(_Hand);
        _fsm[1] = new Hand_Fsm1(_Hand);
        _fsm[2] = new Hand_Fsm2(_Hand);
        _fsm[3] = new Hand_Fsm3(_Hand);
        _fsm[4] = _fsm[0];
        StartCoroutine(turn());
    }



    IEnumerator turn()
    {
        WaitForSeconds wait = new WaitForSeconds(_Time);
        while (true)
        {
            yield return wait;
            _fsm[4].Fsm_Action();
            yield return new WaitUntil(() => _check);
        }
    }

    public IEnumerator Timer(int X_num)
    {
        hand.gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        StopCoroutine(_coroutine);
        fsm[4] = fsm[1];
        hand.gameObject.SetActive(false);
        _check = true;
    }
}
