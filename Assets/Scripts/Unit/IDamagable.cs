using System;

public interface IDamagable
{
    event Action<IDamagable> OnDead;
    IHealth GetHealth();
}