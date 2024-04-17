using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar_UI : MonoBehaviour
{
    private RectTransform rectTransform;
    private Slider slider;

    [SerializeField] private HealthBar_UI HealthBar_UI;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
    }
}
