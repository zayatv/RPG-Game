using System;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int currentExperience;
    private int experinceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        currentExperience = 0;
        experinceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        if (currentExperience >= experinceToNextLevel)
        {
            level++;
            currentExperience -= experinceToNextLevel;

            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }
}
