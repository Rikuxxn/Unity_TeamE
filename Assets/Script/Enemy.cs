using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;
    private int currentHealth;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shootingInterval = 2f;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private float detectionRange = 30f;
    [SerializeField]
    private Transform shootPoint;

    private Transform playerTransform;
    private float shootingTimer;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        shootingTimer = Random.Range(0f, shootingInterval);

        if (shootPoint == null)
        {
            shootPoint = transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            transform.LookAt(playerTransform);

            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                Shoot();
                shootingTimer = shootingInterval;
            }
        }
    }

    private void Shoot()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = (playerTransform.position - shootPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = directionToPlayer * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }

    public void TakeDamage(int damage)
    {
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    // ’Ç‰Á: ‰æ–ÊŠO‚Éo‚½‚çíœ
    private void OnBecameInvisible()
    {
        Debug.Log("Enemy went off-screen and was destroyed.");
        Destroy(gameObject);
    }
}
