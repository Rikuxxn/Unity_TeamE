using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 500; // �{�X�̗̑�
    private int currentHealth;

    [SerializeField]
    private GameObject bulletPrefab; // �ʏ�e
    [SerializeField]
    private GameObject specialBulletPrefab; // ����e
    [SerializeField]
    private float shootingInterval = 2f; // �ʏ�U���̊Ԋu
    [SerializeField]
    private float specialShootingInterval = 5f; // ����U���̊Ԋu
    [SerializeField]
    private float bulletSpeed = 20f; // �ʏ�e�̑��x
    [SerializeField]
    private float minSpecialBulletSpeed = 10f; // ����e�̍ŏ����x
    [SerializeField]
    private float maxSpecialBulletSpeed = 20f; // ����e�̍ő呬�x
    [SerializeField]
    private float detectionRange = 50f; // �v���C���[�����m����͈�
    [SerializeField]
    private Transform shootPoint; // �ʏ�e�̔��˃|�C���g
    [SerializeField]
    private Transform backShootPoint; // �w�ʒe�̔��˃|�C���g

    [SerializeField]
    private float moveSpeed = 5f; // ���E�ړ����x
    [SerializeField]
    private float moveRange = 10f; // ���E�ړ��͈�

    private Transform playerTransform;
    private float shootingTimer;
    private float specialShootingTimer;

    private Vector3 startPosition; // �{�X�̏����ʒu
    private bool movingRight = true; // �ړ������̃t���O

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

        startPosition = transform.position; // �����ʒu���L�^
    }

    void Update()
    {
        if (playerTransform == null) return;

        // ���E�ړ�����
        MoveSideToSide();

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // �v���C���[���͈͓��ɂ���ꍇ�̒ʏ�U��
        if (distanceToPlayer <= detectionRange)
        {
            transform.LookAt(playerTransform); // �v���C���[�̕���������

            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                Shoot();
                shootingTimer = shootingInterval;
            }
        }

        // ����U��
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

        Debug.Log("Attempting to shoot special bullets..."); // �f�o�b�O���O��ǉ�

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
        Debug.Log("Player bullet hit Boss!"); // �ǉ�
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}