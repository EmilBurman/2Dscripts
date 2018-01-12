using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaFlower : MonoBehaviour
{

    public int staminaValue = 10;             // The amount added to the player's score when collecting.
    public float respawnTime = 10;
    public bool destroyOnOtherEntityPickup;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Player", if it is...
        if (destroyOnOtherEntityPickup)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                DeactivateAndRespawn();
                other.gameObject.GetComponent<IStamina>().EarnStamina(staminaValue);
            }
        }
        else
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                DeactivateAndRespawn();
                other.gameObject.GetComponent<IStamina>().EarnStamina(staminaValue);
            }
            else
                Destroy(this.gameObject);
        }
    }
    void DeactivateAndRespawn()
    {
        this.gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }
    void Respawn()
    {
        this.gameObject.SetActive(true);
    }
}
