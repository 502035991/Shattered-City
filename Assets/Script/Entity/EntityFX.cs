using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Material hitMat;
    private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    public async UniTask FlashFX()
    {
        sr.material = hitMat;
        await UniTask.Delay(200);
        sr.material = originalMat;
        sr.color = Color.white;
    }
    public void RedColotrBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    public void CancleBlink() => sr.color = Color.white;

}
