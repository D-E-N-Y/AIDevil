using UnityEngine;

public class MW_Blown : MeleeWeapon
{
    [SerializeField] ParticleSystem blownEffect;

    public override void Initialize(string originLayer)
    {
        base.Initialize(originLayer);
        blownEffect.Stop();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        blownEffect.Play();
    }
}