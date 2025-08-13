using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class E_Moving : Enemy
{
    protected NavMeshAgent _agent;

    public override void Initialize()
    {
        base.Initialize();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = moveSpeed;
    }

    protected virtual void MoveToPosition(Vector3 _position)
    {
        if (_position == null) return;
        _agent.SetDestination(_position);
    }

    protected virtual void Update()
    {
        MoveToPosition(playerTarget.transform.position);
    }
}