using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Runestone", menuName = "Custom/Runestones/Runestone", order = 2)]

public class RunestoneSO : ScriptableObject
{
    public enum RunestoneColor {
        Green,
        Red,
        Blue
    }

    [field: Header("General Information")]
    [field: SerializeField] public string RunestoneName { get; private set; }
    [field: SerializeField] public string RunestoneDescription { get; private set; }
    [field: SerializeField] public RunestoneColor MyRunestoneColor { get; private set; }
    //[field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public GameObject RuneModel { get; private set; }

    [field: Header("Stat Changes")]
    [field: SerializeField] public Stat BaseAttack { get; private set; }
    [field: SerializeField] public Stat PrimaryStat { get; private set; }
    [field: SerializeField] public Stat SecondaryStat { get; private set; }

    [field: Header("Ultimates")]
    [field: SerializeField] public List<string> EquippedSkills { get; private set; }

}