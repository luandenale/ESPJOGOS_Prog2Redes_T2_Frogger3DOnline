using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Car : NetworkBehaviour
{
    public Spawn carSpawner;
    public float carSpeed = 75f;
    public bool goingRight = true;
    public bool collidedPlayer = false;
    public bool carryingPlayer = false;
    public Material[] carPaintMaterials;
    
    public int id;
    private static Dictionary<int, Car> _spawnedCars = new Dictionary<int, Car>();

    public void SetId(int id)
    {
        this.id = id;
        _spawnedCars[id] = this;
    }

    public static Car GetById(int id)
    {
        return _spawnedCars[id];
    }

    private void Start()
    {
        if (!goingRight)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {

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

    public void DestroyCar()
    {
        if (carryingPlayer) {
            var players = GetComponentsInChildren<PlayerMovement>();
            foreach (PlayerMovement player in players) {
                player.transform.SetParent(null);
            }
        }
        _spawnedCars.Remove(id);
        Destroy(gameObject);
        carSpawner.vehicleCount -= 1;
    }
}
