using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{

    IController2D controller;
    ITerrainState stateMachine;
    IAttack attack;
    bool lockedShooting;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        attack = GetComponent<IAttack>();
        lockedShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Attack();
        controller.MoveHorizontal();
        controller.MoveVertical();
    }
    void FixedUpdate()
    {
        if (stateMachine.Grounded())
            attack.GroundedAttack(controller.Attack(), lockedShooting, controller.MoveHorizontal(), controller.MoveVertical());
        else if (stateMachine.Airborne())
        {
            if (stateMachine.WallLeft())
                attack.WallAttackLeft(controller.Attack(), lockedShooting);
            else if (stateMachine.WallRight())
                attack.WallAttackRight(controller.Attack(), lockedShooting);
            else
                attack.AirborneAttack(controller.Attack(), lockedShooting, controller.MoveHorizontal(), controller.MoveVertical());
        }
    }
}