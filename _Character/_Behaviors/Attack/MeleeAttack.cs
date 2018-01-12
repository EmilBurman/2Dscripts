using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    // Interface----------------------------
    public void GroundedAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        throw new NotImplementedException();
    }

    public void AirborneAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        throw new NotImplementedException();
    }

    public void WallAttackLeft(bool attack, bool lockedShooting)
    {
        throw new NotImplementedException();
    }

    public void WallAttackRight(bool attack, bool lockedShooting)
    {
        throw new NotImplementedException();
    }

    public void AttackButtonUp(bool attack)
    {
        throw new NotImplementedException();
    }
    //End interface--------------------------------------------

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
