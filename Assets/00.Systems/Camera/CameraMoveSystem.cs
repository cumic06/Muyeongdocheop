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
        Vector3 balldoUIVec = new(SlowFollow(point).x, SlowFollow(point).y, -10);

        transform.position = balldoUIVec;
    }

    private void FollowPlayer()
    {

        Vector3 moveVec = new(SlowFollow(player.transform.position).x, SlowFollow(player.transform.position).y, -10);
        transform.position = moveVec;
    }

    private Vector2 SlowFollow(Vector2 target)
    {
        Vector2 slowPlayerFollow = Vector2.Lerp(transform.position, target, cameraSpeed * Time.fixedDeltaTime);
        return slowPlayerFollow;
    }
}