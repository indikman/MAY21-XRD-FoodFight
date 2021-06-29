using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMove : MonoBehaviour
{

    private enum hands { Left, Right }

    [SerializeField] private hands hand;


    [SerializeField] private Transform xrRig;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform playerCamera;

    public GameObject reticle;

    private LineRenderer line;

    public string verticalAxis;
    public string horizontalAxis;
    public string triggerButton;

    [Header("Line Colours")]
    public Color validStart;
    public Color validEnd;
    public Color invalidStart;
    public Color invalidEnd;

    // Start is called before the first frame update
    void Start()
    {
        verticalAxis = "XRI_" + hand + "_Primary2DAxis_Vertical";
        horizontalAxis = "XRI_" + hand + "_Primary2DAxis_Horizontal";
        triggerButton =  hand + "TriggerButton";

        line = GetComponent<LineRenderer>();
        reticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        var verticalVal = Input.GetAxis(verticalAxis);
        var horizontalVal = Input.GetAxis(horizontalAxis);

        SmoothRotate(horizontalVal);
        SmoothMove(verticalVal);

        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            line.enabled = true;

            bool validTarget = hit.collider.tag == "ground";

            if (validTarget)
            {
                line.startColor = validStart;
                line.endColor = validEnd;
            }
            else
            {
                line.startColor = invalidStart;
                line.endColor = invalidEnd;
            }

            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);

            if (validTarget && Input.GetButtonDown(triggerButton))
            {
                xrRig.position = hit.point;
            }

            reticle.SetActive(true);
            reticle.transform.position = hit.point;

            reticle.transform.up = hit.normal;

        }
        else
        {
            reticle.SetActive(false);
            line.enabled = false;
        }


    }

    void SmoothRotate(float value)
    {
        xrRig.Rotate(Vector3.up, rotateSpeed * value * Time.deltaTime);
    }

    void SmoothMove(float value)
    {
        Vector3 forwardDirection = new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z);
        forwardDirection.Normalize();

        xrRig.position += (-1) * value * Time.deltaTime * forwardDirection;
    }



}
