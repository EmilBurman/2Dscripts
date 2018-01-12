using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    IController2D controller;
    ITerrainState stateMachine;
    IAttack attack;
    IDash dash;
    IJump jump;
    IMovement move;
    ITimeControll timeControll;
    bool lockedShooting;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        attack = GetComponent<IAttack>();
        dash = GetComponent<IDash>();
        jump = GetComponent<IJump>();
        move = GetComponent<IMovement>();
        timeControll = GetComponent<ITimeControll>();
        lockedShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Attack();
        controller.Dash();
        controller.Sprint();
        controller.Jump();
        controller.SlowReverse();
        controller.FlashReverse();
        controller.MoveHorizontal();
        controller.MoveVertical();
    }
    void FixedUpdate()
    {
        jump.SetContinousJump(controller.Jump(), controller.EndJump());
        timeControll.SlowReverse(controller.SlowReverse());
        timeControll.FlashReverse(controller.FlashReverse());

        if (stateMachine.Grounded())
        {
            attack.GroundedAttack(controller.Attack(), lockedShooting, controller.MoveHorizontal(), controller.MoveVertical());
            dash.ResetDash();
            jump.Grounded(controller.Jump(), controller.Sprint());
            move.Grounded(controller.MoveHorizontal(), controller.Sprint());
        }
        else if (stateMachine.Airborne())
        {
            if (stateMachine.WallLeft())
            {
                attack.WallAttackLeft(controller.Attack(), lockedShooting);
                dash.ResetDash();
                jump.LeftWall(controller.Jump());
                move.Wallride(controller.MoveHorizontal(), controller.Sprint());
            }
            else if (stateMachine.WallRight())
            {
                attack.WallAttackRight(controller.Attack(), lockedShooting);
                dash.ResetDash();
                jump.RightWall(controller.Jump());
                move.Wallride(controller.MoveHorizontal(), controller.Sprint());
            }
            else
            {
                attack.AirborneAttack(controller.Attack(), lockedShooting, controller.MoveHorizontal(), controller.MoveVertical());
                dash.Dash(controller.MoveHorizontal(), controller.MoveVertical(), controller.Dash());
                jump.Airborne(controller.Jump());
                move.Airborne(controller.MoveHorizontal(), controller.Sprint());
            }
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == Tags.PLATFORM)
        {
            transform.parent = c.gameObject.transform;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.transform.tag == Tags.PLATFORM)
        {
            transform.parent = null;
        }
    }
}
