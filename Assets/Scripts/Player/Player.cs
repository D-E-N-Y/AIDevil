using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * _moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * _moveSpeed
        );
    }
}
