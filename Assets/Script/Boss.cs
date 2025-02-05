using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 500; // ボスの体力
    private int currentHealth;

    [SerializeField]
    private GameObject bulletPrefab; // 通常弾
    [SerializeField]
    private GameObject specialBulletPrefab; // 特殊弾
    [SerializeField]
    private float shootingInterval = 2f; // 通常攻撃の間隔
    [SerializeField]
    private float specialShootingInterval = 5f; // 特殊攻撃の間隔
    [SerializeField]
    private float bulletSpeed = 20f; // 通常弾の速度
    [SerializeField]
    private float minSpecialBulletSpeed = 10f; // 特殊弾の最小速度
    [SerializeField]
    private float maxSpecialBulletSpeed = 20f; // 特殊弾の最大速度
    [SerializeField]
    private float detectionRange = 50f; // プレイヤーを検知する範囲
    [SerializeField]
    private Transform shootPoint; // 通常弾の発射ポイント
    [SerializeField]
    private Transform backShootPoint; // 背面弾の発射ポイント

    [SerializeField]
    private float moveSpeed = 5f; // 左右移動速度
    [SerializeField]
    private float moveRange = 10f; // 左右移動範囲

    private Transform playerTransform;
    private float shootingTimer;
    private float specialShootingTimer;

    private Vector3 startPosition; // ボスの初期位置
    private bool movingRight = true; // 移動方向のフラグ

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
        specialShootingTimer = specialShootingInterval;

        if (shootPoint == null)
        {
            shootPoint = transform;
        }

        if (backShootPoint == null)
        {
            Debug.LogError("Back ShootPoint is not assigned!");
        }

        startPosition = transform.position; // 初期位置を記録
    }

    void Update()
    {
        if (playerTransform == null) return;

        // 左右移動処理
        MoveSideToSide();

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // プレイヤーが範囲内にいる場合の通常攻撃
        if (distanceToPlayer <= detectionRange)
        {
            transform.LookAt(playerTransform); // プレイヤーの方向を向く

            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                Shoot();
                shootingTimer = shootingInterval;
            }
        }

        // 特殊攻撃
        specialShootingTimer -= Time.deltaTime;
        if (specialShootingTimer <= 0)
        {
            SpecialShoot();
            specialShootingTimer = specialShootingInterval;
        }
    }

    private void MoveSideToSide()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
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

    private void SpecialShoot()
    {
        if (playerTransform == null || backShootPoint == null)
        {
            Debug.LogWarning("SpecialShoot: playerTransform or backShootPoint is null!");
            return;
        }

        Debug.Log("Attempting to shoot special bullets..."); // デバッグログを追加

        for (int i = -1; i <= 1; i++)
        {
            Vector3 direction = Quaternion.Euler(0, i * 30, 0) * -transform.forward;
            GameObject bullet = Instantiate(specialBulletPrefab, backShootPoint.position, Quaternion.LookRotation(direction));

            if (bullet == null)
            {
                Debug.LogError("Failed to instantiate special bullet!");
                continue;
            }

            SpecialBullet specialBullet = bullet.GetComponent<SpecialBullet>();
            if (specialBullet != null)
            {
                float randomSpeed = Random.Range(minSpecialBulletSpeed, maxSpecialBulletSpeed);
                specialBullet.Initialize(direction, playerTransform, randomSpeed);
                Debug.Log($"Special bullet initialized with speed: {randomSpeed}");
            }
            else
            {
                Debug.LogError("SpecialBullet component not found on instantiated bullet!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player bullet hit Boss!"); // 追加
        currentHealth -= damage;
        Debug.Log($"Boss took {damage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Boss died!");

        // GameManager を探してゲームクリア処理を実行
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameClear();
        }
        else
        {
            Debug.LogError("GameManager がシーン内に見つかりません！");
        }

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}