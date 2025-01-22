using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log($"Player hit by enemy bullet! Damage: {damage}");
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy") && !other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
