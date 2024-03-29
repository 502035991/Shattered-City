using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillRockController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;

    private Player player;
    private int dir;
    private Tween moveTween;
    private bool canControl;


    private float duration;
    private float startTime;
    [SerializeField] private float speed;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        canControl = true;
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void Update()
    {
        if(Time.time < startTime + duration)
        {
            if(Physics2D.Raycast(transform.position,Vector2.right * dir , 1 << LayerMask.NameToLayer("Wall")))
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(speed * dir, rb.velocity.y);
            }

            if (player != null)
            {
                player.ControllPlyer(transform.position, dir);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            canControl = false;

            if(player!= null)
            {
                player.CancelControl();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
    }
    public void Init(int direction , float duration)
    {
        player = null;
        dir = direction;
        this.duration = duration;
        startTime = Time.time;

        if (dir != 1)
            transform.Rotate(0, 180, 0);
    }

    public void AnimtaionFinishTrigger()
    {
        Destroy(gameObject);
    }
}
