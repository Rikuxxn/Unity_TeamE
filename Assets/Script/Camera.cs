using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // カメラの移動速度
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        // カメラを前方（Z軸方向）に一定速度で移動
        MoveForward();
    }

    private void MoveForward()
    {
        // Z軸方向に一定速度で移動
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
