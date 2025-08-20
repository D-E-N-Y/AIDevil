using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using System;
using System.Collections.Generic;

public class Boss : Agent, IHealth
{
    public Action<IHealth> onDead { get; set; }
    public Action onChangeHP { get; set; }

    [SerializeField, Range(1, 9999)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    protected Vector3 oldPosition, newPosition;

    [SerializeField] protected Player playerTarget;
    [SerializeField] protected TrainAIEnvironment environment;

    [SerializeField] protected List<Spell> spells;
    protected List<SpellMelee> meleeSpells;

    protected string _originLayer;

    public override void Initialize()
    {
        _originLayer = "Enemy";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        meleeSpells = new List<SpellMelee>();
        foreach (Spell spell in spells)
        {
            spell.Initialize(_originLayer);
            spell.onSuccessfulAttack += SuccessfulAttack;

            if (spell is SpellMelee _meleeSpell)
            {
                meleeSpells.Add(_meleeSpell);
            }
        }

        playerTarget.onDead += SuccessfulKill;
    }

    public virtual void TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage);
        SetReward(-0.01f);
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

    protected virtual void SuccessfulAttack()
    {
        SetReward(+40f);
    }

    protected virtual void SuccessfulKill(IHealth unit)
    {
        environment.Win();
        SetReward(+200f);
        EndEpisode();
    }

    public void SetPlayerTarget(Player playerTarget) => this.playerTarget = playerTarget;
    
    public int GetMaxHP() => maxHP;
    public int GetCurrentHP() => currentHP;
}