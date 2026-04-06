using UnityEngine;

public class CannonBall : Projectile
{
    protected override void Move()
    {
        if (!_isMove) return;

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