using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 10;

    [SerializeField]
    private bool destroyOnContact = false;  // �ڐG���ɏ��ł��邩�ǂ���

    private void OnTriggerEnter(Collider other)
    {
        // Player�^�O�����I�u�W�F�N�g�Ƃ̏Փ˂����o
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);

                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}