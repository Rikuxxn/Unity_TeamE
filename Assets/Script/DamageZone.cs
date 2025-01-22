using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 10;

    [SerializeField]
    private bool destroyOnContact = false;  // 接触時に消滅するかどうか

    private void OnTriggerEnter(Collider other)
    {
        // Playerタグを持つオブジェクトとの衝突を検出
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);

                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}