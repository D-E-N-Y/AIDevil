using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class B_Melee : Boss
{
    [SerializeField] private MeleeBossAttack meleeBossAttack;
    private Coroutine attacking;

    private bool isTouchingPlayer;
    private bool fuckingNotTouchingPlayer;

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
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode Begin");
        transform.position = GetRandomSpawnPosition();
        player.transform.position = GetRandomSpawnPosition();
        player.Initialize();
        if (isTouchingPlayer) {
            Debug.Log("NOT FUCKING TOUCHING PLAYER (EPISODE BEGIN)");
            isTouchingPlayer = false;
        } 
        //base.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.position);
        sensor.AddObservation(player.transform.position);
        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("isTouchingPlayer:" + isTouchingPlayer);
        if (!fuckingNotTouchingPlayer)
        {
            if (isTouchingPlayer) return;
        }
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

        //base.OnActionReceived(actions);
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
        if (other.TryGetComponent<Player>(out Player _player))
        {
            if (_player == player)
            {
                Attack();
                fuckingNotTouchingPlayer = false;
                isTouchingPlayer = true;
            }
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            floorMeshRenderer.material = loseMaterial;
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
        floorMeshRenderer.material = attackingMaterial;
        SetReward(+0.5f);
        if (player.GetCurrentHP() <= 0)
            {
                floorMeshRenderer.material = winMaterial;
            if (isTouchingPlayer)
            {
                Debug.Log("NOT FUCKING TOUCHING PLAYER");
                    isTouchingPlayer = false;
                    fuckingNotTouchingPlayer = true;
                }    
                SetReward(+100f);
                Debug.Log("Episode End");
                EndEpisode();
            }

    }
}