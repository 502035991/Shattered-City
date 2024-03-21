using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private Slider slider;
    private CharacterStats stats;

    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<CharacterStats>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();


        UpdateHealthBarUI();
        entity.onFlipped += FilpUI;
        stats.onHealthChanged += UpdateHealthBarUI;
    }
    private void UpdateHealthBarUI()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.GetCurrentHealthValue();
    }
    private void FilpUI() => rectTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FilpUI;
        stats.onHealthChanged -= UpdateHealthBarUI;
    }


}
