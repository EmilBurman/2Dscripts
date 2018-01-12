using UnityEngine;
using StateEnumerators;
using System;

public class ProjectileAttack : MonoBehaviour, IAttack
{

    // Interface-----------------------------
    public void GroundedAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        if (lockedShooting)
            LockedShooting();
        else
            SetProjectileDirection(hAxis, yAxis);

        // Complete copy from Attack() except with no "down" and no knockback.
        offset = transform.position;
        switch (attackState)
        {
            case AttackState.Ready:
                if (attack)
                {
                    switch (projectileDirection)
                    {
                        case Directions.Right:
                            offset.x += 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Right);
                            break;
                        case Directions.Left:
                            offset.x -= 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Left);
                            break;
                        case Directions.Up:
                            offset.y += 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Up);
                            break;
                        case Directions.Down:
                            attackState = AttackState.Ready;
                            break;
                    }
                    attackState = AttackState.Attacking;
                }
                break;
            case AttackState.Attacking:
                attackTimer += Time.deltaTime * 3;
                if (attackTimer >= attackCooldown)
                {
                    attackTimer = attackCooldown;
                    attackState = AttackState.Cooldown;
                }
                break;
            case AttackState.Cooldown:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attackTimer = 0;
                    attackState = AttackState.Ready;
                }
                break;
        }
    }

    public void AirborneAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        if (lockedShooting)
            LockedShooting();
        else
            SetProjectileDirection(hAxis, yAxis);
        Attack(attack);
    }

    public void WallAttackLeft(bool attack, bool lockedShooting)
    {
        if (lockedShooting)
            LockedShooting();
        else
            projectileDirection = Directions.Right;
        Attack(attack);
    }

    public void WallAttackRight(bool attack, bool lockedShooting)
    {
        if (lockedShooting)
            LockedShooting();
        else
            projectileDirection = Directions.Left;
        Attack(attack);
    }
    public void AttackButtonUp(bool attack)
    {
        throw new NotImplementedException();
    }
    // End interface------------------------

    //Public variables
    public float attackCooldown;                                // Sets the cooldown of the dash in seconds.
    public GameObject projectilePrefab;                         // The projectile to launch.
    public bool knockback;                                      // If the entity should get knockedback with every shot.

    //Projectile prefab
    GameObject projectile;

    //State enums
    AttackState attackState;
    Directions projectileDirection;

    //Internal variables
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;                              // Get entity sprite.
    float attackTimer;                                          // Shows the current cooldown.
    Vector2 offset;
    float knockbackForce;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        knockbackForce = projectilePrefab.GetComponent<IProjectile>().GetKnockback();
    }

    void Attack(bool attack)
    {
        offset = transform.position;
        switch (attackState)
        {
            case AttackState.Ready:
                if (attack)
                {
                    switch (projectileDirection)
                    {
                        case Directions.Right:
                            offset.x += 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Right);
                            if (knockback)
                                rigidbody2D.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
                            break;
                        case Directions.Left:
                            offset.x -= 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Left);
                            if (knockback)
                                rigidbody2D.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                            break;
                        case Directions.Up:
                            offset.y += 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Up);
                            if (knockback)
                                rigidbody2D.AddForce(Vector2.down * knockbackForce, ForceMode2D.Impulse);
                            break;
                        case Directions.Down:
                            offset.y -= 0.7f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Down);
                            if (knockback)
                                rigidbody2D.AddForce(Vector2.up * knockbackForce, ForceMode2D.Impulse);
                            break;
                    }
                    attackState = AttackState.Attacking;
                }
                break;
            case AttackState.Attacking:
                attackTimer += Time.deltaTime * 3;
                if (attackTimer >= attackCooldown)
                {
                    attackTimer = attackCooldown;
                    attackState = AttackState.Cooldown;
                }
                break;
            case AttackState.Cooldown:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attackTimer = 0;
                    attackState = AttackState.Ready;
                }
                break;
        }
    }

    void SetProjectileDirection(float hAxis, float yAxis)
    {
        if (Mathf.Abs(hAxis) >= Mathf.Abs(yAxis))
            HorziontalFire(hAxis);
        else if (Mathf.Abs(hAxis) < Mathf.Abs(yAxis))
            VeritcalFire(yAxis);
    }

    void HorziontalFire(float hAxis)
    {
        if (hAxis < 0)
            projectileDirection = Directions.Left;
        else if (hAxis > 0)
            projectileDirection = Directions.Right;
    }

    void VeritcalFire(float yAxis)
    {
        if (yAxis < 0)
            projectileDirection = Directions.Down;
        else if (yAxis > 0)
            projectileDirection = Directions.Up;
    }

    void LockedShooting()
    {

    }
}
