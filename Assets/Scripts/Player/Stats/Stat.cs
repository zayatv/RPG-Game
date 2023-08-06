using System;
using UnityEngine;


[Serializable]
public class Stat
{
    [field: SerializeField] private float baseValue;
    [field: SerializeField] private float flatModifier;
    [field: SerializeField] private float percentageModifier;
    
    private float totalValue;

    public float GetTotalValue()
    {
        return totalValue;
    }

    public float GetBaseValue()
    {
        return baseValue;
    }

    public float GetFlatModifierValue()
    {
        return flatModifier;
    }

    public float GetPercentageModifierValue()
    {
        return percentageModifier;
    }

    public void AddValue(float valueToAdd, StatType valueType)
    {
        switch (valueType)
        {
            case StatType.FLAT:
            {
                flatModifier += valueToAdd;
                break;
            }
            case StatType.PERCENTAGE:
            {
                percentageModifier += valueToAdd;
                break;
            }
            default:
            {
                break;
            }
        }

        CalculateTotalStatValue();
    }

    public void RemoveValue(float valueToRemove, StatType valueType)
    {
        switch (valueType)
        {
            case StatType.FLAT:
            {
                flatModifier -= valueToRemove;
                break;
            }
            case StatType.PERCENTAGE:
            {
                percentageModifier -= valueToRemove;
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
        totalValue = (baseValue + flatModifier) * (percentageModifier / 100 + 1);
    }
}

public enum StatType
{
    FLAT,
    PERCENTAGE
}
