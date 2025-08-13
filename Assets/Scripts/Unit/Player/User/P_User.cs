using UnityEngine;

public class P_User : Player
{
    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;

    public void SetControlers(FixedJoystick _joystick)
    {
        Initialize();

        this._joystick = _joystick;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (_joystick == null) return;

        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * moveSpeed
        );
    }
}