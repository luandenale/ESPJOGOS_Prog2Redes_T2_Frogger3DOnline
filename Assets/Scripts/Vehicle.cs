using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Vehicle : NetworkBehaviour
{
    public Spawn spawner;
    public float vehicleSpeed = 75f;
    public bool goingRight = true;
    public bool collidedPlayer = false;
    public bool carryingPlayer = false;

    public int id;
    protected static Dictionary<int, Vehicle> _spawnedVehicles = new Dictionary<int, Vehicle>();

    public void SetId(int id) {
        this.id = id;
        _spawnedVehicles[id] = this;
    }

    public static Vehicle GetById(int id) {
        return _spawnedVehicles[id];
    }

    public void DestroyVehicle() {
        if (carryingPlayer) {
            var players = GetComponentsInChildren<PlayerMovement>();
            foreach (PlayerMovement player in players) {
                player.transform.SetParent(null);
            }
        }
        _spawnedVehicles.Remove(id);
        Destroy(gameObject);
        spawner.vehicleCount -= 1;
    }
}
