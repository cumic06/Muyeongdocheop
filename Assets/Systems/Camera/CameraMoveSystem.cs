using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSystem : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float cameraSpeed;

    [SerializeField] private float LimitYLowValue;
    [SerializeField] private float LimitYHighValue;
    [SerializeField] private float LimitXLowValue;
    [SerializeField] private float LimitXHighValue;

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
        transform.position = new Vector3(slowPlayerFollow.x, slowPlayerFollow.y, -10);
    }

    private void LimitFollow()
    {
        float limitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);
        float limitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);
        transform.position = new Vector3(limitX, limitY, -10);
    }
}