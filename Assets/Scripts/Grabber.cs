using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public string gripButton = "RightGripButton";
    public string triggerButton = "RightTriggerButton";

    public Animator anim;

    public GrabbableObject hoveredObject;
    public GrabbableObject grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(GetComponent<Rigidbody>().GetPointVelocity());

        if (Input.GetButtonDown(gripButton))
        {

            anim.SetTrigger("grab");

            if(hoveredObject != null)
            {
                hoveredObject.OnGrabStart(this);

                grabbedObject = hoveredObject;
                hoveredObject = null;
            }

        }

        if (Input.GetButtonUp(gripButton))
        {
            anim.SetTrigger("release");

            if (grabbedObject != null)
            {
                grabbedObject.OnGrabEnd();
                grabbedObject = null;
            }

        }

        if (Input.GetButtonDown(triggerButton))
        {
            if(grabbedObject != null)
            {
                grabbedObject.OnTriggerStart();
            }
        }

        if (Input.GetButton(triggerButton))
        {
            if (grabbedObject != null)
            {
                grabbedObject.OnTrigger();
            }
        }

        if (Input.GetButtonUp(triggerButton))
        {
            if (grabbedObject != null)
            {
                grabbedObject.OnTriggerEnd();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();
        if(grabbable != null)
        {
            hoveredObject = grabbable;
            hoveredObject.OnHoverStart();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();
        if(hoveredObject != null && grabbable == hoveredObject)
        {
            hoveredObject.OnHoverEnd();
            hoveredObject = null;
        }
    }

}
