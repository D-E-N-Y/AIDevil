using System.Collections;
using UnityEngine; 

[RequireComponent(typeof(SphereCollider))]
public class SpellMelee : Spell
{
    [SerializeField, Range(1, 100)] private int damage;
    private SphereCollider damageCollider;

    [SerializeField] private ParticleSystem blown;

    public override void Initialize()
    {
        damageCollider = GetComponent<SphereCollider>();
        damageCollider.enabled = false;

        blown.Stop();
    }

    public override void Cast()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    private IEnumerator Attacking()
    {
        blown.Play();

        damageCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        damageCollider.enabled = false;

        yield return new WaitForSeconds(cooldown - 0.5f);

        attacking = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsCorrentTarget(other.gameObject) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth _unit)
        {
            _unit.TakeDamage(damage);
        }
    }
}