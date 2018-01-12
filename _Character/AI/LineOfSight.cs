using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    //------------------------------------------
    // Public variables
    public enum SightSensitivity { STRICT, LOOSE };                 //How sensitive should the entity be to sight
    public SightSensitivity Sensitity = SightSensitivity.STRICT;    //Sight sensitivity
    public bool CanSeeTarget = false;                               //Can we see target
    public float FieldOfView = 45f;                                 //FOV
    public Vector3 LastKnowSighting = Vector3.zero;                 //Reference to last know object sighting, if any
    public Transform EyePoint = null;                               //Reference to eyes

    //Internal
    private Transform Target = null;                                //Reference to target
    private Transform ThisTransform = null;                         //Reference to transform component
    private SphereCollider ThisCollider = null;                     //Reference to sphere collider
    //------------------------------------------

    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position;
        Target = GameObject.FindGameObjectWithTag(Tags.PLAYER).
        GetComponent<Transform>();
    }

    bool InFOV()
    {
        //Get direction to target
        Vector3 DirToTarget = Target.position - EyePoint.position;
        //Get angle between forward and look direction
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);
        //Are we within field of view?
        if (Angle <= FieldOfView)
            return true;
        //Not within view
        return false;
    }

    public bool ClearLineofSight()
    {
        RaycastHit Info;
        if (Physics.Raycast(EyePoint.position, (Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius))
        {
            if (Info.transform.CompareTag(Tags.PLAYER))
                return true;
        }
        return false;
    }

    void UpdateSight()
    {
        switch (Sensitity)
        {
            case SightSensitivity.STRICT:
                CanSeeTarget = InFOV() && ClearLineofSight();
                break;
            case SightSensitivity.LOOSE:
                CanSeeTarget = InFOV() || ClearLineofSight();
                break;
        }
    }

    void OnTriggerStay(Collider Other)
    {
        UpdateSight();
        //Update last known sighting
        if (CanSeeTarget)
            LastKnowSighting = Target.position;
    }

    void OnTriggerExit(Collider Other)
    {
        if (!Other.CompareTag(Tags.PLAYER)) return;
        CanSeeTarget = false;
    }

}
