using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerBot : MonoBehaviour
{
    [SerializeField, Range(1f, 15f)] private float maxDistanceToMove;
    [SerializeField] private Sensor meleeSensor;

    private PlayerCharacter _playerCharacter;
    private NavMeshAgent _agent;

    private bool isMoving;

    void Start()
    {
        Initialize(GetComponent<PlayerCharacter>());

        MoveToPosition(GetRandomPosition());
    }

    public void Initialize(PlayerCharacter _player)
    {
        // this._playerCharacter = _player;
        // _player.Initialize();

        // _agent = GetComponent<NavMeshAgent>();
        // _agent.speed = _player.GetMoveSpeed();

        // meleeSensor.Initialize(UnitFaction.Player, 2f);
    }

    protected virtual void MoveToPosition(Vector3 _position)
    {
        if (_position == null) return;
        _agent.SetDestination(_position);
        isMoving = true;
    }

    private void Update()
    {
        //Debug.Log(meleeSensor.IsHasUnits());
        if (isMoving && !_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance &&
                (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f))
            {
                isMoving = false;
                MoveToPosition(GetRandomPosition());
            }
        }

        if (meleeSensor.IsHasUnits())
        {
            _playerCharacter.SpellController.CastRandomMeleeSpell();
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(transform.position.x - maxDistanceToMove, transform.position.x + maxDistanceToMove),
            transform.position.y,
            Random.Range(transform.position.z - maxDistanceToMove, transform.position.z + maxDistanceToMove)
        );
    }
}