using System;
using UnityEngine;

[Serializable]
public class PlayerLayerData
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
}
