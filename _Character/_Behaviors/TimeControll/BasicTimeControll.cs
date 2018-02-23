using StateEnumerators;
using System.Collections;
using UnityEngine;

public class BasicTimeControll : MonoBehaviour, ITimeControll
{
    // Interface--------------------
    public void FlashReverse(bool flashReverse)
    {
        if (flashReverse && (timeState == TimeState.Ready))
        {
            this.transform.position = (positionArray[0] as PositionArray).position;
            timeState = TimeState.Reversing;
            positionArray.Clear();
        }
    }

    public void SlowReverse(bool reversing)
    {
        if (reversing && positionArray.Count > 2)
            isReversing = true;
        else
        {
            firstCycle = true;
            isReversing = false;
        }
    }

    public Vector2 GetPositionFromArrayAt(int pos)
    {
        return (positionArray[pos] as PositionArray).position;
    }
    // End interface----------------

    [Header("Cooldown variables")]
    public float reverseCooldownLimit;
    public float reverseTimer; 			        // Shows the current cooldown.


    public float staminaGain;
    new Rigidbody2D rigidbody2D;                        // Reference to the entity's rigidbody.
    ArrayList positionArray;
    float interpolation;
    public GameObject tracerObject;
    GameObject tracer;

    //Checks for if player is reversing
    bool isReversing = false;
    bool firstCycle = true;

    // Cooldown and state variables
    TimeState timeState;
    IHealth invulnerableState;                  // Use to stop entity from taking damage while dashing
    IStamina stamina;
    IActionState actionState;

    //Determine how much to save
    int keyframe = 10;

    //Amount recorded.
    int frameCounter = 0;

    //Amount played back.
    int reverseCounter = 0;

    // Variables to interpolate between keyframes
    Vector2 currentPosition;
    Vector2 previousPosition;

    void Awake()
    {
        timeState = TimeState.Ready;
        positionArray = new ArrayList();
        stamina = GetComponent<IStamina>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        actionState = GetComponent<IActionState>();
        invulnerableState = GetComponent<IHealth>();
        positionArray.Add(new PositionArray(this.transform.position));
        SpawnTracerObject();
    }

    void FixedUpdate()
    {
        if (!isReversing)
        {
            if (frameCounter < keyframe)
                frameCounter += 1;
            else
            {
                frameCounter = 0;
                positionArray.Add(new PositionArray(this.transform.position));
            }
        }
        ReverseAbility();

        if (positionArray.Count > 15)
            positionArray.RemoveAt(0);

        //Set the action state to reversing time if true
        if (isReversing)
            actionState.ReversingTime = true;
        else if (actionState.ReversingTime = true && !isReversing)
            actionState.ReversingTime = false;
    }

    void RestorePositions()
    {
        int lastIndex = positionArray.Count - 1;
        int secondToLastIndex = positionArray.Count - 2;

        if (secondToLastIndex > 0)
        {
            currentPosition = (positionArray[lastIndex] as PositionArray).position;
            previousPosition = (positionArray[secondToLastIndex] as PositionArray).position;
            positionArray.RemoveAt(lastIndex);
        }
    }

    void ReverseAbility()
    {
        switch (timeState)
        {
            case TimeState.Ready:
                if (isReversing)
                {
                    StartCoroutine(ReverseEntityTimeFlow());
                    DestroyTracerObject();
                    timeState = TimeState.Reversing;
                }
                break;
            case TimeState.Reversing:
                // Set the cooldown and initate cooldown state.
                invulnerableState.Invulnerable(false);
                //Set the cooldown
                reverseTimer = reverseCooldownLimit;
                timeState = TimeState.Cooldown;
                break;
            case TimeState.Cooldown:
                reverseTimer -= Time.deltaTime;
                if (reverseTimer <= 0)
                {
                    reverseTimer = 0;
                    SpawnTracerObject();
                    timeState = TimeState.Ready;
                }
                break;
        }
    }

    IEnumerator ReverseEntityTimeFlow()
    {
        while (isReversing)
        {
            invulnerableState.Invulnerable(true);
            if (reverseCounter > 0)
                reverseCounter -= 1;
            else
            {
                reverseCounter = keyframe;
                RestorePositions();
            }

            if (firstCycle)
            {
                firstCycle = false;
                RestorePositions();
            }
            stamina.EarnStamina(staminaGain);
            interpolation = reverseCounter / keyframe;
            this.transform.position = Vector2.Lerp(previousPosition, currentPosition, 1.1f * interpolation);
            yield return 0; //go to next frame
        }
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }
    void SpawnTracerObject()
    {
        tracer = Instantiate(tracerObject);
        tracer.transform.position = GetPositionFromArrayAt(0);
        tracer.transform.parent = this.transform;
    }
    void DestroyTracerObject()
    {
        Destroy(tracer);
    }
}

public class PositionArray
{
    public Vector2 position;

    public PositionArray(Vector2 position)
    {
        this.position = position;
    }
}
