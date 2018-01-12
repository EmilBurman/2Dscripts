using System;
using System.Collections;
using System.Collections.Generic;
using StateEnumerators;
using UnityEngine;

public class Swordslash : MonoBehaviour, IMelee
{

    //Interface
    public void SetDirection(Directions direction)
    {
        this.direction = direction;
    }
    public Directions GetDirection()
    {
        return direction;
    }
    //End interface

    [Header("Sword setup")]
    public float damage;
    public float slashSpeed;

    Directions direction;
    Vector2 targetDirection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Directions.Up:
                targetDirection = new Vector2(0, slashSpeed);
                break;
            case Directions.Down:
                targetDirection = new Vector2(0, -slashSpeed);
                break;
            case Directions.Left:
                targetDirection = new Vector2(-slashSpeed, 0);
                break;
            case Directions.Right:
                targetDirection = new Vector2(slashSpeed, 0);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.PROJECTILE))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag(Tags.ENEMY) || collision.gameObject.CompareTag(Tags.PLAYER))
            collision.gameObject.GetComponent<IHealth>().TakeDamage(damage);
    }
}
