using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Material material;
    private Color defaultColor;

    public Color hoveredColor;


    // Start is called before the first frame update
    public virtual void Start()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
    }

    public virtual void OnTriggerStart()
    {

    }

    public virtual void OnTriggerEnd()
    {

    }

    public virtual void OnTrigger()
    {

    }


    public virtual void OnHoverStart()
    {
        material.color = hoveredColor;
    }

    public virtual void OnHoverEnd()
    {
        material.color = defaultColor;
    }

    public virtual void OnGrabStart(Grabber hand)
    {
        transform.SetParent(hand.transform);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public virtual void OnGrabEnd()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        
    }
}
