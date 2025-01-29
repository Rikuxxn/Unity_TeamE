using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 10f;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float rotationalDamping = 0.95f;
    [SerializeField]
    private float maxRotationSpeed = 360f;
    [SerializeField]
    private float maxRotationAngle = 70f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 20f;
    [SerializeField]
    private float screenMargin = 0.1f;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float invincibilityDuration = 1f;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float terrainDamageInterval = 0.5f; // Terrain����_���[�W���󂯂�Ԋu
    private float lastTerrainDamageTime; // �Ō��Terrain�_���[�W���󂯂�����

    private bool canMoveForward = true; // �O�i�\�t���O��ǉ�
    private GameManager gameManager;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    private Rigidbody rb;
    private Camera mainCamera;
    private float currentRotationVelocity = 0f;
    private float currentRotationAngle = 0f;
    private Vector3 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public bool CanMoveForward()
    {
        return canMoveForward;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the player!");
        }
        rb.useGravity = false;
        mainCamera = Camera.main;
        currentHealth = maxHealth;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            objectWidth = renderer.bounds.extents.x;
            objectHeight = renderer.bounds.extents.y;
        }
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void LateUpdate()
    {
        UpdateScreenBounds();
        ClampToScreenBounds();
    }

    void Update()
    {
        MoveForward();
        HandleMovement();
        HandleRotationWithInertiaAndLimits();
        HandleShooting();
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                invincibilityTimer = 0f;
            }
        }
    }

    private void UpdateScreenBounds()
    {
        float distanceToCamera = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));

        screenBounds = new Vector3(
            (topRight.x - bottomLeft.x) * 0.5f,
            (topRight.y - bottomLeft.y) * 0.5f,
            0
        );
    }

    private void ClampToScreenBounds()
    {
        Vector3 viewPos = transform.position;
        Vector3 worldPosition = mainCamera.transform.position;

        float minX = worldPosition.x - screenBounds.x + objectWidth + screenMargin;
        float maxX = worldPosition.x + screenBounds.x - objectWidth - screenMargin;
        viewPos.x = Mathf.Clamp(viewPos.x, minX, maxX);

        float minY = worldPosition.y - screenBounds.y + objectHeight + screenMargin;
        float maxY = worldPosition.y + screenBounds.y - objectHeight - screenMargin;
        viewPos.y = Mathf.Clamp(viewPos.y, minY, maxY);

        transform.position = viewPos;

        if (viewPos.x == minX || viewPos.x == maxX)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
        if (viewPos.y == minY || viewPos.y == maxY)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
    private void MoveForward()
    {
        if (canMoveForward)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
        }
        else
        {
            // �O�i���~�i���E�ړ��͉\�j
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
    }

    // �X�g�b�v�|�C���g�Ƃ̐ڐG�����m
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopPoint"))
        {
            canMoveForward = false;
            // ���C���J������GameCamera�R���|�[�l���g���擾���Ē�~
            Camera.main.GetComponent<GameCamera>().StopMoving();
            Debug.Log("Player stopped at checkpoint");
        }
    }
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = horizontal * movementSpeed;
        newVelocity.y = vertical * movementSpeed;
        rb.velocity = newVelocity;
    }

    private void HandleRotationWithInertiaAndLimits()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotationInput = -1f;
        }

        currentRotationAngle = transform.rotation.eulerAngles.z;
        if (currentRotationAngle > 180)
        {
            currentRotationAngle -= 360;
        }

        bool canRotatePositive = currentRotationAngle < maxRotationAngle;
        bool canRotateNegative = currentRotationAngle > -maxRotationAngle;

        if ((!canRotatePositive && rotationInput > 0) || (!canRotateNegative && rotationInput < 0))
        {
            rotationInput = 0;
            currentRotationVelocity *= 0.8f;
        }

        currentRotationVelocity += rotationInput * rotationSpeed * Time.deltaTime;
        currentRotationVelocity = Mathf.Clamp(currentRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        if (rotationInput == 0f)
        {
            currentRotationVelocity *= rotationalDamping;

            if (Mathf.Abs(currentRotationVelocity) < 0.01f)
            {
                currentRotationVelocity = 0f;
            }
        }

        float nextAngle = currentRotationAngle + (currentRotationVelocity * Time.deltaTime);

        if (Mathf.Abs(nextAngle) > maxRotationAngle)
        {
            float adjustedVelocity = (maxRotationAngle * Mathf.Sign(nextAngle) - currentRotationAngle) / Time.deltaTime;
            currentRotationVelocity = adjustedVelocity;
        }

        transform.Rotate(Vector3.forward, currentRotationVelocity * Time.deltaTime);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(1000f);
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 shootDirection = (targetPoint - transform.position).normalized;

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = shootDirection * bulletSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        healthBar.UpdateHealth(currentHealth, maxHealth);  // ���̍s��ǉ�
        Debug.Log($"Player took {damage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.LogWarning("Player Died!");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    public void Heal(int amount)
    {
        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.UpdateHealth(currentHealth, maxHealth);  // ���̍s��ǉ�
        Debug.Log($"Player healed {currentHealth - oldHealth} HP. Current Health: {currentHealth}/{maxHealth}");
    }
    void OnTriggerStay(Collider other)
    {
        // Terrain�Ƃ̐ڐG�����m
        if (other.GetComponent<Terrain>() != null)
        {
            // ���G���Ԓ��łȂ��A���_���[�W�Ԋu�𒴂��Ă���ꍇ
            if (!isInvincible && Time.time - lastTerrainDamageTime >= terrainDamageInterval)
            {
                TakeDamage(10);
                lastTerrainDamageTime = Time.time;

                // �����̖��G���ԏ��������p
                isInvincible = true;
                invincibilityTimer = 0f;
            }
        }
    }
}