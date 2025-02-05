using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理用

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI; // ゲームオーバー用のUI
    [SerializeField]
    private GameObject gameClearUI; // ゲームクリア用のUI

    private bool isGameOver = false;
    private bool isGameClear = false;

    void Start()
    {
        // ゲームオーバー＆ゲームクリアUIを非表示
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (gameClearUI != null) gameClearUI.SetActive(false);
    }

    void Update()
    {
        // **ゲームクリア後にマウスの左クリックでタイトル画面へ**
        if (isGameClear && Input.GetMouseButtonDown(0)) // 左クリック
        {
            ReturnToTitle();
        }
    }

    // **ゲームオーバーの処理**
    public void GameOver()
    {
        if (isGameOver || isGameClear) return;

        isGameOver = true;
        Debug.Log("Game Over!");

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f; // ゲーム停止
    }

    // **ゲームクリアの処理**
    public void GameClear()
    {
        if (isGameClear || isGameOver) return;

        isGameClear = true;
        Debug.Log("Game Clear!");

        if (gameClearUI != null)
        {
            gameClearUI.SetActive(true);
        }

        Time.timeScale = 0f; // ゲーム停止
    }

    // **タイトル画面に戻る処理**
    public void ReturnToTitle()
    {
        Time.timeScale = 1f; // 時間を元に戻す
        SceneManager.LoadScene("TitleScene"); // タイトル画面に移動
    }

    // **ゲーム再開処理（リトライ）**
    public void RestartGame()
    {
        isGameOver = false;
        isGameClear = false;

        Time.timeScale = 1f; // ゲーム再開
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
