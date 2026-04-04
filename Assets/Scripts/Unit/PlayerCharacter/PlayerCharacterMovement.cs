using Unity.Barracuda;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacterMovement : MonoBehaviour 
{
    [SerializeField] private Transform _model;
    [SerializeField] private Animator _animator;
    
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

        CalcVelocity();
        CalcRotation();

        AnimateMovement();
    }

    private void CalcVelocity()
    {
        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * _moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * _moveSpeed
        );
    }

    private void CalcRotation()
    {
        if (_model == null) return;       

        Vector3 direction = _rigidbody.velocity.normalized;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _model.rotation = Quaternion.Slerp(_model.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void AnimateMovement()
    {
        if (_animator == null) return;

        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
    }
}