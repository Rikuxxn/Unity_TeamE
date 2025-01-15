using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 前方への移動速度
    [SerializeField]
    private float forwardSpeed = 10f;

    // 上下左右の移動速度
    [SerializeField]
    private float movementSpeed = 5f;

    // Rigidbodyの参照
    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the player!");
        }

        // 重力の影響を無効化（飛行のため）
        rb.useGravity = false;
    }

    void Update()
    {
        // プレイヤーを前方に移動
        MoveForward();

        // 上下左右の移動処理
        HandleMovement();
    }

    private void MoveForward()
    {
        // 常に前方（Z軸方向）へ移動
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
    }

    private void HandleMovement()
    {
        // 入力を取得
        float horizontal = Input.GetAxis("Horizontal"); // 左右移動 (A/D または ←/→)
        float vertical = Input.GetAxis("Vertical");     // 上下移動 (W/S または ↑/↓)

        // 現在の速度を更新
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = horizontal * movementSpeed; // 左右移動
        newVelocity.y = vertical * movementSpeed;   // 上下移動
        rb.velocity = newVelocity;
    }
}
