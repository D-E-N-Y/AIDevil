using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacterMovement : MonoBehaviour 
{
    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;


    private UnitStats _stats;

    private float _moveSpeed;

    public void Initialize(UnitStats stats)
    {
        _stats = stats;
        SetStats();

        _stats.OnStatChanged += UpdateStats;
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void UpdateStats(StatType statType)
    {
        if(statType == StatType.BaseMoveSpeed || statType == StatType.MoveSpeedModifier)
        {
            SetStats();
        }
    }

    private void SetStats()
    {
        _moveSpeed = _stats.BaseMoveSpeed * _stats.MoveSpeedModifier;
    }

    public void SetControlers(FixedJoystick joystick)
    {
        _joystick = joystick;
    }

    private void FixedUpdate()
    {
        if (_joystick == null || _rigidbody == null) return;

        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * _moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * _moveSpeed
        );
    }
}