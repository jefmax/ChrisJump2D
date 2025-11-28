using UnityEngine;

public class MobileInputBridge : MonoBehaviour
{
    public void LeftDown() { MobileInput.instance.LeftDown(); Debug.Log("LeftDown called"); }
    public void LeftUp() { MobileInput.instance.LeftUp(); Debug.Log("LeftUp called"); }

    public void RightDown() { MobileInput.instance.RightDown(); Debug.Log("RightDown called"); }
    public void RightUp() { MobileInput.instance.RightUp(); Debug.Log("RightUp called"); }

    public void JumpDown() { MobileInput.instance.JumpDown(); Debug.Log("JumpDown called"); }
    public void JumpUp() { MobileInput.instance.JumpUp(); Debug.Log("JumpUp called"); }

    public void ShootDown() { MobileInput.instance.ShootDown(); Debug.Log("ShootDown called"); }
    public void ShootUp() { MobileInput.instance.ShootUp(); Debug.Log("ShootUp called"); }
}

