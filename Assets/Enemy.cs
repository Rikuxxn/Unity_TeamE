using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 startPoint;  // 移動開始位置
    public Vector3 endPoint;    // 移動終了位置
    public float moveSpeed = 3f;  // 移動スピード
    private bool movingToEnd = true;  // どの方向に移動するかのフラグ

    void Start()
    {
        // 初期位置をstartPointに設定
        transform.position = startPoint;
    }

    void Update()
    {
        // 移動方向に応じて位置を更新
        if (movingToEnd)
        {
            // endPointに向かって移動
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);

            // 目標位置に到達したら反対方向に移動
            if (transform.position == endPoint)
            {
                movingToEnd = false;
            }
        }
        else
        {
            // startPointに向かって移動
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);

            // 目標位置に到達したら反対方向に移動
            if (transform.position == startPoint)
            {
                movingToEnd = true;
            }
        }
    }
}
