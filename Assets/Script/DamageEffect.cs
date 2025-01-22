using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField]
    private Image damageOverlay;  // 画面全体を覆う赤いパネル

    [SerializeField]
    private float maxAlpha = 0.3f;  // エフェクトの最大透明度

    [SerializeField]
    private float fadeSpeed = 2f;  // フェードの速度

    private Color overlayColor;
    private float targetAlpha = 0f;

    private void Start()
    {
        // 初期設定
        overlayColor = damageOverlay.color;
        overlayColor.a = 0f;
        damageOverlay.color = overlayColor;
    }

    private void Update()
    {
        // 現在のアルファ値を目標値に徐々に近づける
        overlayColor.a = Mathf.Lerp(overlayColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
        damageOverlay.color = overlayColor;
    }

    // ダメージを受けたときに呼び出すメソッド
    public void OnDamage()
    {
        targetAlpha = maxAlpha;

        // 一定時間後にエフェクトを消す
        Invoke("ResetEffect", 0.5f);
    }

    private void ResetEffect()
    {
        targetAlpha = 0f;
    }
}