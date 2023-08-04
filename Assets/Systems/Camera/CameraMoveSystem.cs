using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSystem : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float cameraSpeed;

    [SerializeField] private Vector2 size;

    private readonly float LimitYLowValue = 0f;
    private readonly float LimitYHighValue = 10f;
    private readonly float LimitXLowValue = 0f;
    private readonly float LimitXHighValue = 10f;

    private void Update()
    {
        LimitFollow();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 slowPlayerFollow = Vector3.Lerp(transform.position, player.transform.position, cameraSpeed * Time.fixedDeltaTime);

        //if (Vector2.Distance(transform.position, player.transform.position) >= 1.7f)
        //{
        //    transform.position = new Vector3(player.transform.position.x, slowPlayerFollow.y, -10);
        //    Debug.Log("가까워");
        //}
        //else
        //{
        transform.position = new Vector3(slowPlayerFollow.x, slowPlayerFollow.y, -10);
        //    Debug.Log("멀어");
        //}
    }

    //이거 쓰면 멀어 계속 뜸 수정해야함
    private void LimitFollow()
    {
        float limitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);
        float limitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);
        transform.position = new Vector3(limitX, limitY, -10);
    }
}