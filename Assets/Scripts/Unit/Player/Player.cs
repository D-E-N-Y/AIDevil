using System;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void Initialize()
    {
        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        spells.ForEach(x => x.Initialize());

        gameObject.SetActive(true);
    }

    public void SetControlers(FixedJoystick _joystick)
    {
        this._joystick = _joystick;
        _rigidbody = GetComponent<Rigidbody>();
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
