using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Stat
{
    [SerializeField] private int baseValue;

    private List<int> modifiersFlat = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiersFlat.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiersFlat.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiersFlat.Remove(modifier);
        }
    }
}
