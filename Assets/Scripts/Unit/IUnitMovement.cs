using UnityEngine;

public interface IUnitMovement
{
    public void Dash(float dashDistance, float dashSpeed);

    public Vector3 Direction { get; }
    public bool IsDashing { get; }
}