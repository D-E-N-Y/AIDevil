using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetController(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Cast());

        button.gameObject.SetActive(true);
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
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy _enemy))
        {
            _enemy.TakeDamage(damage);
        }
    }
}