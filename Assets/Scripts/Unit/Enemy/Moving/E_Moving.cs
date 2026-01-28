using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class E_Moving : Enemy
{
    protected NavMeshAgent _agent;
    protected MovingEnemyState _state;

    public override void Initialize()
    {
        base.Initialize();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.BaseMoveSpeed * _stats.MoveSpeedModifier;
        _agent.stoppingDistance = attackRange;

        _state = MovingEnemyState.Moving;
    }

    protected virtual void MoveToPosition(Vector3 _position)
    {
        if (_position == null) return;
        _agent.SetDestination(_position);
    }

    protected virtual void Update()
    {
        switch (_state)
        {
            case MovingEnemyState.Moving:
                Moving();
                break;

            case MovingEnemyState.Attacking:
                Attacking();
                break;
        }
    }

    protected bool IsCanAttack() => Vector3.Distance(transform.position, playerCharacterTarget.transform.position) <= attackRange;
    protected bool IsAttacking()
    {
        foreach (Spell spell in spells)
        {
            if (spell.IsAttacking)
            {
                return true;
            }
        }

        return false;
    }

    protected virtual void Moving()
    {
        if (!IsCanAttack() && _state == MovingEnemyState.Moving)
        {
            MoveToPosition(playerCharacterTarget.transform.position);
        }
        else
        {
            _state = MovingEnemyState.Attacking;
        }
    }

    protected override void Attacking()
    {
        if (IsCanAttack())
        {
            base.Attacking();
        }
        else if(!IsAttacking())
        {
            _state = MovingEnemyState.Moving;
        }
    }
}