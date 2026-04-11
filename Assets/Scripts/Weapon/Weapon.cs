using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action onSuccessfulAttack;

    [Header("Main")]
    [SerializeField, Range(1, 100)] protected int damage;
    public int Damage => damage;

    public bool isCanAttack { get; protected set; }
    public bool isAvaliable { get; protected set; }

    protected string _originLayer;
    protected LayerMask _interactLayers { private set; get; }

    protected abstract string WeaponType { get; }

    protected float _damageModifier;
    protected float _criticalDamageChance;
    protected float _criticalDamageModifier;
    protected float _areaModifier;

    public virtual void Initialize(UnitFaction unitFaction)
    {
        _originLayer = unitFaction.ToString() + WeaponType;
        _interactLayers = GetInteractingLayers(_originLayer);

        gameObject.layer = LayerMask.NameToLayer(_originLayer);
        gameObject.SetActive(false);
    }

    private LayerMask GetInteractingLayers(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        LayerMask mask = 0;

        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(layer, i))
            {
                mask |= 1 << i;
            }
        }

        return mask;
    }

    public virtual void SetParameters(float damageModifier = 1f, float criticalDamageChance = 0f, float criticalDamageModifier = 1f, float areaModifier = 1f)
    {
        _damageModifier = damageModifier;
        _criticalDamageChance = criticalDamageChance;
        _criticalDamageModifier = criticalDamageModifier;
        _areaModifier = areaModifier;

        transform.localScale = new Vector3(1f * areaModifier, 1f * areaModifier, 1f * areaModifier);
    }

    public abstract void PrepareAttack(Transform fireTransfrom, Vector3 target);

    public abstract void StartAttack();
    public abstract void FinishAttack();

    protected bool IsCriticalHit()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);
        return roll < _criticalDamageChance;
    }

    protected void ApplyDamage(IHealth targetUnit)
    {
        int finalDamage = Mathf.RoundToInt(damage * _damageModifier);

        if (IsCriticalHit())
        {
            finalDamage = finalDamage + Mathf.RoundToInt(finalDamage * _criticalDamageModifier);
        }
        
        targetUnit.TakeDamage(finalDamage);
        onSuccessfulAttack?.Invoke();
    }
}