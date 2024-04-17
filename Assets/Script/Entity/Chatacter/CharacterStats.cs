using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    //public Stat strength;
    //public Stat agility;//����
    //public Stat intelligence;//����
    //public Stat vitality;//����

    //public Stat armor;
    public Stat damage;

    public Stat maxHealth;

    public Action onHealthChanged;

    private EntityFX entityFX;
    private Entity entity;
    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        entity = GetComponentInParent<Entity>();
        entityFX = GetComponentInParent<EntityFX>();
        currentHealth = GetMaxHealthValue();
    }
    public virtual void DoDamage(CharacterStats _targetStats, int damage2)
    {
        //int totalDamage = damage.GetValue() + strength.GetValue();
        /*        if (!entity.CanBeHurt)
                    return;*/
        int totalDamage = damage.GetValue() + damage2;
        _targetStats.TakeDamage(totalDamage);
    }

    public void TakeDamage(int _damage)
    {
        if (!entity.GetHurtState())
            return;

        entityFX.FlashFX();
        currentHealth -= _damage;
        onHealthChanged?.Invoke();

        if (currentHealth < 0)
        {
            Dead();
        }
    }
    public int GetMaxHealthValue() => maxHealth.GetValue();
    public int GetCurrentHealthValue() => currentHealth;
    public void ResetCurrentHealth()
    {
        StartCoroutine(LinearHealthReset());
    }

    private IEnumerator LinearHealthReset()
    {
        int increment = 1; // ÿ�λָ���Ѫ������
        int targetHealth = GetMaxHealthValue(); // Ŀ��Ѫ��Ϊ���Ѫ��
        while (currentHealth < targetHealth)
        {
            currentHealth = Mathf.Min(currentHealth + increment, targetHealth);
            onHealthChanged?.Invoke();
            yield return null; // �ȴ�һ֡
        }
    }
    public abstract void Dead();
}
