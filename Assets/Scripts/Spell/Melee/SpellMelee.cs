using System.Collections;
using UnityEngine; 

public class SpellMelee : Spell
{
    [SerializeField] protected MeleeWeapon meleeWeapon;

    public override void Initialize(string originLayer)
    {
        _originLayer = originLayer;

        meleeWeapon.Initialize(_originLayer);
        meleeWeapon.FinishAttack();
    }

    public override void Cast()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    protected virtual IEnumerator Attacking()
    {
        yield return null;
    }
}