using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f; // 弾の生存時間
    [SerializeField]
    private int damage = 10; // 弾のダメージ量

    void Start()
    {
        // 一定時間後に弾を破壊
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stoppointタグが付いているオブジェクトとの衝突は無視
        if (collision.gameObject.CompareTag("Stoppoint"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }

        // 敵に当たったか確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 敵にダメージを与える
            }
        }
        // ボスに当たったか確認
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // ボスにダメージを与える
            }
        }

        // 衝突時に弾を破壊
        Destroy(gameObject);
    }
}