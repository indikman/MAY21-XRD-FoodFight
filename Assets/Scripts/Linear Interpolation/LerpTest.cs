using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;

    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0;
        }

        pointA.position = Vector3.Lerp(point1.position, point2.position, timer);
        pointB.position = Vector3.Lerp(point2.position, point3.position, timer);

        pointC.position = Vector3.Lerp(pointA.position, pointB.position, timer);


    }
}
