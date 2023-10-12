using UnityEngine;

public class WeaponAffinity : MonoBehaviour
{
    public float percentageModifier { get; private set; }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            percentageModifier += 0.1f;
        }
    }
}
