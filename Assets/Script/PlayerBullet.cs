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
        // �G�ɓ����������m�F
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // �G�Ƀ_���[�W��^����
            }
        }

        // �Փˎ��ɒe��j��
        Destroy(gameObject);
    }
}
