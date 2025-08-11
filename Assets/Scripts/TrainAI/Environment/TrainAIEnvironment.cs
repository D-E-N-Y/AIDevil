using UnityEngine;

public class TrainAIEnvironment : MonoBehaviour
{
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private Material attackingMaterial;

    [SerializeField] protected MeshRenderer floorMeshRenderer;

    [SerializeField] private BoxCollider spawnArea;

    public Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            spawnArea.transform.position.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );
    }

    public void Lose() => floorMeshRenderer.material = loseMaterial;
    public void Win() => floorMeshRenderer.material = winMaterial;
    public void Attacking() => floorMeshRenderer.material = attackingMaterial;
    public void Normal() => floorMeshRenderer.material = normalMaterial;
}