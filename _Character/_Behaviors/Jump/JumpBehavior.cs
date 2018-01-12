using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    IController2D controller;
    ITerrainState stateMachine;
    IJump jump;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        jump = GetComponent<IJump>();
    }

    void Update()
    {
        controller.MoveHorizontal();
        controller.Sprint();
        controller.Jump();
    }

    void FixedUpdate()
    {
        jump.SetContinousJump(controller.Jump(), controller.EndJump());
        if (stateMachine.Grounded())
            jump.Grounded(controller.Jump(), controller.Sprint());
        else if (stateMachine.Airborne())
        {
            if (stateMachine.WallLeft())
                jump.LeftWall(controller.Jump());
            else if (stateMachine.WallRight())
                jump.RightWall(controller.Jump());
            else
                jump.Airborne(controller.Jump());
        }
    }
}
