public class MoveJoyStick : JoyStick
{
    public static MoveJoyStick Instance;

    private void Awake()
    {
        Instance = GetComponent<MoveJoyStick>();
    }

    protected override void ReSetJoystickPos()
    {
        base.ReSetJoystickPos();
        SetJoyStickHorizontalValue();
    }

    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizontalValue() != 0;
    }
}
