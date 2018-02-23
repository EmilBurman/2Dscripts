using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : MonoBehaviour, IActionState
{
    public bool Dashing
    {
        get
        {
            return IsDashing;
        }

        set
        {
            IsDashing = value;
        }
    }

    public bool Jumping
    {
        get
        {
            return IsJumping;
        }

        set
        {
            IsJumping = value;
        }
    }
    public bool Idle
    {
        get
        {
            return IsIdle;
        }

        set
        {
            IsIdle = value;
        }
    }
    public bool Moving
    {
        get
        {
            return IsMoving;
        }

        set
        {
            IsMoving = value;
        }
    }
    public bool Sprinting
    {
        get
        {
            return IsSprinting;
        }

        set
        {
            IsSprinting = value;
        }
    }
    public bool ReversingTime
    {
        get
        {
            return IsReversingTime;
        }

        set
        {
            IsReversingTime = value;
        }
    }
    public bool Crouching
    {
        get
        {
            return IsCrouching;
        }

        set
        {
            IsCrouching = value;
        }
    }

    [Header("Action state variables.")]
    [SerializeField] bool IsDashing;
    [SerializeField] bool IsJumping;
    [SerializeField] bool IsIdle;
    [SerializeField] bool IsMoving;
    [SerializeField] bool IsSprinting;
    [SerializeField] bool IsReversingTime;
    [SerializeField] bool IsCrouching;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
