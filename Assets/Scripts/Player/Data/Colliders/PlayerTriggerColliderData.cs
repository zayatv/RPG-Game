using System;
using UnityEngine;

[Serializable]
public class PlayerTriggerColliderData
{
    [field: SerializeField] public BoxCollider GroundCheckCollider { get; private set; }

    public Vector3 GroundCheckColliderExtends { get; private set; }

    public void Initialize()
    {
        GroundCheckColliderExtends = GroundCheckCollider.bounds.extents;
    }
}
