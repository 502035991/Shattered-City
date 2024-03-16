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
            .SetEase(Ease.Linear)// 设置移动的缓动效果，这里使用线性
             .OnComplete(FadeFunction);
    }    
    //消失，修改a值
    private void FadeFunction()
    {
        sr.DOFade(0f,1.5f)
             .OnComplete(() => Destroy(gameObject));
    }
}
