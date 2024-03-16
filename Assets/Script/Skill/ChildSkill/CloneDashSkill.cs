using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDashSkill : Skill
{
    [SerializeField] private GameObject clongPrefab;

    public void CreatClong(Transform playerTransform , int facingDirection) 
    {
        GameObject newClong = Instantiate(clongPrefab);

        newClong.GetComponent<ClongDashSkillController>().SetPosition(playerTransform , facingDirection);
    }
}
