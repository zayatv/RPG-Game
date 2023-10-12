using UnityEngine;

public class WeaponGround : MonoBehaviour
{
    public float percentageModifier {  get; private set; }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            percentageModifier += 0.1f;
        }
    }

    public void RestoreWeapon() {
        percentageModifier = 0f;
    }
}
