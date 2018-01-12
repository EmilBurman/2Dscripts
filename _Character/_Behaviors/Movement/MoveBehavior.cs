using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    IController2D controller;
    ITerrainState stateMachine;
    IMovement move;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        move = GetComponent<IMovement>();
    }

    void Update()
    {
        controller.MoveHorizontal();
        controller.Sprint();
    }


    void FixedUpdate()
    {
        if (stateMachine.Grounded())
            move.Grounded(controller.MoveHorizontal(), controller.Sprint());
        else if (stateMachine.Airborne())
        {
            move.Airborne(controller.MoveHorizontal(), controller.Sprint());
            if (stateMachine.WallLeft())
                move.Wallride(controller.MoveHorizontal(), controller.Sprint());
            if (stateMachine.WallRight())
                move.Wallride(controller.MoveHorizontal(), controller.Sprint());
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
