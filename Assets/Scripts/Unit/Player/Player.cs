using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHealth
{
    public Action<IHealth> onDead { get; set; }
    public Action onChangeHP { get; set; }

    [SerializeField] private string _name;

    [SerializeField, Range(1, 1000)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected int moveSpeed;

    [SerializeField, Range(1, 1000)] protected int armor;

    public Action updateSpells;
    [SerializeField, Range(1, 20)] protected int countSpells;
    [SerializeField] private List<Spell> spells;
    private List<SpellMelee> meleeSpells;
    private List<SpellRange> rangeSpells;

    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;

    protected UnitFaction _unitFaction;

    public virtual void Initialize()
    {
        _unitFaction = UnitFaction.Player;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        OrderBySpells();

        _rigidbody = GetComponent<Rigidbody>();

        gameObject.SetActive(true);
    }

    private void OrderBySpells()
    {
        meleeSpells = new List<SpellMelee>();
        rangeSpells = new List<SpellRange>();
        
        foreach (Spell spell in spells)
        {
            spell.Initialize(_unitFaction);

            if (spell is SpellMelee _meleeSpell)
            {
                meleeSpells.Add(_meleeSpell);
            }
            else if (spell is SpellRange _rangeSpell)
            {
                rangeSpells.Add(_rangeSpell);
            }
        }

        updateSpells?.Invoke();
    }

    public void SetControlers(FixedJoystick _joystick)
    {
        this._joystick = _joystick;
    }

    public void CastRandomMeleeSpell()
    {
        meleeSpells[UnityEngine.Random.Range(0, meleeSpells.Count)].Cast();
    }

    private void FixedUpdate()
    {
        if (_joystick == null || _rigidbody == null) return;

        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * moveSpeed
        );
    }

    public virtual void TakeDamage(int value)
    {
        value = Math.Max(0, value);
        float reduction = armor / ((float)armor + 100f);
        float damage = (float)value * (1f - reduction);

        currentHP -= (int)damage;

        onChangeHP?.Invoke();

        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
        onDead?.Invoke(this);
    }

    public string GetName() => _name;
    public int GetCurrentHP() => currentHP;
    public int GetMaxHP() => maxHP;
    public float GetMoveSpeed() => moveSpeed;
    public int GetArmor() => armor;

    public List<SpellRange> GetSpellRanges() => rangeSpells;
    public List<SpellMelee> GetSpellMelees() => meleeSpells;
}
