using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Car : Vehicle
{
    public Material[] carPaintMaterials;

    private void Start()
    {
        if (!goingRight)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        if (goingRight)
        {
            if (transform.position.x < spawner.endPos.position.x)
                transform.position = new Vector3((transform.position.x) + vehicleSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else
                DestroyVehicle();
        }
        else
        {
            if (transform.position.x > spawner.endPos.position.x)
                transform.position = new Vector3((transform.position.x) - vehicleSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else
                DestroyVehicle();
        }
    }
}
