using System;
using UnityEngine;

[Serializable]
public class Currency
{
    [field: SerializeField] public string CurrencyName { get; private set; }
    [field: SerializeField] public string CurrencyDescription { get; private set; }

    [field: SerializeField] public int CurrencyAmount { get; set; } = 0;
}
