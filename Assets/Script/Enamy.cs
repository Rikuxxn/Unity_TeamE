using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50; // �ő�̗�

    private int currentHealth; // ���݂̗̑�

    void Start()
    {
        // �����̗͂�ݒ�
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // �_���[�W���󂯂鏈��
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
        Destroy(gameObject); // �G�����ł�����
    }
}
