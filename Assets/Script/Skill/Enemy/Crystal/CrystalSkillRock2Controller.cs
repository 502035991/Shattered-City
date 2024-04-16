using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillRock2Controller : MonoBehaviour
{
    private BoxCollider2D coll;
    private Animator anim;

    private Player player;

    private bool Controlled;
    private int dir;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    public void Init(int direction,Vector2 pos)
    {
        anim.SetBool("Skill_2", true);
        dir = direction;
        pos.x += 5 * dir;
        transform.position = pos;
        coll.enabled = true ;
        Controlled = false;


        if (dir != 1)
            transform.Rotate(0, 180, 0);
    }
    public void CancleController()
    {
        coll.enabled = false;
    }
    public void AnimFinishTrigger()
    {
        anim.SetBool("Skill_2", false);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (player != null && !Controlled)
        {
            Controlled = true;
            player.KnockBack(10,dir,1,true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
            return;

        player = collision.GetComponent<Player>();
    }
}
