using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class FlipSprite : MonoBehaviour
{

    Rigidbody2D playerRb2D;                                 // Reference to the entity's rigidbody.
    SpriteRenderer mySpriteRenderer;                        // Get entity sprite.
    ITerrainState stateMachine;                             // Get the entity terrain state.

    // Use this for initialization
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine = GetComponent<ITerrainState>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipTheSprite();
    }

    protected void FlipTheSprite()
    {
        if (playerRb2D.velocity.x < -0.001 || (stateMachine.Airborne() && stateMachine.WallRight()))
            mySpriteRenderer.flipX = true;
        if (playerRb2D.velocity.x > 0 || (stateMachine.Airborne() && stateMachine.WallLeft()))
            mySpriteRenderer.flipX = false;
    }
}
