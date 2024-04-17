using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Material hitMat;
    [SerializeField] private GameObject hitEffectObj;
    [SerializeField] private Transform hitEffectPos;
    private Material originalMat;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    public async void FlashFX()
    {
        sr.color = new Color(1f, 0.676f, 0.676f, 1f);

        // ��������Ƕ�
        float randomAngle = Random.Range(0f, 360f);

        // ���Ƕ�ת��Ϊ����
        float radians = randomAngle * Mathf.Deg2Rad;

        // ���ݼ�����������λ��
        float randomRadius = Random.Range(0f, 1f);
        Vector3 effectPosition = hitEffectPos.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * randomRadius;

        var effectObj = Instantiate(hitEffectObj, effectPosition, Quaternion.identity, hitEffectPos);

        await UniTask.Delay(200);
        sr.color = Color.white;
        Destroy(effectObj);
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
