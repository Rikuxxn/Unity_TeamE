using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // 弾の速度
    [SerializeField] private float riseDuration = 1f; // 上昇時間
    [SerializeField] private float pauseDuration = 2f; // 静止時間
    [SerializeField] private float chaseDuration = 8f; // プレイヤー追尾時間

    private Vector3 initialDirection; // 最初の方向
    private Transform target; // プレイヤー
    private bool isChasingPlayer = true; // プレイヤー追尾フラグ
    private Vector3 finalDirection; // 最終的に進む方向

    public void Initialize(Vector3 direction, Transform playerTransform, float speed)
    {
        initialDirection = direction.normalized; // 初期方向を正規化
        target = playerTransform; // プレイヤーの参照
        this.speed = speed; // 弾速を設定

        // 最初の向きを設定
        transform.rotation = Quaternion.LookRotation(initialDirection);

        // 動作を管理するコルーチンを開始
        StartCoroutine(HandleMovement());
    }


    private IEnumerator HandleMovement()
    {
        // 最初に上昇して静止
        Vector3 upDirection = Vector3.up;
        float timer = riseDuration;

        while (timer > 0)
        {
            transform.position += upDirection * speed * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(pauseDuration); // 静止時間

        // プレイヤー追尾開始
        StartCoroutine(ChasePlayer());
    }

    private IEnumerator ChasePlayer()
    {
        float remainingChaseTime = chaseDuration;

        while (remainingChaseTime > 0 && target != null)
        {
            // プレイヤーの方向を計算
            Vector3 directionToPlayer = (target.position - transform.position).normalized;

            // 弾の向きを更新
            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // プレイヤー方向に移動
            transform.position += directionToPlayer * speed * Time.deltaTime;

            remainingChaseTime -= Time.deltaTime;
            yield return null;
        }

        // 追尾終了後の進行方向を設定
        isChasingPlayer = false;
        finalDirection = transform.forward; // 現在の向きを保持
    }

    void Update()
    {
        if (!isChasingPlayer)
        {
            // 追尾終了後は直進
            transform.position += finalDirection * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 何かに当たったら削除
        Destroy(gameObject);
    }
}
