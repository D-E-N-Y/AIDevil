using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    protected Coroutine attacking;

    public abstract void Initialize();
    public abstract void Cast();

    protected bool IsCorrentTarget(GameObject gameObject)
    {
        return targetLayer == (targetLayer | (1 << gameObject.layer));
    }
}