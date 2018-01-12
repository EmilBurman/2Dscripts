using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void GroundedAttack(bool attack, bool lockedShooting, float hAxis, float yAxis);
    void AirborneAttack(bool attack, bool lockedShooting, float hAxis, float yAxis);
    void WallAttackLeft(bool attack, bool lockedShooting);
    void WallAttackRight(bool attack, bool lockedShooting);
    void AttackButtonUp(bool attack);
}
