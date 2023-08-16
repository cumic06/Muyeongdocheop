using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    [SerializeField] //플레이어 공격 맞았을때 파괴되는거 아직 안만들었음
    private GameObject Manager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("닿음");
            StartCoroutine(Boom(player));
        }
    }
    IEnumerator Boom(Player player)
    {
        yield return new WaitForSeconds(15);
        Debug.Log(player.Hp);
        player.TakeDamage(5);
        Debug.Log(player.Hp);
        Manager.SetActive(false);
    }
}
