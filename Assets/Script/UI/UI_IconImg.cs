using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_IconImg : MonoBehaviour
{
    private Image bg;
    private Slider slider;

    private void Start()
    {
        bg = transform.Find("Back"). GetComponent<Image>();
        slider = GetComponentInChildren<Slider>();

        if(slider != null )
            slider.gameObject.SetActive(false);
    }

    public void EnterCooldown()
    {
        bg.fillAmount = 1;
    }
    public void SetValue(float _cooldown)
    {
        if(bg.fillAmount > 0)
            bg.fillAmount = _cooldown;
    }
    public void SetSliderValue(float _cooldown)
    {
        if (_cooldown < 1)
        {
            slider.gameObject.SetActive(true);
            slider.value = _cooldown;
        }
        else
            slider.gameObject.SetActive(false);

        
    }
}
