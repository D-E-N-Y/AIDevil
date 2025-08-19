using System;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Action onSuccessfulAttack;

    protected string _originLayer;
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    protected Coroutine attacking;

    public abstract void Initialize(string originalLayer);
    public abstract void Cast();
}