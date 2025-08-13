public interface IHealth
{
    public int GetCurrentHP();
    public int GetMaxHP();

    public void TakeDamage(int _value);
    public void Death();
}