using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrainState
{
    bool Grounded();
    bool Airborne();
    bool WallLeft();
    bool WallRight();
    bool EdgeRight();
    bool EdgeLeft();
}
