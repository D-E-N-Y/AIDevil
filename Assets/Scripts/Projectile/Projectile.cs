using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int damage;
    [SerializeField, Range(1f, 100f)] private float moveSpeed;
    private int _originLayer;
    private LayerMask _targetLayer;
    private Vector3 _targetPosition;

    private int enemyLayer; 
    private int playerLayer;

    public bool isAvaliable { get; protected set; }

    public void Initialize(Vector3 _position)
    {
        transform.position = _position;

        enemyLayer = LayerMask.NameToLayer("Enemy"); 
        playerLayer = LayerMask.NameToLayer("Player");
    }

    public void Fire(LayerMask _targetLayer, Vector3 _targetPosition)
    {
        _originLayer = (_targetLayer == (1 << enemyLayer)) 
            ? playerLayer 
            : enemyLayer;

        this._targetLayer = _targetLayer;
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
        if (_originLayer == other.gameObject.layer) return;

        if (_targetLayer == (_targetLayer | (1 << other.gameObject.layer)) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth _unit)
        {
            _unit.TakeDamage(damage);
        }

        isAvaliable = true;
        gameObject.SetActive(false);
    }
}