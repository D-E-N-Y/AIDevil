using UnityEngine;
using Unity.MLAgents;

using Unity.MLAgents.Actuators;
using System;
using System.Collections.Generic;

public class Boss : Agent, IUnit
{
    [SerializeField] protected string _name;
    [SerializeField] protected UnitStats _stats;

    protected UnitHealth _health;
    public event Action<IUnit> OnDead;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    protected Vector3 oldPosition, newPosition;

    [SerializeField] protected PlayerCharacter playerCharacterTarget;
    [SerializeField] protected TrainAIEnvironment environment;

    [SerializeField] protected List<Spell> spells;
    protected List<SpellMelee> meleeSpells;

    protected UnitFaction _unitFaction;

    public override void Initialize()
    {
        _unitFaction = UnitFaction.Enemy;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _health = new UnitHealth(_stats);
        _health.OnDead += Death;

        ui_unitHPIndicator.Initialize(_health);

        meleeSpells = new List<SpellMelee>();
        foreach (Spell spell in spells)
        {
            spell.Initialize(_unitFaction, _stats);
            spell.onSuccessfulAttack += SuccessfulAttack;

            if (spell is SpellMelee _meleeSpell)
            {
                meleeSpells.Add(_meleeSpell);
            }
        }

        playerCharacterTarget.OnDead += SuccessfulKill;
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

    protected virtual void SuccessfulKill(IUnit unit)
    {
        environment.Win();
        SetReward(+200f);
        EndEpisode();
    }

    public void SetPlayerTarget(PlayerCharacter playerTarget) => this.playerCharacterTarget = playerTarget;
    
    public string GetName() => _name;
    public UnitStats GetStats() => _stats;
    public UnitHealth GetHealth() => _health;
}