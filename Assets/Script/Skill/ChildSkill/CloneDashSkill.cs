using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDashSkill : Skill
{
    [SerializeField] private GameObject clonePrefab;

    CloneDashSkillController newClone;
    public void CreatClone(Transform playerTransform , int facingDirection) 
    {
        newClone = Instantiate(clonePrefab).gameObject.GetComponent<CloneDashSkillController>();
        newClone.SetPosition(playerTransform , facingDirection);
    }
    public void ChangePositionToClone(Transform playerTransform)
    {
        if(newClone != null)
        {
            playerTransform.position = newClone.transform.position;            
            newClone.DestroyObj();
        }
    }
    public bool ObjStats()
    {
        return newClone != null;
    }

}
