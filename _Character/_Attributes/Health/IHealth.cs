using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface IHealth
{
    float CurrentHealth();
    void TakeDamage(float amount);
    void EarnHealth(float amount);
    bool CanEarnBackHealth();
    bool Invulnerable(bool state);
    void Death();
}