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

    private void Start()
    {
        UpdateSystem.Instance.AddFixedUpdateAction(CameraMoveManager);
    }

    private void CameraMoveManager()
    {
        FollowPlayer();
        LimitMove();
    }

    private void FollowPlayer()
    {
        Vector2 slowPlayerFollow = Vector2.Lerp(transform.position, player.transform.position, cameraSpeed * Time.fixedDeltaTime);

        Vector3 moveVec = new(slowPlayerFollow.x, slowPlayerFollow.y, -10);
        transform.position = moveVec;
    }

    private void LimitMove()
    {
        float limitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);
        float limitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);

        Vector3 limitVec = new(limitX, limitY+0.2f, -10);

        transform.position = limitVec;
    }
}