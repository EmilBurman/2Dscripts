using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlower : MonoBehaviour {

    public int healthValue = 10;             // The amount added to the player's score when collecting.
    public bool playerOnly;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Player", if it is...
        if (playerOnly)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                this.gameObject.SetActive(false);
                Invoke("Respawn", 10);
                other.gameObject.GetComponent<IHealth>().EarnHealth(healthValue);
            }
        }
        else
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                other.gameObject.GetComponent<IHealth>().EarnHealth(healthValue);
                Destroy(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
    }
    void Respawn()
    {
        this.gameObject.SetActive(true);
    }
}
