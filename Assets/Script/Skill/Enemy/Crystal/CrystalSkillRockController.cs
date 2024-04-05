using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillRockController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Player player;

    private bool Controlled;
    private int dir;
    private float duration;
    private float startTime;
    private float speed =0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
    }
    private void Update()
    {
        if(Time.time < startTime + duration)
        {
            if(Physics2D.Raycast(transform.position , Vector2.right * dir ,1, 1 << LayerMask.NameToLayer("Wall")))
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(speed * dir, rb.velocity.y);
            }

            if (player != null)
            {
                Controlled = true;
                player.ControllPlyer(new Vector2(transform.position.x + dir *0.5f , transform.position.y), dir);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            if(player!= null && Controlled)
            {
                Controlled = false;
                player.CancelControl();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
            return;

        player = collision.GetComponent<Player>();
    }
    public void Init(int direction , float duration , float speed)
    {
        player = null;
        dir = direction;
        this.duration = duration;
        this.speed = speed;
        startTime = Time.time;

        if (dir != 1)
            transform.Rotate(0, 180, 0);
    }

    public void AnimtaionFinishTrigger()
    {
        Destroy(gameObject);
    }
}
