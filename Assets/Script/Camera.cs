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

    // �O������J�����̈ړ����~�����邽�߂̃��\�b�h
    public void StopMoving()
    {
        canMoveForward = false;
    }
}