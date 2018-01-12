using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDash
{
    void Dash(float hAxis, float yAxis, bool dash);
    void ResetDash();
}