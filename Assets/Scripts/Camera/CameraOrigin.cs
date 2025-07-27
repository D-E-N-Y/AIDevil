using UnityEngine;

public class CameraOrigin : MonoBehaviour
{
    // [SerializeField] private Camera _camera;
    [SerializeField, Range(1.0f, 10.0f)] private float _speed;

    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, _speed * Time.deltaTime);
    }
}
