using System.Collections;
using UnityEngine; 

public class SpellMelee : Spell
{
    [SerializeField] protected MeleeWeapon meleeWeapon;

    public override void Initialize(string originLayer)
    {
        attacking = null;

        _originLayer = originLayer;

        meleeWeapon.Initialize(_originLayer);
        meleeWeapon.FinishAttack();

        meleeWeapon.onSuccessfulAttack += () => onSuccessfulAttack?.Invoke();
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