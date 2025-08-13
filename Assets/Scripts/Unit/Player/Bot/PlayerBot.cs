using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerBot : MonoBehaviour
{
    private Player _player;
    private NavMeshAgent _agent;

    void Start()
    {

    }

    public void Initialize(Player _player)
    {
        this._player = _player;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _player.GetMoveSpeed();
    }

    protected virtual void MoveToPosition(Vector3 _position)
    {
        if (_position == null) return;
        _agent.SetDestination(_position);
    }
}