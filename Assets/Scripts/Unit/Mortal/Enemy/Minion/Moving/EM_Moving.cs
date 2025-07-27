using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EM_Moving : E_Minion
{
    [SerializeField, Range(1f, 100f)] private float moveSpeed;
    protected NavMeshAgent navMeshAgent;

    public override void Initialize()
    {
        base.Initialize();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }

    protected virtual void Move()
    {
        if (target == null) return;

        navMeshAgent.SetDestination(target.position);
    }

    protected virtual void Update()
    {
        Move();
    }
}