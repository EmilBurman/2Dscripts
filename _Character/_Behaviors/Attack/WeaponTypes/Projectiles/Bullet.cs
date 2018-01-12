using StateEnumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IProjectile
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
    public float GetKnockback()
    {
        return knockback;
    }
    //End interface

    [Header("Bullet setup")]
    public float bulletSpeed;
    public float knockback;
    public float damage;

    //Internal variables
    new Rigidbody2D rigidbody2D;
    Directions direction;
    Vector2 targetDirection;
    IHealth parentHealth;

    // Use this for initialization
    void Awake()
    {
        if (transform.parent != null && (transform.parent.tag == Tags.PLAYER || transform.parent.tag == Tags.ENEMY))
        {
            parentHealth = GetComponent<IHealth>();
            Debug.Log("Success");
        }
        transform.parent = null;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Directions.Up:
                targetDirection = new Vector2(0, bulletSpeed);
                break;
            case Directions.Down:
                targetDirection = new Vector2(0, -bulletSpeed);
                break;
            case Directions.Left:
                targetDirection = new Vector2(-bulletSpeed, 0);
                break;
            case Directions.Right:
                targetDirection = new Vector2(bulletSpeed, 0);
                break;
        }
        rigidbody2D.velocity = targetDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.PROJECTILE))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag(Tags.ENEMY) || collision.gameObject.CompareTag(Tags.PLAYER))
            collision.gameObject.GetComponent<IHealth>().TakeDamage(damage);
    }
}
