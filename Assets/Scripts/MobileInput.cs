using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput instance;

    public bool leftPressed;
    public bool rightPressed;
    public bool jumpPressed;
    public bool shootPressed;

    private void Awake()
    {
        instance = this;
    }

    public void LeftDown() { leftPressed = true; }
    public void LeftUp() { leftPressed = false; }

    public void RightDown() { rightPressed = true; }
    public void RightUp() { rightPressed = false; }

    public void JumpDown() { jumpPressed = true; }
    public void JumpUp() { jumpPressed = false; }

    public void ShootDown() { shootPressed = true; }
    public void ShootUp() { shootPressed = false; }
}

