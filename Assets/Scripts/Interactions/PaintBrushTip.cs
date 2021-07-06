using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrushTip : MonoBehaviour
{
    private bool _isColourSelected;

    private Material defaultMaterial;



    public bool isColourSelected()
    {
        return _isColourSelected;
    }

    private void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        _isColourSelected = false;
    }

    public void ResetColour()
    {
        GetComponent<Renderer>().material = defaultMaterial;
        _isColourSelected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "paint")
        {
            GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
            _isColourSelected = true;
        }
    }
}
