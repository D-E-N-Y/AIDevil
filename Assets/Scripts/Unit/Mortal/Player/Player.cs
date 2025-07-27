using UnityEngine;

public class Player : U_Mortal
{
    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;

    public virtual void Initialize(FixedJoystick _joystick)
    {
        Initialize();

        this._joystick = _joystick;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystick == null) return;

        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * _moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * _moveSpeed
        );
    }
}
