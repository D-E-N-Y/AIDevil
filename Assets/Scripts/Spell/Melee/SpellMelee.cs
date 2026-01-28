using System;
using UnityEngine;

public abstract class SpellMelee : Spell
{
    [SerializeField] protected MeleeWeapon meleeWeapon;
    private Action _meleeWeaponHandler;

    public override void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        base.Initialize(unitFaction, stats);

        RemoveSubsriptions();
        meleeWeapon.Initialize(_unitFaction.ToString(), rangeAttack);
        meleeWeapon.FinishAttack();
        SetSubsriptions();
    }

    public override void Cast()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }
    
    protected override void SetSubsriptions()
    {
        _meleeWeaponHandler = () => onSuccessfulAttack?.Invoke();
        meleeWeapon.onSuccessfulAttack += _meleeWeaponHandler;
    }

    protected override void RemoveSubsriptions()
    {
        if (_meleeWeaponHandler != null)
        {
            meleeWeapon.onSuccessfulAttack -= _meleeWeaponHandler;
        }
    }
}