using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopSkillController : MonoBehaviour
{
    public float maxSize;
    public float growSpeed;
    public bool canGrow;

    public void Init()
    {
        gameObject.SetActive(true);
        transform.DOScale(maxSize, growSpeed);
    }

    public void CancelSkill()
    {
        transform.DOScale(1, growSpeed).OnComplete( ()=> Destroy(gameObject));
    }
}
