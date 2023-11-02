using System;
using UnityEngine;

[Serializable]
public class LevelSystem
{
    [field: SerializeField] public int CurrentLevel { get; set; } = 1;
    [field: SerializeField] public int MaxLevel { get; private set; }

    [field: SerializeField] public int CurrentXP { get; set; } = 0;
    [field: SerializeField] public int XPNeededForFirstLevelUp { get; private set; }
    [field: SerializeField] public int XpNeededForNextLevelMultiplier { get; private set; }
    [field: SerializeField] public int XPNeededForNextLevel { get; private set; }

    public bool CanLevelUp()
    {
        return CurrentLevel < MaxLevel;
    }

    public void LevelUp()
    {
        ++CurrentLevel;
        CurrentXP -= XPNeededForNextLevel;
    }

    public bool HasEnoughXpForLevelUp()
    {
        return (XPNeededForNextLevel - CurrentXP) <= 0;
    }

    public void GainXp(int gainedXp)
    {
        CurrentXP += gainedXp;

        if (!HasEnoughXpForLevelUp())
        {
            return;
        }

        while (CanLevelUp())
        {
            LevelUp();
            CalculateAmountNeededForNextLevel();
        }
    }

    public void CalculateAmountNeededForNextLevel()
    {
        XPNeededForNextLevel = Mathf.RoundToInt(XPNeededForNextLevel * XpNeededForNextLevelMultiplier);
    }
}
