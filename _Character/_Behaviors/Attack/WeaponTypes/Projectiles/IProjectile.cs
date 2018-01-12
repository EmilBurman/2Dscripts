using StateEnumerators;
using UnityEngine;

public interface IProjectile
{
    void SetDirection(Directions direction);
    Directions GetDirection();
    float GetKnockback();
}
