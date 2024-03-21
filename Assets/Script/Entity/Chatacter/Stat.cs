using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    public List<int> modifiers;


    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void AddModifer(int value)
    {
        modifiers.Add(value);
    }
    public void RemoveModifier(int value)
    {
        modifiers.RemoveAt(value);
    }
}
