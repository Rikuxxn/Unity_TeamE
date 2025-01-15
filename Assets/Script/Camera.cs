using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // �J�����̈ړ����x
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        // �J������O���iZ�������j�Ɉ�葬�x�ňړ�
        MoveForward();
    }

    private void MoveForward()
    {
        // Z�������Ɉ�葬�x�ňړ�
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
