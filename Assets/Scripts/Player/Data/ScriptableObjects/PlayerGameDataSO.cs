using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom/Player/PlayerData")]
public class PlayerGameDataSO : ScriptableObject
{
    [field: Header("Currency")]
    [field: SerializeField] public Currency FreeCurrency { get; private set; }
    [field: SerializeField] public Currency AnotherFreeCurrency { get; private set; }
    [field: SerializeField] public Currency PaidCurrency { get; private set; }
}
