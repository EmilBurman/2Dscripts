using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeControll
{
    void SlowReverse(bool reversing);
    void FlashReverse(bool flashReverse);
    Vector2 GetPositionFromArrayAt(int pos);
}
