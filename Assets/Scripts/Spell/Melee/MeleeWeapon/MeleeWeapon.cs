using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField, Range(1, 100)] protected int damage;
    protected string _originLayer;

    public virtual void Initialize(string originLayer)
    {
        _originLayer = originLayer + "MeleeWeapon";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);
    }

    public virtual void StartAttack()
    {
        gameObject.SetActive(true);
    }

    public virtual void FinishAttack()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth unit)
        {
            unit.TakeDamage(damage);
        }
    }
}