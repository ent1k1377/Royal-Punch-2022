using UnityEngine;

namespace Resources.Scripts.Joystick
{
    public class DynamicDisappearingJoystick : JoystickHandler
    {
        public Vector3 GetDirection()
        {
            if (InputVector.x != 0 || InputVector.y != 0)
                return new Vector3(InputVector.x, 1, InputVector.y);
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
