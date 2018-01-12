using StateEnumerators;
using System.Collections;
using UnityEngine;

public class DashHorizontal : MonoBehaviour, IDash
{
    // Interface----------------------------
    public void Dash(float horizontalAxis, float verticalAxis, bool dash)
    {
        DashAbility(horizontalAxis, dash);
    }

    public void ResetDash()
    {
        dashTimer = 0;
        dashState = DashState.Ready;
    }
    // End interface------------------------

    new Rigidbody2D rigidbody2D;                        // Reference to the player's rigidbody.
    public DashState dashState;                     // Shows the current state of dashing.
    float dashTimer;                                // Shows the current cooldown.
    float dashCooldownLimit = 1f;                   // Sets the cooldown of the dash in seconds.
    private float boostSpeed = 50f;
    private Vector2 boostSpeedRight;
    private Vector2 boostSpeedLeft;
    private float hAxis;


        void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boostSpeedRight = new Vector2(boostSpeed, 0);
        boostSpeedLeft = new Vector2(-boostSpeed, 0);
    }

    void DashAbility(float horizontalAxis, bool dash)
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (dash)
                {
                    hAxis = horizontalAxis;
                    StartCoroutine(Dash(0.1f));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                // Set the cooldown and initate cooldown state.
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
    IEnumerator Dash(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0f; //create float to store the time this coroutine is operating
        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            if (hAxis > 0)
                rigidbody2D.velocity = boostSpeedRight;
            else if (hAxis < 0)
                rigidbody2D.velocity = boostSpeedLeft;
            yield return 0; //go to next frame
        }
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }
}
