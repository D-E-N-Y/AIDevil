using System;
using UnityEngine;

public class MeleeBossAttack : MonoBehaviour
{
    public Action isSuccessfulAttack;

    [SerializeField, Range(1, 100)] private int damage;
    public bool isCanAttack { get; private set; } = true;


    public void StartAttack()
    {
        gameObject.SetActive(true);
        isCanAttack = false;
    }

    public void EndAttack()
    {
        gameObject.SetActive(false);
        isCanAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(damage);
            isSuccessfulAttack?.Invoke();
        }
    }
}