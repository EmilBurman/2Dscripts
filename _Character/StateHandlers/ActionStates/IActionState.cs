using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionState
{
    bool Attacking();
    bool Dashing();
    bool Jumping();
    bool Idle();
    bool Moving();
    bool Sprinting();
    bool ReversingTime();
    bool Crouching();
}
