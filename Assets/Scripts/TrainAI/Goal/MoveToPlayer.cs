using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToPlayer : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    [SerializeField] private BoxCollider spawnArea;

    private Vector3 oldPosition, newPosition;



    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        oldPosition = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = GetRandomSpawnPosition();
        targetTransform.position = GetRandomSpawnPosition();
        //base.OnEpisodeBegin();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            spawnArea.transform.position.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.ContinuousActions[0]);
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 2f;

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        newPosition = transform.position;

        if (Vector3.Distance(oldPosition, targetTransform.position) < Vector3.Distance(newPosition, targetTransform.position))
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
        if (other.TryGetComponent<Player>(out Player player))
        {
            SetReward(+100f);
            EndEpisode();
            floorMeshRenderer.material = winMaterial;
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            floorMeshRenderer.material = loseMaterial;
            SetReward(-100f);
            EndEpisode();
        }

    }

}
