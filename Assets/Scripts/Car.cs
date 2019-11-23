using UnityEngine;
using UnityEngine.Networking;

public class Car : NetworkBehaviour
{
    public Spawn carSpawner;
    public float carSpeed = 75f;
    public bool goingRight = true;

    private void Start()
    {
        if (!goingRight)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        if (isServer) {

            if (goingRight)
            {
                if (transform.position.x < carSpawner.endPos.position.x)
                    transform.position = new Vector3((transform.position.x) + carSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                else
                    DestroyCar();
            }
            else
            {
                if (transform.position.x > carSpawner.endPos.position.x)
                    transform.position = new Vector3((transform.position.x) - carSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                else
                    DestroyCar();
            }

        }
    }



    public void DestroyCar()
    {
        Destroy(gameObject);
        carSpawner.vehicleCount -= 1;
    }
}
