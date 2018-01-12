using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllBehavior : MonoBehaviour
{

    IController2D controller;
    ITimeControll timeControll;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        timeControll = GetComponent<ITimeControll>();
    }

    void Update()
    {
        controller.SlowReverse();
        controller.FlashReverse();
    }

    void FixedUpdate()
    {
        timeControll.SlowReverse(controller.SlowReverse());
        timeControll.FlashReverse(controller.FlashReverse());
    }
}
