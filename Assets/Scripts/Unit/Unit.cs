using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string _name { get; protected set; }

    public abstract void Initialize();
}