using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI; // �Q�[���I�[�o�[�p��UI�iCanvas��Panel�j

    private bool isGameOver = false;

    void Start()
    {
        // �Q�[���I�[�o�[UI���\��
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // �Q�[���I�[�o�[�̏���
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over!");

        // �Q�[���I�[�o�[UI��\��
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // �K�v�ɉ����ăQ�[�����~
        Time.timeScale = 0f;
    }

    // �Q�[���ĊJ�����i���g���C�Ȃǂ̂��߁j
    public void RestartGame()
    {
        // �Q�[���I�[�o�[��Ԃ����Z�b�g
        isGameOver = false;

        // �K�v�ɉ����ăV�[���������[�h
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
