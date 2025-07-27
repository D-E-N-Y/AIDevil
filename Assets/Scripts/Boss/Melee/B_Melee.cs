using System.Collections;
using UnityEngine;

public class B_Melee : Boss
{
    [SerializeField] private MeleeBossAttack meleeBossAttack;
    private Coroutine attacking;

    public override void Initialize()
    {
        base.Initialize();

        meleeBossAttack.EndAttack();
        meleeBossAttack.isSuccessfulAttack += SuccessfulAttack;
    }

    private void Attack()
    {
        if (attacking != null)
        {
            StopCoroutine(attacking);
        }
        attacking = StartCoroutine(nameof(Attaking));
    }

    private IEnumerator Attaking()
    {
        meleeBossAttack.StartAttack();

        yield return null;

        meleeBossAttack.EndAttack();
    }

    private void SuccessfulAttack()
    {

    }
}