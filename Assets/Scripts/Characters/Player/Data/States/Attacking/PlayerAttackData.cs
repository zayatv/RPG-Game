using System;
using UnityEngine;

[Serializable]
public class PlayerAttackData
{ 
    [field: SerializeField] public MeleeNormalAttackingData MeleeNormalAttackingData { get; private set; }
    [field: SerializeField] public PlayerChargedAttackingData ChargedAttackingData { get; private set; }
}
