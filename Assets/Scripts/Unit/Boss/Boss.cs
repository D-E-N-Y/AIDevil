using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using System;

public class Boss : Agent, IHealth
{
    public Action<IHealth> onDead { get; set; }
    public Action onChangeHP { get; set; }

    [SerializeField, Range(1, 9999)] private int maxHP;
    private int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    protected Vector3 oldPosition, newPosition;

    [SerializeField] protected Player playerTarger;
    [SerializeField] protected TrainAIEnvironment environment;

    public override void Initialize()
    {
        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);
    }

    public virtual void TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage);
        currentHP -= damage;

        onChangeHP?.Invoke();

        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        //base.Heuristic(actionsOut);
    }

    public void SetPlayerTarget(Player playerTarger) => this.playerTarger = playerTarger;
    
    public int GetMaxHP() => maxHP;
    public int GetCurrentHP() => currentHP;
}