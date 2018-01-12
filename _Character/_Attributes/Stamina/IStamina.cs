using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IStamina
{
    void LoseStamina(float staminaLoss);
    void EarnStamina(float staminaGain);
    bool StaminaRecharging();
    float GetCurrentStamina();
}
