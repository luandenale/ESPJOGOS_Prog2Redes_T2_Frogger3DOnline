using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private GameObject _vehiclePrefab;
    private PlayerDistanceSpawn playerDistanceSpawn;
    private BoxCollider _collider;
    [SerializeField]
    private bool _spawnPointIsFree = true;

    public LayerMask vehicleLayer;

    public int maxTimeSpawn;
    public int vehicleCount = 0;
    public int maxVehicles;
    public float vehicleSpeed = 75f;
    public bool goingRight = true;
    public VehicleType vehicleType;

    private float timeToSpawn;
    private static int _nextId;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        playerDistanceSpawn = GetComponent<PlayerDistanceSpawn>();
    }
    
    private void Update()
    {
        if (GameManager.instance.startGame) {

            if (isServer) {
                playerDistanceSpawn.CheckPlayersPos();
                timeToSpawn -= Time.fixedDeltaTime;
                if (playerDistanceSpawn.canSpawn) {

                    if (timeToSpawn < 0 && _spawnPointIsFree && vehicleCount < maxVehicles)
                    {
                        SpawnVehicle();
                    }
                }
            }

        }
    }

    private void SpawnVehicle()
    {
        if (vehicleType == VehicleType.Car) {
            _spawnPointIsFree = false;
            timeToSpawn = Random.Range(1f, maxTimeSpawn);
            int i = Random.Range(0, 4);
            CmdCreateCar(i);
        }
        else {
            _spawnPointIsFree = false;
            timeToSpawn = Random.Range(6f, maxTimeSpawn);
            CmdCreateTrain();
        }
    }

    [Command]
    public void CmdCreateCar(int i) {
        RpcCreateCar(_nextId++, i);
    }

    [ClientRpc]
    public void RpcCreateCar(int id, int i)
    {
        GameObject __vehicle = Instantiate(_vehiclePrefab, startPos.position, _vehiclePrefab.transform.rotation, transform);
        var car = __vehicle.GetComponent<Car>();
        car.spawner = this; //checar se isso nao vai dar errado em runtime com a instancia de vehicle
        car.vehicleSpeed = vehicleSpeed;
        car.goingRight = goingRight;
        car.SetId(id);

        var newMaterial = car.GetComponent<Renderer>().sharedMaterials;

        newMaterial[1] = car.carPaintMaterials[i];
        car.GetComponent<Renderer>().sharedMaterials = newMaterial;

        vehicleCount++;
    }

    [Command]
    public void CmdCreateTrain() {
        RpcCreateTrain(_nextId);
    }

    [ClientRpc]
    public void RpcCreateTrain(int id) {
        GameObject __vehicle = Instantiate(_vehiclePrefab, startPos.position, _vehiclePrefab.transform.rotation, transform);
        var train = __vehicle.GetComponent<Train>();
        train.spawner = this;
        train.vehicleSpeed = vehicleSpeed;
        train.goingRight = goingRight;
        train.SetId(id);

        vehicleCount++;
    }



    private void OnTriggerExit(Collider other)
    {
        if ((vehicleLayer & (1 << other.gameObject.layer)) != 0)
            _spawnPointIsFree = true;        
    }

}
