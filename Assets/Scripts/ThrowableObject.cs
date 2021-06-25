using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject
{
    public int numVelocitySamples = 10;
    public float throwBoost = 1;

    FixedJoint joint;

    private Queue<Vector3> previousVelocities = new Queue<Vector3>();
    private Vector3 previousPosition;

    public override void OnHoverStart()
    {
        // Do nothing
    }

    public override void OnHoverEnd()
    {
        // do nothing
    }

    public override void OnGrabStart(Grabber hand)
    {
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = hand.GetComponent<Rigidbody>();
    }

    public override void OnGrabEnd()
    {
        Destroy(joint);

        var averageVelocity = Vector3.zero;

        foreach (var velocity in previousVelocities)
        {
            averageVelocity += velocity;
        }

        averageVelocity /= previousVelocities.Count;

        GetComponent<Rigidbody>().velocity = averageVelocity * throwBoost;

    }


    private void FixedUpdate()
    {
        var velocity = transform.position - previousPosition;

        previousPosition = transform.position;

        previousVelocities.Enqueue(velocity);

        if(previousVelocities.Count > numVelocitySamples)
        {
            previousVelocities.Dequeue();
        }

    }
}
