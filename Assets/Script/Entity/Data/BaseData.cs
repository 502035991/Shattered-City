using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData : ScriptableObject
{
    [Header("���")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.4f;
    public float attackDistance = 2f;

    [Header("�ƶ�")]
    public float movementVelocity = 10f;
    public LayerMask layerToGround;
    public LayerMask layerToWall;
}
