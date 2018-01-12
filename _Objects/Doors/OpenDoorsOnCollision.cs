using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsOnCollision : MonoBehaviour
{
    // Which doors to interact with
    public GameObject[] doors;

    // Only trigger once
    public bool triggerOnceOnly;

    //Public variables
    public float massTriggerAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].GetComponent<IDoor>().isClosed() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
                Debug.Log("Opening door");
                doors[i].GetComponent<IDoor>().Open();
            }
            else if (doors[i].GetComponent<IDoor>().isOpen() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
                Debug.Log("Closing door");
                doors[i].GetComponent<IDoor>().Close();
            }
        }
        if (triggerOnceOnly)
            Destroy(gameObject);
    }

    void Update()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            Debug.DrawLine(transform.position, doors[i].transform.position, Color.green);
        }
    }
}
