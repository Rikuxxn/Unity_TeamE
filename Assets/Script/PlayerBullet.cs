using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f;
    [SerializeField]
    private int damage = 10;

    void Start()
    {
        // Bullet ���C���[��ݒ�
        gameObject.layer = LayerMask.NameToLayer("Bullet");

        // StopPoint ���C���[���擾
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        int stopPointLayer = LayerMask.NameToLayer("StopPoint");

        // StopPoint�Ƃ̃��C���[�Փ˂𖳌���
        Physics.IgnoreLayerCollision(bulletLayer, stopPointLayer, true);

        // �������Ԍ�ɒe��j��
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        // StopPoint�Ƃ̏Փ˂𖳎�
        if (tag == "StopPoint") return;

        // �G�܂��̓{�X�Ƀ_���[�W��^����
        switch (tag)
        {
            case "Enemy":
                if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(damage);
                }
                break;

            case "Boss":
                if (collision.gameObject.TryGetComponent<Boss>(out var boss))
                {
                    boss.TakeDamage(damage);
                }
                break;
        }

        // �Փˎ��ɒe��j��
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopPoint"))
        {
            // StopPoint��ʉ�
            return;
        }
    }

}