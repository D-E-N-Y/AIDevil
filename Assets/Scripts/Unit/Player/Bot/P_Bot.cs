using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class P_Bot : Player
{
    private NavMeshAgent _agent;

    void Start()
    {
        Initialize();
    }

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
}