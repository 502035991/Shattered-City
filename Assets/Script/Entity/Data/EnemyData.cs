using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : BaseData
{
    [Header("���")]
    public int playerCheckLength;
    public LayerMask layerToPlayer;

    [Header("����")]
    public float attackCD;

}
