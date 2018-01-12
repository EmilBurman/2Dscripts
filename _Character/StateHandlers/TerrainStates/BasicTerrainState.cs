using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BasicTerrainState : MonoBehaviour, ITerrainState
{
    //Interface--------------------
    public bool Grounded()
    {
        return IsGrounded();
    }

    public bool Airborne()
    {
        return (!IsGrounded());
    }

    public bool WallLeft()
    {
        return CheckWallLeft();
    }

    public bool WallRight()
    {
        return CheckWallRight();
    }
    public bool EdgeRight()
    {
        return CheckEdgeRight();
    }
    public bool EdgeLeft()
    {
        return CheckEdgeLeft();
    }
    //End interface---------------

    [Header("Terrain layers.")]
    public LayerMask groundLayer;                                   // Layermask variable to check for ground layer.
    public LayerMask wallLayer;                                     // Layermask variable to check for wall layer.

    //Checks for ground.
    [Header("Ground variables.")]
    public float groundCheckSpacing = 0.94f;                        // Determines how to space out the left and right ground checks.
    public float yAxisRayCastStartPoint = 0.01f;                    // Defines how far out from the collider to check for the ground.
    float yAxisExtendFromCollider;                                  // Gets collider and sets raycast starting point for the ground.

    //Checks for walls.
    [Header("Wall variables.")]
    public float wallCheckSpacing = 2f;                             // Determines how to space out the top and bottom wall checks.
    public float xAxisRayCastStartPoint = 0.01f;                    // Defines how far out from the collider to check for the walls.
    float xAxisExtendFromCollider;                                  // Gets collider and sets raycast starting point for the ground.

    //Checks for platform edges.
    [Header("Platform variables.")]
    public float edgeCheckSpacing = 2f;                             // Determines how to space out the top and bottom wall checks.

    // Distance to check
    [Header("Displacement variables.")]
    public float groundDistanceCheck;                               // Defines the distance to check for terrain.
    public float wallDistanceCheck;                                 // Defines the distance to check for terrain.

    // Internal system variables.
    new Rigidbody2D rigidbody2D;                                    // Reference to the player's rigidbody.
    bool grounded;
    bool wallLeft;
    bool wallRight;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        yAxisExtendFromCollider = (GetComponent<Collider2D>().bounds.extents.y + yAxisRayCastStartPoint);
        xAxisExtendFromCollider = (GetComponent<Collider2D>().bounds.extents.x + xAxisRayCastStartPoint);
        groundDistanceCheck = 0.03f;
        wallDistanceCheck = 0.03f;
    }

    bool IsGrounded()
    {
        Color color;
        bool groundCenterCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, groundDistanceCheck, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, color = Color.yellow, 0.1f);

        bool groundRightCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + groundCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, groundDistanceCheck, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + groundCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, color = Color.green, 0.1f);

        bool groundLeftCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - groundCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, groundDistanceCheck, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - groundCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, color = Color.red, 0.1f);
        if (!(groundLeftCheck && groundCenterCheck && groundRightCheck))
            Invoke("CoyoteTime", 0.2f);
        else
            grounded = true;

        return (grounded) ? true : false;
    }
    bool CheckWallLeft()
    {
        Color color;
        bool leftCenterCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y), Vector2.left, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y), Vector2.left, color = Color.yellow, 0.1f);

        bool leftBottomCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y - wallCheckSpacing), Vector2.left, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y - wallCheckSpacing), Vector2.left, color = Color.green, 0.1f);

        bool leftTopCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y + wallCheckSpacing), Vector2.left, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - xAxisExtendFromCollider, rigidbody2D.transform.position.y + wallCheckSpacing), Vector2.left, color = Color.red, 0.1f);

        if (!(leftCenterCheck && leftBottomCheck && leftTopCheck))
            Invoke("CoyoteTimeLeft", 0.1f);
        else
            wallLeft = true;

        return wallLeft ? true : false;
    }
    bool CheckWallRight()
    {
        Color color;
        bool rightCenterCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y), Vector2.right, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y), Vector2.right, color = Color.yellow, 0.1f);

        bool rightBottomCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y - wallCheckSpacing), Vector2.right, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y - wallCheckSpacing), Vector2.right, color = Color.green, 0.1f);

        bool rightTopCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y + wallCheckSpacing), Vector2.right, wallDistanceCheck, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + xAxisExtendFromCollider, rigidbody2D.transform.position.y + wallCheckSpacing), Vector2.right, color = Color.red, 0.1f);

        if (!(rightCenterCheck && rightBottomCheck && rightTopCheck))
            Invoke("CoyoteTimeRight", 0.1f);
        else
            wallRight = true;

        return wallRight ? true : false;
    }
    bool CheckEdgeRight()
    {
        Color color;
        bool rightEdgeCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + edgeCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, groundDistanceCheck, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + edgeCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, color = Color.white, 0.1f);

        return (rightEdgeCheck) ? true : false;
    }
    bool CheckEdgeLeft()
    {
        Color color;
        bool leftEdgeCheck = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - edgeCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, groundDistanceCheck, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - edgeCheckSpacing, rigidbody2D.transform.position.y - yAxisExtendFromCollider), Vector2.down, color = Color.white, 0.1f);

        return (leftEdgeCheck) ? true : false;
    }

    void CoyoteTime()
    {
        grounded = false;
    }
    void CoyoteTimeLeft()
    {
        wallLeft = false;
    }
    void CoyoteTimeRight()
    {
        wallRight = false;
    }
}
