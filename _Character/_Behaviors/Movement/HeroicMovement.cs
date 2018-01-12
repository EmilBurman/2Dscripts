using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicMovement : MonoBehaviour, IMovement
{
    // Interface-------------------------------------------
    public void Airborne(float horizontalAxis, bool sprint)
    {
        float airborneMovement = 0.5f;

        // Help change direction in the air to the right
        if (horizontalAxis > 0 && mySpriteRenderer.flipX)
        {
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.AddForce(vector2Right * 1.4f, ForceMode2D.Impulse);
        }
        // If not changing direction continue as normal.
        else if (horizontalAxis > 0)
            rigidbody2D.AddForce(vector2Right * airborneMovement, ForceMode2D.Impulse);

        // Help change direction in the air to the left
        if (horizontalAxis < 0 && !mySpriteRenderer.flipX)
        {
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.AddForce(vector2Left * 1.4f, ForceMode2D.Impulse);
        }
        // If not changing direction continue as normal.
        else if (horizontalAxis < 0)
            rigidbody2D.AddForce(vector2Left * airborneMovement, ForceMode2D.Impulse);

        // If player is falling, increase gravity
        if (rigidbody2D.velocity.y < -0.001)
            rigidbody2D.AddForce(Physics.gravity * 1.2f, ForceMode2D.Force);
        MovementSpeedClamp();

        /*
        if (sprint && !stamina.StaminaRecharging())
            StaminaLossCheck(staminaLoss);
        */
    }

    public void Grounded(float horizontalAxis, bool sprint)
    {
        SetCanWallRide(true);
        rigidbody2D.velocity = new Vector2(horizontalAxis * moveSpeed, rigidbody2D.velocity.y);

        StaminaGainCheck(sprint);

        if (sprint && !stamina.StaminaRecharging())
        {
            if (rigidbody2D.velocity.x > 0)
                rigidbody2D.AddForce(vector2Right * (moveSpeed * sprintForceMultiplier), ForceMode2D.Force);
            else if (rigidbody2D.velocity.x < 0)
                rigidbody2D.AddForce(vector2Left * (moveSpeed * sprintForceMultiplier), ForceMode2D.Force);
            StaminaLossCheck(staminaLoss);
        }
    }

    public void Wallride(float horizontalAxis, bool sprint)
    {
        if (!(rigidbody2D.velocity.y > -0.01f) && sprint && canWallRide)
        {
            rigidbody2D.AddForce(Vector2.up * Mathf.Abs(horizontalAxis * rigidbody2D.velocity.x * 1.3f), ForceMode2D.Impulse);
            canWallRide = false;
        }
        else
            Airborne(horizontalAxis, sprint);
        MovementSpeedClamp();
    }
    //End interface----------------------------------------

    // Variables needed for movement.
    [Header("Movement variables.")]
    public float moveSpeed = 8.0f;                          // The speed that the player will move at.
    public float sprintForceMultiplier = 40.0f;             // The factor which will * the player speed when sprinting.
    public float staminaLoss;
    public float staminaGain;

    //Internal variables
    bool canWallRide;                                       // Checks if the player can wallride
    new Rigidbody2D rigidbody2D;                            // Reference to the player's rigidbody.
    SpriteRenderer mySpriteRenderer;                        // To get the current sprite.
    Vector2 vector2Right;
    Vector2 vector2Left;
    IStamina stamina;

    // Use this for initialization
    void Awake()
    {
        // Set up references.
        rigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        stamina = GetComponent<IStamina>();

        //Create own definitions for left and right
        vector2Right = new Vector2(1f, rigidbody2D.velocity.y);
        vector2Left = new Vector2(-1f, rigidbody2D.velocity.y);
    }
    void SetCanWallRide(bool setWallRide)
    {
        canWallRide = setWallRide;
    }

    void MovementSpeedClamp()
    {
        //Checks all movement and clamps it.
        if (rigidbody2D.velocity.sqrMagnitude > moveSpeed * 1.2f)
            rigidbody2D.velocity *= 0.97f;

        //Harder clamp for y movement.
        if (rigidbody2D.velocity.y < -14f)
            rigidbody2D.velocity *= 0.97f;
    }
    void StaminaGainCheck(bool sprint)
    {
        if (!sprint && !stamina.StaminaRecharging())
            stamina.EarnStamina(staminaGain);
    }

    void StaminaLossCheck(float loseAmount)
    {
        if ((rigidbody2D.velocity.x > 0 || rigidbody2D.velocity.x < 0) || (rigidbody2D.velocity.y > 0 || rigidbody2D.velocity.y < 0))
            stamina.LoseStamina(loseAmount);
    }
}
