using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour, IDoor
{
    //Interface------------------------------
    public bool canBeOpened()
    {
        throw new NotImplementedException();
    }

    public bool canBeClosed()
    {
        throw new NotImplementedException();
    }

    public bool isOpen()
    {
        if (Vector3.Distance(transform.position, endPosition) < 0.06f)
            return true;
        else
            return false;
    }
    public bool isClosed()
    {
        if (Vector3.Distance(transform.position, startPosition) < 0.06f)
            return true;
        else
            return false;
    }
    public void Open()
    {
        if (multipleTriggers)
        {
            if (CheckAllTriggers())
            {
                if (toSetPoint)
                    StartCoroutine(MoveDoor(endPosition));
            }
            else
            {
                AddTriggerAsTrue();
                if (CheckAllTriggers())
                {
                    if (toSetPoint)
                        StartCoroutine(MoveDoor(endPosition));
                }
            }
        }
        else
        {
            if (toSetPoint)
                StartCoroutine(MoveDoor(endPosition));
        }
    }
    public void Close()
    {
        if (toSetPoint)
            StartCoroutine(MoveDoor(startPosition));
    }
    //End interface-------------------------

    //If multiple are needed to open
    public bool multipleTriggers;
    public bool[] triggers;

    //Move between two points
    public bool toSetPoint;
    public Vector3 startPosition,
                   endPosition;

    public float smoothing = 1f;

    void Awake()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        Debug.DrawLine(startPosition, endPosition, Color.blue);
    }

    bool CheckAllTriggers()
    {
        bool canOpen = true;
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i] == false)
            {
                canOpen = false;
                break;
            }
        }
        if (canOpen)
            return true;
        else
            return false;
    }
    void AddTriggerAsTrue()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i] == false)
            {
                triggers[i] = true;
                break;
            }
        }
    }

    IEnumerator MoveDoor(Vector3 toPosition)
    {
        while (Vector3.Distance(transform.position, toPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, toPosition, smoothing * Time.deltaTime);
            yield return null;
        }
        print("Reached the target.");
        yield return new WaitForSeconds(2f);
        print("Door is now moved.");
    }
}

