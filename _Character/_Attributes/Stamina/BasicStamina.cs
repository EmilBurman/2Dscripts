using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicStamina : MonoBehaviour, IStamina
{
    //Interface------------------------------------------------
    public void LoseStamina(float staminaLoss)
    {
        // Reduce the current stamina by the stamina loss amount.
        currentStamina -= staminaLoss * Time.deltaTime;
        // Update the slider value
        staminaSlider.value = currentStamina;
        if (currentStamina <= 1)
            StartCoroutine(Exhausted());
    }

    public void EarnStamina(float staminaGain)
    {
        //Increase the stamina by the earn amount.
        if (currentStamina < maxStamina)
            currentStamina += staminaGain * Time.deltaTime;

        // Update the slider value
        staminaSlider.value = currentStamina;
    }
    public bool StaminaRecharging()
    {
        return rechargingStamina;
    }
    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    //End interface--------------------------------------------
    [Header("Stamina setup.")]
    public float startingStamina = 100;                             // The amount of health the entity starts the game with.
    public float currentStamina;                                    // The current health of the entity.
    public Slider staminaSlider;                                    // Reference to the UI's stamina bar.

    float staminaLoss;
    float maxStamina;
    bool rechargingStamina = false;
    public Image fillImage;

    void Awake()
    {
        // Set the initial stamina of the entity.
        currentStamina = startingStamina;
        maxStamina = staminaSlider.maxValue;
    }
    IEnumerator Exhausted()
    {
        rechargingStamina = true;
        new WaitForSeconds(5);
        while (currentStamina < 100)
        {
            fillImage.color = Color.grey;
            currentStamina += 10 * Time.deltaTime;
            staminaSlider.value = currentStamina;
            Debug.Log("Recharging stamina" + currentStamina);
            yield return 0;
        }
        if (currentStamina >= 100)
        {
            rechargingStamina = false;
            fillImage.color = Color.green;
        }
    }
}