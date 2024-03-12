using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData" , menuName = "Data/PlayerData")]
public class PlayerData : BaseData
{

    [Header("��Ծ")]
    public float jumpVelocity = 15f;

    [Header("ǽ��")]
    public float wallSlideVelocity = 3f;
    [Header("���")]
    public float duration = 3f;
    public float dashVelocity = 20f;

    [Header("����")]
    public Vector2[] attacckMovement = null;
}
