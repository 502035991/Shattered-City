using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dash {  get; private set; }
    public CloneDashSkill cloneDash { get; private set; }
    public TimeStopSkill timeStop {  get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        cloneDash = GetComponent<CloneDashSkill>();
        timeStop = GetComponent<TimeStopSkill>();
    }
}
