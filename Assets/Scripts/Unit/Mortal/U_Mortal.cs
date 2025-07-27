using UnityEngine;

public class U_Mortal : Unit
{
    [SerializeField, Range(1, 9999)] private float maxHP;
    private float currentHP;

    public override void Initialize()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        damage = Mathf.Max(0, damage);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
    }

    public float GetMaxHP() => maxHP;
    public float GetCurrentHP() => currentHP;
}