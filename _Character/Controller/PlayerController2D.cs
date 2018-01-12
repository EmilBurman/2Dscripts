using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IController2D
{
    //Interface-----------------------------
    public float MoveHorizontal()
    {
        if (freezeInput)
            return 0f;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    public float MoveVertical()
    {
        if (freezeInput)
            return 0f;
        else
            return Input.GetAxisRaw("Vertical");
    }

    public bool Jump()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButton("Jump");
    }

    public bool EndJump()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButtonUp("Jump");
    }
    public bool Dash()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButton("Dash");
    }

    public bool Sprint()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButton("Sprint");
    }

    public bool Attack()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButton("Attack");
    }

    public bool EndAttackCharge()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButtonUp("Attack");
    }

    public bool FlashReverse()
    {
        if (freezeInput)
            return false;
        else
            return Input.GetButtonDown("Flash");
    }

    public bool SlowReverse()
    {
        freezeInput = Input.GetButton("Reverse");
        return Input.GetButton("Reverse");
    }
    //End interface-------------------------
    bool freezeInput;
}
