using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxStamina = 150;
    public float currentStamina = 150;

    // Method to decrease stamina
    public void DecreaseStamina(float amount)
    {
        currentStamina = Mathf.Max(currentStamina - amount, 0);
    }

    // Method to increase stamina
    public void IncreaseStamina(float amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, maxStamina);
    }
}
