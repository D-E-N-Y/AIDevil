using System.Collections;
using UnityEngine;

using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class B_Melee : Boss
{
    [SerializeField] protected Sensor meleeSensor;

    private Coroutine attacking;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        meleeSensor.Initialize(_unitFaction, 2f);
        meleeSensor.onEnterUnit += AttackRangeEnter;
        meleeSensor.onExitUnit += AttackRangeExit;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = environment.GetRandomSpawnPosition();
        playerTarget.transform.position = environment.GetRandomSpawnPosition();

        currentHP = maxHP;
        onChangeHP?.Invoke();

        if (playerTarget.gameObject.TryGetComponent<PlayerBot>(out PlayerBot playerBot))
        {
            playerBot.Initialize(playerTarget);
        }
        else
        {
            playerTarget.Initialize();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        sensor.AddObservation(transform.position);
        sensor.AddObservation(playerTarget.transform.position);
        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        // newPosition = transform.position;

        // if (Vector3.Distance(oldPosition, playerTarget.transform.position) < Vector3.Distance(newPosition, playerTarget.transform.position))
        // {
        //     SetReward(+0.1f);
        // }
        // else SetReward(-0.1f);

        // oldPosition = newPosition;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        //base.Heuristic(actionsOut);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-100f);
            EndEpisode();
        }
    }

    private void AttackRangeEnter()
    {

        Attack();
        environment.Attacking();
    }

    private void AttackRangeExit()
    {
        if (attacking != null)
        {
            StopCoroutine(attacking);
            attacking = null;
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
        while (meleeSensor.IsHasUnits())
        {
            meleeSpells[Random.Range(0, meleeSpells.Count)].Cast();

            yield return null;
        }
    }

    protected override void SuccessfulKill(IHealth unit)
    {
        base.SuccessfulKill(unit);
    }
}