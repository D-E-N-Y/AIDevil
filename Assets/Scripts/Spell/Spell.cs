using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    protected Coroutine attacking;

    public abstract void Initialize();
    public abstract void Cast();
}