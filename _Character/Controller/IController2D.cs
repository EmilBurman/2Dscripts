using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController2D
{
    float MoveHorizontal();
    float MoveVertical();
    bool Jump();
    bool EndJump();
    bool Dash();
    bool Sprint();
    bool Attack();
    bool EndAttackCharge();
    bool FlashReverse();
    bool SlowReverse();
}
