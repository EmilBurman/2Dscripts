using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Essence : MonoBehaviour
{
    public int scoreValue = 10;             // The amount added to the player's score when collecting.
    public bool playerOnly;

    void FixedUpdate()
    {
        transform.Rotate(0, 6.0f * 10f * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Player", if it is...
        if (playerOnly)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.gameObject.SetActive(false);
                ScoreManager.score += scoreValue;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
                ScoreManager.score += scoreValue;
            }
            else
                Destroy(this.gameObject);
        }
    }
}
