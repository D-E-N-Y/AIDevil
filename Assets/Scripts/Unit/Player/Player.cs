using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHealth
{
    public Action<IHealth> onDead { get; set; }
    public Action onChangeHP { get; set; }

    [SerializeField, Range(1, 1000)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected int moveSpeed;

    [SerializeField] private List<Spell> spells;
    private List<SpellMelee> meleeSpells;

    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;
    private Button melleAttackButton;

    protected string _originLayer;

    public virtual void Initialize()
    {
        _originLayer = "Player";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        meleeSpells = new List<SpellMelee>();
        foreach (Spell spell in spells)
        {
            spell.Initialize(_originLayer);

            if (spell is SpellMelee _meleeSpell)
            {
                if (melleAttackButton != null)
                {
                    SetMeleeSpellController(_meleeSpell);
                }

                meleeSpells.Add(_meleeSpell);
            }
        }

        _rigidbody = GetComponent<Rigidbody>();

        gameObject.SetActive(true);
    }

    public void SetControlers(FixedJoystick _joystick, Button melleAttackButton)
    {
        this._joystick = _joystick;
        this.melleAttackButton = melleAttackButton;
    }

    public void SetMeleeSpellController(SpellMelee spell)
    {
        melleAttackButton.onClick.RemoveAllListeners();
        melleAttackButton.onClick.AddListener(() => spell.Cast());

        melleAttackButton.gameObject.SetActive(true);
    }

    public void CastRandomMeleeSpell()
    {
        meleeSpells[UnityEngine.Random.Range(0, meleeSpells.Count)].Cast();
    }

    private void FixedUpdate()
    {
        if (_joystick == null) return;

        _rigidbody.velocity = new Vector3(
            _joystick.Horizontal * moveSpeed,
            _rigidbody.velocity.y,
            _joystick.Vertical * moveSpeed
        );
    }

    public virtual void TakeDamage(int value)
    {
        value = Math.Max(0, value);
        currentHP -= value;

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

    public int GetCurrentHP() => currentHP;
    public int GetMaxHP() => maxHP;
    public float GetMoveSpeed() => moveSpeed;
}
