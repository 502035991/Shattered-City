using UnityEngine;

public class TimeStopSkill : Skill
{
    [SerializeField] private GameObject timeStopObj;
    TimeStopSkillController newObj;

    public void CreatTimeStop(Transform playerPosition)
    {
        newObj = Instantiate(timeStopObj).gameObject.GetComponent<TimeStopSkillController>();
        newObj.transform.position = playerPosition.position; 
        newObj.Init();
    }
    public void CancleSkill()
    {
        newObj.CancelSkill();
    }
}
