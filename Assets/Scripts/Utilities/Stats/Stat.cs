using System;
using UnityEngine;


[Serializable]
public class Stat
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float FlatModifier { get; private set; }
    [field: SerializeField] public float PercentageModifier { get; private set; }

    [field: SerializeField]  public float CurrentValue { get;  set; }
    public float TotalValue { get; set; }

    public void AddModifierValue(float valueToAdd, StatValueType valueType)
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

    public void RemoveModifierValue(float valueToRemove, StatValueType valueType)
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

    public void RemoveCurrentValue(float valueToRemove)
    {
        CurrentValue -= valueToRemove;
    }
}

public enum StatValueType
{
    Flat,
    Percentage
}
