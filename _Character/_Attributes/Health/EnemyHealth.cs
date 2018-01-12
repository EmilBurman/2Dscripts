using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IHealth
{
    //Interface------------------------------------------------
    public float CurrentHealth()
    {
        return currentHealth;
    }
    public void EarnHealth(float amount)
    {

    }
    public bool CanEarnBackHealth()
    {
        return false;
    }
    public void TakeDamage(float amount)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        // enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // And play the particles.
        // hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
            // ... the enemy is dead.
            Death();

    }
    public bool Invulnerable(bool state)
    {
        isInvulnerable = state;
        return isInvulnerable;
    }

    public void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Tell the animator that the enemy is dead.
        // anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
        Destroy(this.gameObject);
    }
    //End interface--------------------------------------------

    [Header("Health setup.")]
    public float startingHealth = 100;                            // The amount of health the entity starts the game with.
    public float currentHealth;                                   // The current health of the entity.
    public AudioClip deathClip;                                 // The audio clip to play when the entity dies.
    public float sinkSpeed = 2.5f;              		// The speed at which the enemy sinks through the floor when dead.


    // Internal system variables.
    Animator anim;                              		// Reference to the animator.
    AudioSource audioComponent;                     	// Reference to the audio source.
    ParticleSystem hitParticles;                		// Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            		// Reference to the capsule collider.
    bool isDead;                                		// Whether the enemy is dead.
    bool isSinking;                             		// Whether the enemy has started sinking through the floor.
    bool isInvulnerable;


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        audioComponent = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        // collider2D = GetComponent<Collider2D>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }


    void Update()
    {
        // Reset the damaged flag.
        // damaged = false;
        // If the enemy should be sinking...
        if (isSinking)
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(Vector2.down * sinkSpeed * Time.deltaTime);
    }

    void StartSinking()
    {
        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody2D>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}