using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGround : MonoBehaviour
{
    public float percentageModifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            percentageModifier += 0.1f;
        }
    }

    private void RestoreWeapon() {
        percentageModifier = 0f;
    }
}
