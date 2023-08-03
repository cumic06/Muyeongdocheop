using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSystem : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float cameraSpeed;

    private readonly float LimitYLowValue = 0f;
    private readonly float LimitYHighValue = 10f;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 slowFollowPos = Vector3.Lerp(transform.position, player.transform.position, cameraSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, player.transform.position) > 10f)
        {
            transform.position = new Vector3(slowFollowPos.x, slowFollowPos.y, -10);
            Debug.Log("멀어");
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
            Debug.Log("가까워");
        }
    }
}