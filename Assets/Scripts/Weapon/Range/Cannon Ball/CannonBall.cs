using UnityEngine;

public class CannonBall : Projectile
{
    private float _timeToLive = 5f;
    private float _timeAlive = 0f;

    protected override void Move()
    {
        if (_targetPosition == Vector3.zero) return;

        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;

        _timeAlive += Time.fixedDeltaTime;
        if (_timeAlive >= _timeToLive)
        {
            _timeAlive = 0f;
            
            _targetPosition = Vector3.zero;
            mesh.gameObject.SetActive(false);

            isAvaliable = true;
            gameObject.SetActive(false);
        }
    }
}