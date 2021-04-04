using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLine : MonoBehaviour
{
    // calculates if a line of sight exists between the object running the script ("Enemy") and anohter object ("Player")
    public Transform EyePoint;
    public string TargetTag = "Player";
    public float FieldOfView = 45f;


    
    public bool IsTargetInSightLine { get; set; } = false;
    public Vector3 LastKnownSighting { get; set; } = Vector3.zero;

    private SphereCollider ThisCollider;

    void Awake()
    {
        //Debug.Log("SightLine.Awake()");
        ThisCollider = GetComponent<SphereCollider>();
        LastKnownSighting = transform.position;
    }

    // the enemy should have a Collider attached to it representing its maximum view distance. When OnTriggerStay fires for that
    // Collider, the Player may be within view
    void OnTriggerStay(Collider Other)
    {
        //Debug.Log("SightLine.OnTriggerStay()");
        if (Other.CompareTag(TargetTag))
        {
            UpdateSight(Other.transform);
        }
    }

    void OnTriggerExit(Collider Other)
    {
        //Debug.Log("SightLine.OnTriggerExit()");
        if (Other.CompareTag(TargetTag)) { IsTargetInSightLine = false; }
    }

    private bool HasClearLineofSightToTarget(Transform Target)
    {
        // casts a ray to determine if we can see the player -- (not blocked by obstacle)
        RaycastHit Info;

        Vector3 DirToTarget = (Target.position - EyePoint.position).normalized;
        if (Physics.Raycast(EyePoint.position, DirToTarget, out Info, ThisCollider.radius))
        {
            if (Info.transform.CompareTag(TargetTag))
            {
                return true;
            }
        }

        return false;
    }

    private bool TargetInFOV(Transform Target)
    {
        Vector3 DirToTarget = Target.position - EyePoint.position;
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        if (Angle <= FieldOfView)
        {
            return true;
        }

        return false;
    }

    private void UpdateSight(Transform Target)
    {
        //Debug.Log("SightLine.UpdateSight()");
        IsTargetInSightLine = HasClearLineofSightToTarget(Target) && TargetInFOV(Target);

        if (IsTargetInSightLine)
        {
            Debug.Log("Player spotted");
            LastKnownSighting = Target.position;
        }
    }
}
