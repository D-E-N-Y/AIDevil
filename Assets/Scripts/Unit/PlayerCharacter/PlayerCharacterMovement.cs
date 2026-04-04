using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacterMovement : MonoBehaviour, IUnitMovement 
{
    [SerializeField] private Transform _model;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Collider _unitCollider;

    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;

    private UnitStats _stats;

    private float _moveSpeed;

    private bool _isDashing;
    public bool IsDashing => _isDashing;

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

        if (_isDashing) return;

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

    public void Dash(float dashDistance, float dashSpeed)
    {
        if (_rigidbody == null) return;

        Vector3 startPos = _rigidbody.position;
        Vector3 targetPos = startPos + _model.forward * dashDistance;

        StartCoroutine(DashRoutine(targetPos, dashSpeed));
    }

    private IEnumerator DashRoutine(Vector3 targetPos, float dashSpeed)
    {
        _isDashing = true;
        _unitCollider.enabled = false;

        var oldConstraints = _rigidbody.constraints;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        float remainingDistance = Vector3.Distance(_rigidbody.position, targetPos);
        float maxTime = 1f;
        float elapsed = 0f;

        while (remainingDistance > 0.01f && elapsed < maxTime)
        {
            Vector3 newPos = Vector3.MoveTowards(_rigidbody.position, targetPos, dashSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);

            remainingDistance = Vector3.Distance(_rigidbody.position, targetPos);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _rigidbody.MovePosition(targetPos);

        _rigidbody.constraints = oldConstraints;
        _unitCollider.enabled = true;
        _isDashing = false;
    }
}