using UnityEngine;

public class ReturnToGameGround : MonoBehaviour
{
    [SerializeField] private Transform returnPostion;

    void OnTriggerEnter(Collider other)
    {
        if (returnPostion == null) return;

        other.transform.position = returnPostion.position;
    }
}
