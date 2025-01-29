using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private bool canMoveForward = true;

    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if (canMoveForward)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }

    // 外部からカメラの移動を停止させるためのメソッド
    public void StopMoving()
    {
        canMoveForward = false;
    }
}