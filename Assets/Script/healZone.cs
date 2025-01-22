using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 10; // 回復量

    [SerializeField]
    private bool destroyOnContact = false; // 接触時に消滅するかどうか

    private void OnTriggerEnter(Collider other)
    {
        // Playerタグを持つオブジェクトとの衝突を検出
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healAmount); // プレイヤーの回復処理を呼び出す

                if (destroyOnContact)
                {
                    Destroy(gameObject); // 接触時にヒールゾーンを消滅させる
                }
            }
        }
    }
}
