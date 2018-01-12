using StateEnumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController2D : MonoBehaviour, IController2D
{
    //Interface----------------------------
    public bool Dash()
    {
        return false;
    }

    public bool FlashReverse()
    {
        return false;
    }

    public bool Jump()
    {
        return false;
    }

    public bool EndJump()
    {
        return false;
    }

    public float MoveHorizontal()
    {
        return horizontalDirection;
    }

    public float MoveVertical()
    {
        return 0;
    }

    public bool Attack()
    {

        return false;
    }

    public bool EndAttackCharge()
    {

        return false;
    }

    public bool SlowReverse()
    {
        return false;
    }

    public bool Sprint()
    {
        return sprint;
    }
    //End interface-------------------------

    private ITerrainState stateMachine;
    public PatrolState patrolState;                     // Shows the current state of dashing.
    float horizontalDirection;
    bool sprint;
    bool attackEntity;
    LineOfSight LoS;
    // Use this for initialization
    void Start()
    {
        stateMachine = GetComponent<ITerrainState>();
        LoS = GetComponent<LineOfSight>();
    }
    void Update()
    {
        PatrolStateCheck();
        LoSCheck();
    }


    void PatrolStateCheck()
    {
        switch (patrolState)
        {
            case PatrolState.PatrolRight:
                if (!stateMachine.WallRight() && stateMachine.EdgeRight())
                {
                    horizontalDirection = 1;
                }
                else
                    patrolState = PatrolState.PatrolLeft;
                break;
            case PatrolState.PatrolLeft:
                if (!stateMachine.WallLeft() && stateMachine.EdgeLeft())
                {
                    horizontalDirection = -1;
                }
                else
                    patrolState = PatrolState.PatrolRight;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER)
            collision.gameObject.GetComponent<IHealth>().TakeDamage(40);
    }
    void LoSCheck()
    {
    }
}
