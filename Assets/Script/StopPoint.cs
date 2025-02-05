using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // ÉvÉåÉCÉÑÅ[ÇÃí‚é~èàóù
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.StopMoving();
            }
            GameCamera gameCamera = Camera.main.GetComponent<GameCamera>();
            if (gameCamera != null)
            {
                gameCamera.StopMoving();
            }
            Destroy(gameObject);
        }

    }
}