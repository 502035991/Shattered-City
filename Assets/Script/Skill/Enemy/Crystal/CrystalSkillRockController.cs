using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillRockController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    private Player player;
    private int dir;
    private Tween moveTween;
    private bool canControl;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        canControl = true;
    }
    private void Update()
    {
        if (!canControl)
            return;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 2, 1 << LayerMask.NameToLayer("Player"));
        if(collider != null )
        {
            player = collider.gameObject.GetComponent<Player>();
            player.ControllPlyer(transform.position , dir);
        }
    }
    public void Init(int direction)
    {
        player = null;
        dir = direction;

        if (direction != 1)
            transform.Rotate(0, 180, 0);
        moveTween = transform.DOMove(new Vector2(transform.position.x + direction * 6, transform.position.y), 1);

        moveTween.OnUpdate(() =>
        {
            if (Physics2D.Raycast(transform.position, Vector3.right * dir, 0.5f, 1 << LayerMask.NameToLayer("Ground")))
            {
                moveTween.Kill();
            }
            float progress = moveTween.ElapsedPercentage();
            if (progress >= 0.7f)
            {
                canControl = false;
                if (player != null)
                    player.CancleController();
            }
        });
        moveTween.OnComplete(() => 
        {
            Destroy(gameObject); });
    }
}
