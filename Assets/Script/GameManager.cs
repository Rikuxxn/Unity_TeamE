using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI; // ゲームオーバー用のUI（CanvasやPanel）

    private bool isGameOver = false;

    void Start()
    {
        // ゲームオーバーUIを非表示
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // ゲームオーバーの処理
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over!");

        // ゲームオーバーUIを表示
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 必要に応じてゲームを停止
        Time.timeScale = 0f;
    }

    // ゲーム再開処理（リトライなどのため）
    public void RestartGame()
    {
        // ゲームオーバー状態をリセット
        isGameOver = false;

        // 必要に応じてシーンをリロード
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
