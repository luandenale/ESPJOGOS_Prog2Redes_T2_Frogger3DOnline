using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Spawn carSpawner;
    private float carSpeed = 75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < carSpawner.endPos.position.x)
        {
            transform.position = new Vector3((transform.position.x)  + carSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else {
            DestroyCar();
        }
    }

    public void DestroyCar()
    {
        Destroy(gameObject);
        carSpawner.vehicleCount -= 1;
    }


}
