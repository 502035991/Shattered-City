using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillLandController : MonoBehaviour
{
    private Player player;
    private Action<bool> attackedPlayer;

    private bool Controlled;

    private float duration;
    private float startTime;
    public void Init(float duration,Vector2 pos , Action<bool> ac)
    {
        transform.position = pos;
        attackedPlayer = ac;

        startTime =Time.time;
        this.duration = duration;

        Controlled = true;
    }
    private void Update()
    {

        if (Time.time < startTime + duration)
        {
/*            if (Controlled && player != null && player.CanBeHurt)
            {
                attackedPlayer?.Invoke(true);
                Controlled = false;
            }*/
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
            return;

        player = collision.GetComponent<Player>();
    }
}
