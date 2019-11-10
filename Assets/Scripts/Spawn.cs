using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    public GameObject vehiclePrefab;
    private BoxCollider collider;
    private bool _spawnPointIsFree = false;

    public LayerMask vehicleLayer;

    public int maxTimeSpawn;
    public int vehicleCount = 0;
    public int maxVehicles;

    private float timeToSpawn;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        CreateCar();
    }
    
    private void Update()
    {
        timeToSpawn -= Time.fixedDeltaTime;

        if (timeToSpawn < 0 && _spawnPointIsFree && vehicleCount < maxVehicles)
        {
            SpawnCar();
        }
    }

    public void CreateCar()
    {
        GameObject vehicle = Instantiate(vehiclePrefab, startPos.position, Quaternion.identity, transform);
        vehicle.GetComponent<Car>().carSpawner = this; //checar se isso nao vai dar errado em runtime com a instancia de vehicle
        vehicleCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((vehicleLayer & (1 << other.gameObject.layer)) != 0)
            _spawnPointIsFree = true;        
    }

    private void SpawnCar()
    {
        _spawnPointIsFree = false;
        timeToSpawn = Random.Range(1f, maxTimeSpawn);

        CreateCar();
    }

}
