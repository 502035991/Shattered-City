using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : BaseData
{
    [Header("¼ì²â")]
    public int playerCheckLength;
    public LayerMask layerToPlayer;

    [Header("¹¥»÷")]
    public float attackCD;


    [Serializable]
    public struct FruitPrefab
    {
        public CrystalAttackMenu name;
        public float CD;
        public float distance;
        public int Damage;
        [Header("È¨ÖØ")]
        public float weight;
        public GameObject obj;
    }
    public FruitPrefab[] AllAttack;
    private FruitPrefab[] _allAttack;

    private Dictionary<CrystalAttackMenu, FruitPrefab> skillDictionary;
    public Dictionary<CrystalAttackMenu, FruitPrefab> Skill
    {
        get
        {
            if (skillDictionary == null)
            {
                _allAttack = new FruitPrefab[AllAttack.Length];
                Array.Copy(AllAttack, _allAttack, AllAttack.Length);
                Array.Sort(_allAttack, (a, b) => b.weight.CompareTo(a.weight));

                skillDictionary = new Dictionary<CrystalAttackMenu, FruitPrefab>();
                foreach (var attack in _allAttack)
                {
                    skillDictionary[attack.name] = attack;
                }
            }
            return skillDictionary;
        } 
    }
}
