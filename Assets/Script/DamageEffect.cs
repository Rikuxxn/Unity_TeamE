using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField]
    private Image damageOverlay;  // ��ʑS�̂𕢂��Ԃ��p�l��

    [SerializeField]
    private float maxAlpha = 0.3f;  // �G�t�F�N�g�̍ő哧���x

    [SerializeField]
    private float fadeSpeed = 2f;  // �t�F�[�h�̑��x

    private Color overlayColor;
    private float targetAlpha = 0f;

    private void Start()
    {
        // �����ݒ�
        overlayColor = damageOverlay.color;
        overlayColor.a = 0f;
        damageOverlay.color = overlayColor;
    }

    private void Update()
    {
        // ���݂̃A���t�@�l��ڕW�l�ɏ��X�ɋ߂Â���
        overlayColor.a = Mathf.Lerp(overlayColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
        damageOverlay.color = overlayColor;
    }

    // �_���[�W���󂯂��Ƃ��ɌĂяo�����\�b�h
    public void OnDamage()
    {
        targetAlpha = maxAlpha;

        // ��莞�Ԍ�ɃG�t�F�N�g������
        Invoke("ResetEffect", 0.5f);
    }

    private void ResetEffect()
    {
        targetAlpha = 0f;
    }
}