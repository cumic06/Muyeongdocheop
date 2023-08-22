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
        UpdateSystem.Instance.AddUpdateAction(CameraMoveManager);
    }

    private void CameraMoveManager()
    {
        bool Charging = UIManager.Instance.GetBaldoSkillUIActive() & BaldoSkill.Instance.CheckCharging() && BaldoSkill.Instance.TryWall(out float wallAngle, out Vector2Int normalInt, out RaycastHit2D wallHit, out Vector2 point);

        point = Vector2.zero;

        if (!Charging)
        {
            FollowPlayer();
        }
        else
        {
            FollowBalldoUI(point);
        }
    }

    private void FollowBalldoUI(Vector2 point)
    {
        Vector3 balldoUIVec = new(point.x, point.y, -10);

        transform.position = balldoUIVec;
    }

    private void FollowPlayer()
    {
        Vector2 slowPlayerFollow = Vector2.Lerp(transform.position, player.transform.position, cameraSpeed * Time.fixedDeltaTime);

        Vector3 moveVec = new(slowPlayerFollow.x, slowPlayerFollow.y, -10);
        transform.position = moveVec;
    }
}