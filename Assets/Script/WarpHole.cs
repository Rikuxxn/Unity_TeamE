using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpHole : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName = "Scene2"; // 遷移先のシーン名

    [SerializeField]
    private string playerTag = "Player"; // プレイヤーのタグ

    [SerializeField]
    private float transitionDelay = 0.5f; // 遷移までの待機時間（秒）

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが触れたかチェック
        if (other.CompareTag(playerTag))
        {
            // 遷移処理を開始
            StartCoroutine(WarpToScene());
        }
    }

    private System.Collections.IEnumerator WarpToScene()
    {
        // 指定した時間だけ待機
        yield return new WaitForSeconds(transitionDelay);

        // シーンを読み込む
        SceneManager.LoadScene(targetSceneName);
    }
}