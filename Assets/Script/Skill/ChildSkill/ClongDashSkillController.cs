using UnityEngine;
using DG.Tweening;

public class ClongDashSkillController : MonoBehaviour
{
    Vector3 targetDirection;

    private SpriteRenderer sr;
    private Animator anim;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            anim.SetBool("ClongAttack", true);
        }
    }
    public void SetPosition(Transform originPosition, int facingDir)
    {
        if(facingDir != 1)
            transform.Rotate(0,180,0);

        transform.position = originPosition.position;
        targetDirection = transform.position + Vector3.right * facingDir * 10;
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        transform.DOMove(targetDirection, 0.5f)
            .SetEase(Ease.Linear)// �����ƶ��Ļ���Ч��������ʹ������
             .OnComplete(FadeFunction);
    }    
    //��ʧ���޸�aֵ
    private void FadeFunction()
    {
        sr.DOFade(0f,1.5f)
             .OnComplete(() => Destroy(gameObject));
    }
}
