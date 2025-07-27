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

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    } 

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        //base.OnEpisodeBegin();
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

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

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
            SetReward(+1f);
            EndEpisode();
            floorMeshRenderer.material = winMaterial;
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            floorMeshRenderer.material = loseMaterial;
            SetReward(-1f);
            EndEpisode();
        }

    }

}
