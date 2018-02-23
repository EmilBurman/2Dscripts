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
        dashCooldownTimer = 0;
        dashState = DashState.Ready;
    }
    // End interface------------------------
    public DashState dashState;                     // Shows the current state of dashing.
    [SerializeField] float dashCooldownTimer;       // Shows the current cooldown.
    public float dashCooldownLimit;                 // Sets the cooldown of the dash in seconds.
    public float boostSpeed;
    public float dashDuration;

    //Internal variables
    new Rigidbody2D rigidbody2D;                    // Reference to the player's rigidbody.
    IHealth invulnerableState;                      // Use to stop player from taking damage while dashing
    IActionState actionState;
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
        actionState = GetComponent<IActionState>();

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
                    actionState.Dashing = true;
                    //Get the axises
                    hAxis = horizontalAxis;
                    yAxis = verticalAxis;
                    //Check which one is larger
                    if (Mathf.Abs(hAxis) >= Mathf.Abs(yAxis))
                        StartCoroutine(HorizontalDash(dashDuration));
                    else if (Mathf.Abs(hAxis) < Mathf.Abs(yAxis))
                        StartCoroutine(VerticalDash(dashDuration));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                //Remove the invulnerability after a short delay
                Invoke("DelayedVulnerability", 0.1f);
                //Set the cooldown
                dashCooldownTimer = dashCooldownLimit;
                dashState = DashState.Cooldown;
                break;
            case DashState.Cooldown:
                dashCooldownTimer -= Time.deltaTime;
                if (dashCooldownTimer <= 0)
                {
                    dashCooldownTimer = 0;
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
        actionState.Dashing = false;
    }
}
