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
<<<<<<< HEAD
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
<<<<<<< HEAD

=======
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
>>>>>>> 91355c947d8a02accace66817bac5c676188542e
=======
>>>>>>> parent of b690242... Removed network transform from objects(Will fix death Soon)
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
