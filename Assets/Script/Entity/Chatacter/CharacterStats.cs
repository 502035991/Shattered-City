using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    //public Stat strength;
    //public Stat agility;//√ÙΩ›
    //public Stat intelligence;//÷«¡¶
    //public Stat vitality;//ªÓ¡¶

    //public Stat armor;
    public Stat damage;

    public Stat maxHealth;

    public Action onHealthChanged;

    [SerializeField]private int currentHealth;

    protected virtual void Start() 
    {
        currentHealth = GetMaxHealthValue();
    }
    public virtual void DoDamage(CharacterStats _targetStats , int damage2)
    {
        //int totalDamage = damage.GetValue() + strength.GetValue();
        int totalDamage = damage.GetValue() + damage2;
        _targetStats.TakeDamage(totalDamage);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        onHealthChanged?.Invoke();

        if (currentHealth < 0)
        {
            Dead();
        }
    }

    public int GetMaxHealthValue() => maxHealth.GetValue();
    public int GetCurrentHealthValue() => currentHealth;
    public abstract void Dead();
}
