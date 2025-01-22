using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    private float maxWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.sizeDelta.x;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float currentWidth = (currentHealth / maxHealth) * maxWidth;
        rectTransform.sizeDelta = new Vector2(currentWidth, rectTransform.sizeDelta.y);
    }
}