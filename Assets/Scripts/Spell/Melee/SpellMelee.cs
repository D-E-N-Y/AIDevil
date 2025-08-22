using System;
using UnityEngine;

public abstract class SpellMelee : Spell
{
    [SerializeField] protected MeleeWeapon meleeWeapon;
    private Action _meleeWeaponHandler;

    public override void Initialize(UnitFaction unitFaction)
    {
        attacking = null;

        _unitFaction = unitFaction;

        RemoveSubsriptions();
        meleeWeapon.Initialize(_unitFaction.ToString());
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