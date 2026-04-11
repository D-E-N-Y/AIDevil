using System;
using UnityEngine;

public interface IDamagable
{
    event Action<IDamagable> OnDead;
    
    IHealth GetHealth();
    Transform GetTransform();
}