using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    [field: Header("General Information")]
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public string SkillDescription { get; private set; }
    [field: SerializeField] public Rarity SkillRarity { get; private set; }

    [field: Header("Skill")]
    [field: SerializeField] public List<SkillSO> SkillSynergy { get; private set; }
}
