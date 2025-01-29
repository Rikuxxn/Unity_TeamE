using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f; // �e�̐�������
    [SerializeField]
    private int damage = 10; // �e�̃_���[�W��

    void Start()
    {
        // ��莞�Ԍ�ɒe��j��
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stoppoint�^�O���t���Ă���I�u�W�F�N�g�Ƃ̏Փ˂͖���
        if (collision.gameObject.CompareTag("Stoppoint"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }

        // �G�ɓ����������m�F
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // �G�Ƀ_���[�W��^����
            }
        }
        // �{�X�ɓ����������m�F
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // �{�X�Ƀ_���[�W��^����
            }
        }

        // �Փˎ��ɒe��j��
        Destroy(gameObject);
    }
}