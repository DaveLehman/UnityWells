using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;

public class MonsterAgent : Agent
{
    public float MoveSpeed = 1.0f;
    public float TurnSpeed = 90.0f;
    public float MaxVelocity = 10.0f;

    private Rigidbody ThisRigidBody;
    private ObjectSpawner SceneObjectSpawner;

    void Awake()
    {
        ThisRigidBody = GetComponent<Rigidbody>();
        SceneObjectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // called every time the Agent receives an action. The contents of VectorAction depend on whether the vector action space is continuous or discrete.
        // We defined our vecore action space to be discrete have a size of 2 and an index with maximum value of 2.
        // The values of MovementAction and RotationAction can be 0, 1 or 2 as defined in the Behaviors Parameters component. Consequently, we have 3 possible actions we can perform for 
        // movement and rotation.
        var MovementAction = (int)vectorAction[0];
        var RotationAction = (int)vectorAction[1];

        var MovementDir = Vector3.zero;
        if (MovementAction == 1)
        {
            MovementDir = transform.forward;
        }
        else if (MovementAction == 2)
        {
            MovementDir = -transform.forward;
        }

        var RotationDir = Vector3.zero;
        if (RotationAction == 1)
        {
            RotationDir = -transform.up;
        }
        else if (RotationAction == 2)
        {
            RotationDir = transform.up;
        }

        ApplyMovement(MovementDir, RotationDir);
    }

    private void ApplyMovement(Vector3 movementDir, Vector3 rotationDir)
    {
        ThisRigidBody.AddForce(movementDir * MoveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotationDir, Time.fixedDeltaTime * TurnSpeed);
        // clamp the maximum velocity
        if (ThisRigidBody.velocity.sqrMagnitude > MaxVelocity)
        {
            ThisRigidBody.velocity *= 0.95f;
        }
    }

    public override void OnEpisodeBegin()
    {
        // reset the environment back to its initial state so that learning can begin anew
        SceneObjectSpawner.Reset();
        ThisRigidBody.velocity = Vector3.zero;
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnCollisionEnter(Collision OtherCollision)
    {
        // reward for finding food
        if (OtherCollision.gameObject.CompareTag("Chick"))
        {
            Destroy(OtherCollision.gameObject);
            SceneObjectSpawner.SpawnFood();
            AddReward(2f);
        }
        else if (OtherCollision.gameObject.CompareTag("Rock"))
        {
            Destroy(OtherCollision.gameObject);
            SceneObjectSpawner.SpawnRock();
            AddReward(-1f);
        }
        else if (OtherCollision.gameObject.CompareTag("Wall"))
        {
            AddReward(-1f);
        }
    }
}
