using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStamina : MonoBehaviour, IStamina
{
    //Interface------------------------------------------------
    public void LoseStamina(float staminaLoss)
    {
        // Reduce the current stamina by the stamina loss amount.
        currentStamina -= staminaLoss * Time.deltaTime;
    }

    public void EarnStamina(float staminaGain)
    {
        //Increase the stamina by the earn amount.
        if (currentStamina < 100)
            currentStamina += staminaGain * Time.deltaTime;
    }
    public bool StaminaRecharging()
    {
        return false;
    }
    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    //End interface--------------------------------------------
    [Header("Stamina setup.")]
    public float startingStamina = 100;                             // The amount of health the entity starts the game with.
    public float currentStamina;                                    // The current health of the entity.

    float staminaLoss;

    void Awake()
    {
        // Set the initial stamina of the entity.
        currentStamina = startingStamina;
    }
}
