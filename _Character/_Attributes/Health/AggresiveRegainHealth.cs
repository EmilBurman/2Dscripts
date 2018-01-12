using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AggresiveRegainHealth : MonoBehaviour, IHealth
{
    //Interface------------------------------------------------
    public float CurrentHealth()
    {
        return currentHealth;
    }

    public void EarnHealth(float amount)
    {
        if (canEarnBackHealth)
        {
            if (amount > returnableHealth)
            {
                currentHealth += returnableHealth;
                returnableHealth = 0;
                canEarnBackHealth = false;
            }
            else
            {
                currentHealth += amount;
                returnableHealth -= amount;
            }
        }
        else if (currentHealth < maxHealth)
            currentHealth += amount;

        healthSlider.value = currentHealth;
    }
    public bool CanEarnBackHealth()
    {
        return canEarnBackHealth;
    }
    public void TakeDamage(float amount)
    {
        if (!isInvulnerable)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Initiate window for return of health.
            RegainHealth(amount);

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Play the hurt sound effect.
            // audioComponent.Play();

            // If the entity has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
                // ... it should die.
                Death();
        }
        healthSlider.value = currentHealth;
    }
    public bool Invulnerable(bool state)
    {
        isInvulnerable = state;
        return isInvulnerable;
    }
    public void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        audioComponent.clip = deathClip;
        audioComponent.Play();
    }


    //End interface--------------------------------------------

    [Header("Health setup.")]
    public float startingHealth = 100;                            // The amount of health the entity starts the game with.
    public float currentHealth;                                   // The current health of the entity.
    public bool isInvulnerable;
    public AudioClip deathClip;                                 // The audio clip to play when the entity dies.

    [Header("Hurt Image")]
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Slider healthSlider;                                 // Reference to the UI's health bar.

    // Internal system variables.
    Animator anim;                                              // Reference to the Animator component.
    AudioSource audioComponent;                                 // Reference to the AudioSource component.
    bool isDead;                                                // Whether the entity is dead.
    bool damaged;                                               // True when the player gets damaged.
    float returnableHealth;                                     // Amount player can earn back due to aggresive attacks
    bool canEarnBackHealth;                                     // Internal variable if the player can earn back health
    float maxHealth;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        audioComponent = GetComponent<AudioSource>();
        maxHealth = healthSlider.maxValue;

        // Set the initial health of the entity.
        currentHealth = startingHealth;

        healthSlider.value = currentHealth;
        isInvulnerable = false;
    }


    void Update()
    {
        // If the entity has just been damaged...
        if (damaged)
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        // Otherwise...
        else
            // ... transition the colour back to clear.
            // damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            // Reset the damaged flag.
            damaged = false;
    }
    void RegainHealth(float amount)
    {
        canEarnBackHealth = true;
        returnableHealth = amount;
    }
}