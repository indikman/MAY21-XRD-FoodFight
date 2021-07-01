using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocomotionMove : MonoBehaviour
{

    private enum hands { Left, Right }

    [SerializeField] private hands hand;


    [SerializeField] private Transform xrRig;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform playerCamera;

    [SerializeField] private int lineResolution = 20;
    [SerializeField] private Vector3 curveHeight;

    [SerializeField] private RawImage fader;


    public GameObject reticle;

    private LineRenderer line;

    private bool teleportLock = false;

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

        fader.color = Color.clear;

        line = GetComponent<LineRenderer>();

        line.positionCount = lineResolution;

        reticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        var verticalVal = Input.GetAxis(verticalAxis);
        var horizontalVal = Input.GetAxis(horizontalAxis);

        SmoothRotate(horizontalVal);
        SmoothMove(verticalVal);
        Teleport();
        
    }




    private void Teleport()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            line.enabled = true;

            // check whether this is a correct target to navigate to
            bool validTarget = hit.collider.tag == "ground";

            // change the colours of the line accordigly
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


            // getting the midpoint of the raycast (hand position and the hit point)

            Vector3 startPoint = transform.position;
            Vector3 endPoint = hit.point;
            Vector3 midPoint = startPoint + ((endPoint - startPoint) / 2f);
            midPoint += curveHeight;


            // Converting the hit point to a smooth follow, and setting up the reticle
            reticle.SetActive(true);

            Vector3 desiredPosition = endPoint - reticle.transform.position;
            Vector3 smoothVectodesired = (desiredPosition / 0.2f) * Time.deltaTime;
            Vector3 reticleEndpoint = reticle.transform.position + smoothVectodesired;

            reticle.transform.position = reticleEndpoint;

            reticle.transform.up = hit.normal;



            // set all the curved line positions
            for (int i = 0; i < lineResolution; i++)
            {
                float t = i / (float)lineResolution;
                Vector3 starToMid = Vector3.Lerp(startPoint, midPoint, t);
                Vector3 midToEnd = Vector3.Lerp(midPoint, reticleEndpoint, t);

                Vector3 curvePos = Vector3.Lerp(starToMid, midToEnd, t);

                //setting the position of all the points in the line, through a loop
                line.SetPosition(i, curvePos);
            }

            //------- This is just a straight line, not a curve
            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, hit.point);

            if (!teleportLock && validTarget && Input.GetButtonDown(triggerButton))
            {
                //xrRig.position = hit.point;

                // Use coroutine to teleport
                StartCoroutine(FadeTeleport(hit.point));

            }

        }
        else
        {
            reticle.SetActive(false);
            line.enabled = false;
        }
    }

    void SmoothRotate(float value)
    {
        // Torate the xrrig towards the y axis
        xrRig.Rotate(Vector3.up, rotateSpeed * value * Time.deltaTime);
    }

    void SmoothMove(float value)
    {
        Vector3 forwardDirection = new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z);
        forwardDirection.Normalize();

        xrRig.position += (-1) * value * Time.deltaTime * forwardDirection;
    }


    private IEnumerator FadeTeleport(Vector3 newPosition)
    {
        // disabling more teleports until the current teleport is over
        teleportLock = true;

        float currentTime = 0;

        // fade from clear to black
        while(currentTime < 1)
        {
            fader.color = Color.Lerp(Color.clear, Color.black, currentTime);
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;
        }

        fader.color = Color.black;

        // Teleport the user here!!
        xrRig.transform.position = newPosition;

        yield return new WaitForSeconds(0.5f);

        currentTime = 0;

        // fade from black to clear
        while (currentTime < 1)
        {
            fader.color = Color.Lerp(Color.black, Color.clear, currentTime);
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;
        }

        fader.color = Color.clear;

        teleportLock = false;
    }


}
