using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Grounded(float horizontalAxis, bool sprint);
    void Airborne(float horizontalAxis, bool sprint);
    void Wallride(float horizontalAxis, bool sprint);
}
