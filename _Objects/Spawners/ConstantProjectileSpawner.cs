using StateEnumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantProjectileSpawner : MonoBehaviour
{
    [Header("Projectile setup")]
    public float attackCooldown;
    public Directions projectileDirection;
    public GameObject projectilePrefab;

    //Internal
    GameObject projectile;
    AttackState attackState;
    float attackTimer;                                          // Shows the current cooldown.
    Vector2 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset = transform.position;
        switch (attackState)
        {
            case AttackState.Ready:
                switch (projectileDirection)
                {
                    case Directions.Right:
                        offset.x += 0.6f;
                        projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                        projectile.GetComponent<IProjectile>().SetDirection(Directions.Right);
                        break;
                    case Directions.Left:
                        offset.x -= 0.6f;
                        projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                        projectile.transform.parent = gameObject.transform;
                        projectile.GetComponent<IProjectile>().SetDirection(Directions.Left);
                        break;
                    case Directions.Up:
                        offset.y += 0.6f;
                        projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                        projectile.GetComponent<IProjectile>().SetDirection(Directions.Up);
                        break;
                    case Directions.Down:
                        offset.y -= 0.6f;
                        projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                        projectile.GetComponent<IProjectile>().SetDirection(Directions.Down);
                        break;
                }
                attackState = AttackState.Attacking;
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
}
