using System;
using UnityEngine;

public class Player : U_Mortal
{
    public Action onDead;

    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize(FixedJoystick _joystick)
    {
        Initialize();

        this._joystick = _joystick;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Initialize()
    {
        base.Initialize();
        gameObject.SetActive(true);
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

    public override void Death()
    {
        onDead.Invoke();
        base.Death();
    }
}
