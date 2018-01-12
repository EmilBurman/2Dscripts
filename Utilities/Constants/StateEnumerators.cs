using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateEnumerators
{
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public enum AttackState
    {
        Ready,
        Attacking,
        Cooldown
    }

    public enum TimeState
    {
        Ready,
        Reversing,
        Cooldown
    }

    public enum Directions
    {
        Up,
        Down,
        Left,
        Right,
        DownLeft,
        DownRight,
        UpRight,
        UpLeft,
        Unknown
    }

    public enum PatrolState
    {
        PatrolRight,
        PatrolLeft,
        Stop
    }
}
