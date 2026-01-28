using System;

public interface IHealth
{
    event Action OnHpChanged;
    event Action OnDead;

    int CurrentHP { get; }
    int MaxHP { get; }

    void TakeDamage(float value);
    void Heal(int value);
}