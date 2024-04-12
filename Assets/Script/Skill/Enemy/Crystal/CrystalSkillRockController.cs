using System;
using UnityEngine;

public class CrystalSkillRockController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private Player player;

    private int dir;
    private float duration;
    private float moveStartTime;
    private float speed;
    private bool isAttacking;

    [SerializeField]
    private Vector3 AttackRange;
    [SerializeField]
    private Transform checkPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void Init(int direction, float duration, float speed, Vector2 pos)
    {
        player = null;
        anim.SetBool("Skill_1", true);

        dir = direction;
        this.duration = duration;
        this.speed = speed;

        transform.position = pos;

        moveStartTime = Time.time;

        if (direction != 1)
            transform.Rotate(0, 180, 0);
    }    
    private void FixedUpdate()
    {
        PushPlayerMethod();
        ControlPlayer();
        AttackPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.transform.GetComponent<Player>();
    }
    private void PushPlayerMethod()
    {
        if (Time.time < moveStartTime + duration)
        {
            if (Physics2D.Raycast(transform.position, Vector2.right * dir, 3, 1 << LayerMask.NameToLayer("Wall")))
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(speed * dir, rb.velocity.y);
            }             
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void ControlPlayer()
    {
        if (player == null)
            return;
        
        // 检查玩家是否在道具的前面
        if (transform.position.x < player.transform.position.x && dir == 1 ||
            transform.position.x > player.transform.position.x && dir == -1)
        {
            player.SetControl();
        }
        else
        {
            player.CancelControl(); // 如果玩家在道具的后面，则取消推动玩家的状态
        }
        
    }
    private void AttackPlayer()
    {
        if(isAttacking)
        {
            Collider2D collider = Physics2D.OverlapBox(checkPos.position, AttackRange, 0f, 1 << LayerMask.NameToLayer("Player"));
            if (collider == null)
                return;
            player = collider.GetComponent<Player>();
            player.KnockBack(5, dir, 0.5f , true);            
        }
    }
    public void AnimtaionFinishTrigger()
    {
        anim.SetBool("Skill_1", false);
        Destroy(gameObject);
    }
    public void AnimationAttackStart() => isAttacking =true;
    public void AnimationAttackEnd() => isAttacking =false;
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, 3 * Vector3.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(checkPos.position, AttackRange);
    }
}
