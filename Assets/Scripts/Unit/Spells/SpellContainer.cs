using UnityEngine;

[RequireComponent(typeof(Transform))]
public class SpellContainer : MonoBehaviour 
{
    private Transform _spellContainer;

    public void Initialize()
    {
        _spellContainer = GetComponent<Transform>();
    }

    public Spell CreateSpell(Spell spell)
    {
        return Instantiate(spell, _spellContainer);
    }

    public void DisableSpell(Spell spell)
    {
        spell.gameObject.SetActive(false);
    }
}