using System;
using UnityEngine;

public abstract class SpellMelee : Spell
{
    protected MeleeWeapon _meleeWeapon;

    public override void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        base.Initialize(unitFaction, stats);

        _meleeWeapon = (MeleeWeapon)_weapon;
    }

    public override void Cast()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }
}