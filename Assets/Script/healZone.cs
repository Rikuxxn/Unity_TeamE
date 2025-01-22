using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 10; // �񕜗�

    [SerializeField]
    private bool destroyOnContact = false; // �ڐG���ɏ��ł��邩�ǂ���

    private void OnTriggerEnter(Collider other)
    {
        // Player�^�O�����I�u�W�F�N�g�Ƃ̏Փ˂����o
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healAmount); // �v���C���[�̉񕜏������Ăяo��

                if (destroyOnContact)
                {
                    Destroy(gameObject); // �ڐG���Ƀq�[���]�[�������ł�����
                }
            }
        }
    }
}
