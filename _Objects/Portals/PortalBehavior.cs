using StateEnumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    //Public variables
    [Header("Setup for portal")]
    public GameObject portal;
    public bool portalToPortal;
    public float offSetDistance;

    [Header("Setup for exit point")]
    public Vector3 exitPoint;

    bool isActive;

    //Internal variables
    Directions angleDirection;
    Vector3 entityOffset;
    Vector3 offSetDistanceFixed;
    Vector2 exitDirection;
    float speed;

    void Start()
    {
        if (transform.rotation.eulerAngles.z >= 0 && transform.rotation.eulerAngles.z < 90)
            angleDirection = Directions.Up;

        if (transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z < 180)
            angleDirection = Directions.Left;

        if (transform.rotation.eulerAngles.z >= 180 && transform.rotation.eulerAngles.z < 270)
            angleDirection = Directions.Down;

        if (transform.rotation.eulerAngles.z >= 270 && transform.rotation.eulerAngles.z < 360)
            angleDirection = Directions.Right;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (portalToPortal)
            Debug.DrawLine(transform.position, portal.transform.position, Color.green);
        else
            Debug.DrawLine(transform.position, exitPoint, Color.green);
    }

    public Vector3 portalExitPoint(Bounds entitySize)
    {
        switch (angleDirection)
        {
            case Directions.Up:
                entityOffset = new Vector3(0, entitySize.extents.y, 0);
                offSetDistanceFixed = new Vector3(0, offSetDistance, 0);
                exitDirection = new Vector2(0, 1);
                break;
            case Directions.Right:
                entityOffset = new Vector3(entitySize.extents.x, 0, 0);
                offSetDistanceFixed = new Vector3(offSetDistance, 0, 0);
                exitDirection = new Vector2(1, 0);
                break;
            case Directions.Down:
                entityOffset = new Vector3(0, -entitySize.extents.y, 0);
                offSetDistanceFixed = new Vector3(0, -offSetDistance, 0);
                exitDirection = new Vector2(0, -1);
                break;
            case Directions.Left:
                entityOffset = new Vector3(-entitySize.extents.x, 0, 0);
                offSetDistanceFixed = new Vector3(-offSetDistance, 0, 0);
                exitDirection = new Vector2(-1, 0);
                break;
        }
        return transform.position + entityOffset + offSetDistanceFixed;
    }

    public void setActive(bool state)
    {
        isActive = state;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            if (portalToPortal)
            {
                float velocityX = collision.GetComponent<Rigidbody2D>().velocity.x;
                float velocityY = collision.GetComponent<Rigidbody2D>().velocity.y;
                if (velocityX > velocityY)
                    speed = Mathf.Abs(velocityX);
                if (velocityX < velocityY)
                    speed = Mathf.Abs(velocityY);
                portal.GetComponent<PortalBehavior>().setActive(true);
                collision.transform.position = portal.GetComponent<PortalBehavior>().portalExitPoint(collision.bounds);
                collision.GetComponent<Rigidbody2D>().AddForce(exitDirection * (speed * 1.2f), ForceMode2D.Impulse);
                portal.GetComponent<PortalBehavior>().setActive(false);
            }
            else
                collision.transform.position = exitPoint;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }
}
