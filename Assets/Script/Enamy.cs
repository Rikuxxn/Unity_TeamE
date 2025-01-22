using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50; // Å‘å‘Ì—Í

    private int currentHealth; // Œ»İ‚Ì‘Ì—Í

    void Start()
    {
        // ‰Šú‘Ì—Í‚ğİ’è
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // “G‚ğÁ–Å‚³‚¹‚é
    }
}
