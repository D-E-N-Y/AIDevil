using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IUnitMovement
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Collider _unitCollider;

    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;

    private UnitStats _stats;

    private bool _isDashing;
    public bool IsDashing => _isDashing;

    public Vector3 Direction => transform.forward;

    public void Initialize(UnitStats stats)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        
        _stats = stats;
        SetStats();

        _stats.OnStatChanged += UpdateStats;
    }

    public void SetStopDistance(float distance)
    {
        if (_agent == null) return;
        _agent.stoppingDistance = distance;
    }

    public void MoveTo(Vector3 position)
    {
        if (_agent == null) return;
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        if (_agent == null) return;
        _agent.ResetPath();
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
        _agent.speed = _stats.BaseMoveSpeed * _stats.MoveSpeedModifier;
    }

    void FixedUpdate()
    {
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        if (_animator == null) return;

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void Dash(float dashDistance, float dashSpeed)
    {
        if (_rigidbody == null) return;

        Vector3 startPos = _rigidbody.position;
        Vector3 targetPos = startPos + transform.forward * dashDistance;

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