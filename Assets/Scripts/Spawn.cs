using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private GameObject _vehiclePrefab;
    private BoxCollider _collider;
    private bool _spawnPointIsFree = true;

    public LayerMask vehicleLayer;

    public int maxTimeSpawn;
    public int vehicleCount = 0;
    public int maxVehicles;
    public float vehicleSpeed = 75f;
    public bool goingRight = true;

    private float timeToSpawn;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        //CreateCar();
    }
    
    private void Update()
    {
        if (GameManager.startGame) {

            if (isServer) {
                timeToSpawn -= Time.fixedDeltaTime;

                if (timeToSpawn < 0 && _spawnPointIsFree && vehicleCount < maxVehicles)
                {
                    SpawnCar();
                }

            }

        }
    }

    private void SpawnCar()
    {
        _spawnPointIsFree = false;
        timeToSpawn = Random.Range(1f, maxTimeSpawn);

        CreateCar();
    }

    
    public void CreateCar()
    {
        GameObject __vehicle = Instantiate(_vehiclePrefab, startPos.position, Quaternion.identity, transform);
        __vehicle.GetComponent<Car>().carSpawner = this; //checar se isso nao vai dar errado em runtime com a instancia de vehicle
        __vehicle.GetComponent<Car>().carSpeed = vehicleSpeed;
        __vehicle.GetComponent<Car>().goingRight = goingRight;
        vehicleCount++;
        NetworkServer.Spawn(__vehicle);
    }


    private void OnTriggerExit(Collider other)
    {
        if ((vehicleLayer & (1 << other.gameObject.layer)) != 0)
            _spawnPointIsFree = true;        
    }

}
