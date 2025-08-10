using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Boss : Agent
{
    [SerializeField, Range(1, 9999)] private float maxHP;
    private float currentHP;

    [SerializeField] protected Material winMaterial;
    [SerializeField] protected Material loseMaterial;
    [SerializeField] protected Material attackingMaterial;

    [SerializeField] protected MeshRenderer floorMeshRenderer;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    [SerializeField] private BoxCollider spawnArea;

    protected Vector3 oldPosition, newPosition;

    [SerializeField] protected Player player;

    public override void Initialize()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        damage = Mathf.Max(0, damage);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
    }

    public override void OnEpisodeBegin()
    {
        transform.position = GetRandomSpawnPosition();
        player.transform.position = GetRandomSpawnPosition();
        //base.OnEpisodeBegin();
    }

    protected Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            spawnArea.transform.position.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
    }

    // public override void CollectObservations(VectorSensor sensor)
    // {
    //     sensor.AddObservation(transform.localPosition);
    //     sensor.AddObservation(target.localPosition);
    //     //base.CollectObservations(sensor);
    // }
    // public override void OnActionReceived(ActionBuffers actions)
    // {
    //     //Debug.Log(actions.ContinuousActions[0]);
    //     float moveX = actions.ContinuousActions[0];
    //     float moveZ = actions.ContinuousActions[1];

    //     transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    //     //base.OnActionReceived(actions);
    // }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        //base.Heuristic(actionsOut);
    }

    //public virtual void SetTarget(Transform target) => this.player.transform = target;
    
    public float GetMaxHP() => maxHP;
    public float GetCurrentHP() => currentHP;
}