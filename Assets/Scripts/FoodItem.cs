using UnityEngine;

public class FoodItem : ThrowableObject
{
    private Grabber tempHand;

    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

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
        tempHand = hand;

        base.OnGrabStart(hand);
    }

    public override void OnGrabEnd()
    {
         base.OnGrabEnd();
        Destroy(gameObject, 5);
    }
}
