using UnityEngine;

public interface IUnitMovement
{
    public void Initialize(UnitStats stats);
    public void Dash(float dashDistance, float dashSpeed);

    public Vector3 Direction { get; }
    public bool IsDashing { get; }
}