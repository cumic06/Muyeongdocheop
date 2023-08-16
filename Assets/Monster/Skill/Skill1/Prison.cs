using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    [SerializeField] //�÷��̾� ���� �¾����� �ı��Ǵ°� ���� �ȸ������
    private GameObject Manager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("����");
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
