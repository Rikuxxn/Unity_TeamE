using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}