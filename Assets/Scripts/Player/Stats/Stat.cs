using System;
using UnityEngine;


[Serializable]
public class Stat
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float FlatModifier { get; private set; }
    [field: SerializeField] public float PercentageModifier { get; private set; }

    public float CurrentValue { get; private set; }
    public float TotalValue { get; private set; }

    public void AddValue(float valueToAdd, StatValueType valueType)
    {
        switch (valueType)
        {
            case StatValueType.Flat:
            {
                FlatModifier += valueToAdd;
                break;
            }
            case StatValueType.Percentage:
            {
                PercentageModifier += valueToAdd;
                break;
            }
            default:
            {
                break;
            }
        }

        CalculateTotalStatValue();
    }

    public void RemoveValue(float valueToRemove, StatValueType valueType)
    {
        switch (valueType)
        {
            case StatValueType.Flat:
            {
                FlatModifier -= valueToRemove;
                break;
            }
            case StatValueType.Percentage:
            {
                PercentageModifier -= valueToRemove;
                break;
            }
            default:
            {
                break;
            }
        }
        
        CalculateTotalStatValue();
    }

    public void CalculateTotalStatValue()
    {
        float oldTotalValue = TotalValue;
        TotalValue = (BaseValue + FlatModifier) * (PercentageModifier / 100 + 1);
        CurrentValue += TotalValue - oldTotalValue;
    }
}

public enum StatValueType
{
    Flat,
    Percentage
}
