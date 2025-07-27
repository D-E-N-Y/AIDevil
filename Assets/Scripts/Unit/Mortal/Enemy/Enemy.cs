using UnityEngine;

public class Enemy : U_Mortal
{
    protected Transform target;
    public virtual void SetTarget(Transform target) => this.target = target;
}