using System.Collections;
using UnityEngine;

using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class B_Melee : Boss
{
    [SerializeField] private MeleeBossAttack meleeBossAttack;
    private Coroutine attacking;

    private bool isTouchingPlayer;

    private bool canDetectTouch;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        isTouchingPlayer = false;
        base.Initialize();

        meleeBossAttack.EndAttack();
        meleeBossAttack.isSuccessfulAttack += SuccessfulAttack;

        player.onDead += SuccessfulKill;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = environment.GetRandomSpawnPosition();
        canDetectTouch = false;
        isTouchingPlayer = false;
        StartCoroutine(EnableTouchDetectionAfterDelay());
        //base.OnEpisodeBegin();
        player.transform.position = environment.GetRandomSpawnPosition();
        player.Initialize();
    }

    private IEnumerator EnableTouchDetectionAfterDelay()
    {
        yield return null;
        canDetectTouch = true;
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        if (!isTouchingPlayer)
        {
            sensor.AddObservation(transform.position);
            sensor.AddObservation(player.transform.position);
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }

        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        newPosition = transform.position;

        if (Vector3.Distance(oldPosition, player.transform.position) < Vector3.Distance(newPosition, player.transform.position))
        {
            SetReward(+0.1f);
        }
        else SetReward(-0.1f);

        oldPosition = newPosition;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        //base.Heuristic(actionsOut);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDetectTouch) return;
        if (other.TryGetComponent<Player>(out Player _player))
        {
            if (_player == player)
            {
                Attack();
                environment.Attacking();
                isTouchingPlayer = true;
            }
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            environment.Lose();
            SetReward(-100f);
            EndEpisode();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            isTouchingPlayer = false;
            if (attacking != null)
            {
                meleeBossAttack.EndAttack();
                StopCoroutine(attacking);
            }
        }
    }

    private void Attack()
    {
        if (attacking != null)
        {
            StopCoroutine(attacking);
        }
        attacking = StartCoroutine(nameof(Attaking));
    }

    private IEnumerator Attaking()
    {
        while (true)
        {
            meleeBossAttack.StartAttack();

            yield return new WaitForSeconds(0.05f);

            meleeBossAttack.EndAttack();
        }
    }

    private void SuccessfulAttack()
    {
        SetReward(+0.5f);
    }

    private void SuccessfulKill()
    {
        environment.Win();
        isTouchingPlayer = false;
        SetReward(+100f);
        EndEpisode();
    }
}