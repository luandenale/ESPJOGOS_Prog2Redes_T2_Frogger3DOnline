using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCar : MonoBehaviour
{
    public float carSpeed = 75f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 20f)
            transform.position = new Vector3((transform.position.x) + carSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        else
            Destroy(gameObject);
    }
}
