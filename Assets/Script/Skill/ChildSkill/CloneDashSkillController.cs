using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class CloneDashSkillController : MonoBehaviour
{
    Vector3 targetDirection;

    private SpriteRenderer sr;
    private Animator anim;

    private Tweener moveTween;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<Enemy>();
        if (target!=null && target.CanBeHurt)
        {
            anim.SetBool("CloneAttack", true);
            target.KnockBack(Vector2.zero, 0, 0.3f).Forget();
            PlayerManager.instance.player.stats.DoDamage(target.stats,2);            
        }
    }
    public void SetPosition(Transform originPosition, int facingDir)
    {
        if(facingDir != 1)
            transform.Rotate(0,180,0);

        transform.position = originPosition.position;
        targetDirection = transform.position + Vector3.right * facingDir * 10;
        MoveToTarget(facingDir);
    }
    private void MoveToTarget(int facingDir)
    {
        moveTween = transform.DOMove(targetDirection, 0.5f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                if (Physics2D.Raycast(transform.position, Vector3.right * facingDir ,0.5f, 1 << LayerMask.NameToLayer("Wall")))
                {
                    moveTween.Kill();
                    FadeFunction();
                }
            })
             .OnComplete(FadeFunction);
    }    
    //ÏûÊ§£¬ÐÞ¸ÄaÖµ
    private void FadeFunction()
    {
        sr.DOFade(0f, 1f)
             .OnComplete(() => DestroyObj());
    }
    public void DestroyObj()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

}
