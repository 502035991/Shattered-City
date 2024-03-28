using System;
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


    [Serializable]
    public struct FruitPrefab
    {
        public CrystalCD name;
        public float CD;
        public float distance;
        public int Damage;
        public GameObject obj;
    }
    public FruitPrefab[] Skill;
}
