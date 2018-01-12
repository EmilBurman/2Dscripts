using StateEnumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMelee
{
    void SetDirection(Directions direction);
    Directions GetDirection();
}
