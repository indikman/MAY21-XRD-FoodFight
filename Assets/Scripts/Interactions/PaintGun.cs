using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGun : GrabbableObject
{
    public GameObject bulletPrefab;
    public Transform gunTip;

    public float ShootForce;

    public Material[] colours;

    public override void Start()
    {
        //base.Start();
    }

    public override void OnTriggerStart()
    {

        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);

        bullet.GetComponent<Renderer>().material = colours[Random.Range(0, colours.Length)];
        bullet.GetComponent<Rigidbody>().AddForce(gunTip.forward * ShootForce, ForceMode.Impulse);

        Destroy(bullet, 5);


    }

    

}
