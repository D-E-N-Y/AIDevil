using System;

public interface IHealth
{
    public Action onChangeHP { get; set; }
    public Action<IHealth> onDead { get; set; }

    public int GetCurrentHP();
    public int GetMaxHP();

    public void TakeDamage(int _value);
    public void Death();
}