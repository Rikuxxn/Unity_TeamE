using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �O���ւ̈ړ����x
    [SerializeField]
    private float forwardSpeed = 10f;

    // �㉺���E�̈ړ����x
    [SerializeField]
    private float movementSpeed = 5f;

    // Rigidbody�̎Q��
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody���擾
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the player!");
        }

        // �d�͂̉e���𖳌����i��s�̂��߁j
        rb.useGravity = false;
    }

    void Update()
    {
        // �v���C���[��O���Ɉړ�
        MoveForward();

        // �㉺���E�̈ړ�����
        HandleMovement();
    }

    private void MoveForward()
    {
        // ��ɑO���iZ�������j�ֈړ�
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }

    private void HandleMovement()
    {
        // ���͂��擾
        float horizontal = Input.GetAxis("Horizontal"); // ���E�ړ� (A/D �܂��� ��/��)
        float vertical = Input.GetAxis("Vertical");     // �㉺�ړ� (W/S �܂��� ��/��)

        // ���݂̑��x���X�V
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = horizontal * movementSpeed; // ���E�ړ�
        newVelocity.y = vertical * movementSpeed;   // �㉺�ړ�
        rb.velocity = newVelocity;
    }
}
