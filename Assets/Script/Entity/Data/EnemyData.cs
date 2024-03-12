using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : BaseData
{
    [Header("¼ì²â")]
    public int playerCheckLength;
    public float attackDistance;
    public LayerMask layerToPlayer;

    [Header("¹¥»÷")]
    public float attackCD;
    public float CriticalValue;

}
