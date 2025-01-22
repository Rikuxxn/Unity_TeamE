using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f; // ’e‚Ì¶‘¶ŠÔ

    [SerializeField]
    private int damage = 10; // ’e‚Ìƒ_ƒ[ƒW—Ê

    void Start()
    {
        // ˆê’èŠÔŒã‚É’e‚ğ”j‰ó
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // “G‚É“–‚½‚Á‚½‚©Šm”F
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // “G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }
        }

        // Õ“Ë‚É’e‚ğ”j‰ó
        Destroy(gameObject);
    }
}
