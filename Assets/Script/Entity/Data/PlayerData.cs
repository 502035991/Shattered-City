using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData" , menuName = "Data/PlayerData")]
public class PlayerData : BaseData
{

    [Header("ÌøÔ¾")]
    public float jumpVelocity = 15f;

    [Header("Ç½±Ú")]
    public float wallSlideVelocity = 3f;
    [Header("³å´Ì")]
    public float duration = 3f;
    public float dashVelocity = 20f;

    [Header("¹¥»÷")]
    public Vector2[] attacckMovement = null;
}
