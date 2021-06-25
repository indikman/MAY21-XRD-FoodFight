using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : GrabbableObject
{
    private Transform snapper;
    bool isSnapperAvailable = false;

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
        
        base.OnGrabStart(hand);
    }

    public override void OnGrabEnd()
    {
        
        if (isSnapperAvailable)
        {
            Debug.Log("SNAP");
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

            transform.SetParent(null);

            
            var tempPosition = snapper.position;
            tempPosition.x = transform.position.x;
            tempPosition.y = transform.position.y;

            transform.position = tempPosition;
            transform.rotation = snapper.rotation;

            snapper = null;

        }
        else
        {
            Debug.Log("NO SNAP");
            base.OnGrabEnd();
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "snapper")
        {
            Debug.Log(other.gameObject.name);
            snapper = other.transform;
            isSnapperAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "snapper")
        {
            snapper = null;
            isSnapperAvailable = false;
        }
    }
}
