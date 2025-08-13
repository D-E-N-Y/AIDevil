using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHealth
{
    public Action onDead;

    [SerializeField, Range(1, 1000)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected int moveSpeed;

    [SerializeField] private List<Spell> spells;

    private Rigidbody _rigidbody;
    private FixedJoystick _joystick;
    private Button melleAttackButton;

    public virtual void Initialize()
    {
        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        foreach (Spell spell in spells)
        {
            spell.Initialize();

            if (spell is SpellMelee)
            {
                ((SpellMelee)spell).SetController(melleAttackButton);
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

        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
        onDead?.Invoke();
    }

    public int GetCurrentHP() => currentHP;
    public int GetMaxHP() => maxHP;
    public float GetMoveSpeed() => moveSpeed;
}
