using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��p

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI; // �Q�[���I�[�o�[�p��UI
    [SerializeField]
    private GameObject gameClearUI; // �Q�[���N���A�p��UI

    private bool isGameOver = false;
    private bool isGameClear = false;

    void Start()
    {
        // �Q�[���I�[�o�[���Q�[���N���AUI���\��
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (gameClearUI != null) gameClearUI.SetActive(false);
    }

    void Update()
    {
        // **�Q�[���N���A��Ƀ}�E�X�̍��N���b�N�Ń^�C�g����ʂ�**
        if (isGameClear && Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            ReturnToTitle();
        }
    }

    // **�Q�[���I�[�o�[�̏���**
    public void GameOver()
    {
        if (isGameOver || isGameClear) return;

        isGameOver = true;
        Debug.Log("Game Over!");

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f; // �Q�[����~
    }

    // **�Q�[���N���A�̏���**
    public void GameClear()
    {
        if (isGameClear || isGameOver) return;

        isGameClear = true;
        Debug.Log("Game Clear!");

        if (gameClearUI != null)
        {
            gameClearUI.SetActive(true);
        }

        Time.timeScale = 0f; // �Q�[����~
    }

    // **�^�C�g����ʂɖ߂鏈��**
    public void ReturnToTitle()
    {
        Time.timeScale = 1f; // ���Ԃ����ɖ߂�
        SceneManager.LoadScene("TitleScene"); // �^�C�g����ʂɈړ�
    }

    // **�Q�[���ĊJ�����i���g���C�j**
    public void RestartGame()
    {
        isGameOver = false;
        isGameClear = false;

        Time.timeScale = 1f; // �Q�[���ĊJ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
