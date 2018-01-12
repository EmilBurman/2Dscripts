using StateEnumerators;
using System.Collections;
using UnityEngine;

public class DashMultiDirection : MonoBehaviour, IDash
{

    // Interface----------------------------
    public void Dash(float horizontalAxis, float verticalAxis, bool dash)
    {
        DashAbility(horizontalAxis, verticalAxis, dash);
    }

    public void ResetDash()
    {
        dashTimer = 0;
        dashState = DashState.Ready;
    }
    // End interface------------------------
    public DashState dashState;                     // Shows the current state of dashing.

    //Internal variables
    new Rigidbody2D rigidbody2D;                    // Reference to the player's rigidbody.
    IHealth invulnerableState;                      // Use to stop player from taking damage while dashing
    float dashTimer;                                // Shows the current cooldown.
    float dashCooldownLimit = 1f;                   // Sets the cooldown of the dash in seconds.
    float boostSpeed = 50f;
    float hAxis;
    float yAxis;
    Vector2 boostSpeedRight;
    Vector2 boostSpeedLeft;
    Vector2 boostSpeedUp;
    Vector2 boostSpeedDown;

    void Awake()
    {
        //Set the rigidbody and invulnerable components.
        rigidbody2D = GetComponent<Rigidbody2D>();
        invulnerableState = GetComponent<IHealth>();

        //Setup for all four different directions
        boostSpeedRight = new Vector2(boostSpeed, 0);
        boostSpeedLeft = new Vector2(-boostSpeed, 0);
        boostSpeedUp = new Vector2(0, boostSpeed);
        boostSpeedDown = new Vector2(0, -boostSpeed);
    }

    void DashAbility(float horizontalAxis, float verticalAxis, bool dash)
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (dash)
                {
                    //Get the axises
                    hAxis = horizontalAxis;
                    yAxis = verticalAxis;
                    //Check which one is larger
                    if (Mathf.Abs(hAxis) >= Mathf.Abs(yAxis))
                        StartCoroutine(HorizontalDash(0.1f));
                    else if (Mathf.Abs(hAxis) < Mathf.Abs(yAxis))
                        StartCoroutine(VerticalDash(0.1f));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                //Remove the invulnerability after a short delay
                Invoke("DelayedVulnerability", 0.1f);
                //Set the cooldown
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= dashCooldownLimit)
                {
                    dashTimer = dashCooldownLimit;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    IEnumerator HorizontalDash(float boostDur)
    {
        //create float to store the time this coroutine is operating
        float time = 0f;

        //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        while (boostDur > time) 
        {
            //Make the entity invulnerable while dashing
            invulnerableState.Invulnerable(true);
            //Increase our "time" variable by the amount of time that it has been since the last update
            time += Time.deltaTime;
            //Checks which direction to dash towards
            if (hAxis > 0)
                rigidbody2D.velocity = boostSpeedRight;
            else if (hAxis < 0)
                rigidbody2D.velocity = boostSpeedLeft;
            //go to next frame
            yield return 0;
        }
        //Makes sure the entity doesn't fall after dashing.
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }
    //Identical as the above coroutine but for y-axis dashing.
    IEnumerator VerticalDash(float boostDur)
    {
        float time = 0f;
        while (boostDur > time)
        {
            invulnerableState.Invulnerable(true);
            time += Time.deltaTime;
            if (yAxis > 0)
                rigidbody2D.velocity = boostSpeedUp;
            else if (yAxis < 0)
                rigidbody2D.velocity = boostSpeedDown;
            yield return 0;
        }
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }

    void DelayedVulnerability()
    {
        invulnerableState.Invulnerable(false);
    }
}
