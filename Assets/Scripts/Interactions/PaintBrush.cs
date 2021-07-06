using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : GrabbableObject
{
    public GameObject paintPrefab;
    public Transform paintBrushTip;

    private GameObject paint;
    private PaintBrushTip tip;

    public override void Start()
    {
        base.Start();
        tip = GetComponentInChildren<PaintBrushTip>();
    }


    public override void OnTriggerStart()
    {
        if (tip.isColourSelected())
        {
            paint = Instantiate(paintPrefab, paintBrushTip.position, paintBrushTip.rotation);
            paint.GetComponent<Renderer>().material = paintBrushTip.GetComponent<Renderer>().material;
        }

    }

    public override void OnTrigger()
    {
        if(paint!=null)
            paint.transform.position = paintBrushTip.transform.position;
    }

    public override void OnTriggerEnd()
    {
        if(paint != null)
        {
            paint.transform.position = paint.transform.position;
            paint = null;
        }
        
    }

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();
        tip.ResetColour();
    }
}
