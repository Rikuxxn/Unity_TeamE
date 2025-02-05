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
        // Bullet レイヤーを設定
        gameObject.layer = LayerMask.NameToLayer("Bullet");

        // StopPoint レイヤーを取得
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        int stopPointLayer = LayerMask.NameToLayer("StopPoint");

        // StopPointとのレイヤー衝突を無効化
        Physics.IgnoreLayerCollision(bulletLayer, stopPointLayer, true);

        // 生存時間後に弾を破壊
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        // StopPointとの衝突を無視
        if (tag == "StopPoint") return;

        // 敵またはボスにダメージを与える
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

        // 衝突時に弾を破壊
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopPoint"))
        {
            // StopPointを通過
            return;
        }
    }

}