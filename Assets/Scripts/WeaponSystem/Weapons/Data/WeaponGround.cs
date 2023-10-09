using UnityEngine;

public class WeaponGround : MonoBehaviour
{
    public float percentageModifier;

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
