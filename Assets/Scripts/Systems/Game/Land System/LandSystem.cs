using UnityEngine;
using UnityEngine.AI;

public class LandSystem : MonoBehaviour 
{
    [SerializeField] private Renderer _land;

    private Bounds _bounds;

    public void Initialize()
    {
        _bounds = _land.bounds;
    }

    public Vector3 GetValidRandomPosition()
    {
        const int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            if (NavMesh.SamplePosition(GetRandomPosition(), out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return _bounds.center;
    }

    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(_bounds.min.x, _bounds.max.x);
        float z = Random.Range(_bounds.min.z, _bounds.max.z);
        float y = _bounds.center.y;

        return new Vector3(x, y, z);
    }
}