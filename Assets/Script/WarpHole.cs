using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpHole : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName = "Scene2"; // �J�ڐ�̃V�[����

    [SerializeField]
    private string playerTag = "Player"; // �v���C���[�̃^�O

    [SerializeField]
    private float transitionDelay = 0.5f; // �J�ڂ܂ł̑ҋ@���ԁi�b�j

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���G�ꂽ���`�F�b�N
        if (other.CompareTag(playerTag))
        {
            // �J�ڏ������J�n
            StartCoroutine(WarpToScene());
        }
    }

    private System.Collections.IEnumerator WarpToScene()
    {
        // �w�肵�����Ԃ����ҋ@
        yield return new WaitForSeconds(transitionDelay);

        // �V�[����ǂݍ���
        SceneManager.LoadScene(targetSceneName);
    }
}