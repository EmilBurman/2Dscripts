using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOrResetSceneOnCollision : MonoBehaviour
{
    public bool teleportEntity;
    [Header("Setup for exit point")]
    public Vector3 exitPoint;


    float startTime;
    float distCovered;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (teleportEntity)
            Debug.DrawLine(transform.position, exitPoint, Color.green);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportEntity)
            collision.transform.position = exitPoint;
        else if (collision.CompareTag(Tags.PLAYER))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
