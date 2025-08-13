using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int damage;
    [SerializeField, Range(1f, 100f)] private float moveSpeed;
    private Vector3 _targetPosition;

    public bool isAvaliable { get; protected set; }

    public void Initialize(Vector3 _position)
    {
        transform.position = _position;
    }

    public void Fire(Vector3 _targetPosition)
    {
        this._targetPosition = _targetPosition;
        transform.LookAt(_targetPosition);

        isAvaliable = false;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (_targetPosition == null) return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy _enemy))
        {
            _enemy.TakeDamage(damage);
        }

        isAvaliable = true;
        gameObject.SetActive(false);
    }
}